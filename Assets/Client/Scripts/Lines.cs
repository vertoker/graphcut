using System.Collections;
using UnityEngine;

public class Lines : MonoBehaviour//Скрипт для линий
{
    public GameObject[] gameGraphs;//Объекты графов
    public GameObject[] coins;//Монеты на сцене
    public Vector2[] lineRendererIds;//ID для линий между графами
    public float speedGraph = 75f;
    public GameObject line;
    public GameObject graph;
    public GameObject coin;
    private Data d;
    private Menu m;
    private Cut ct;
    public GameObject[] lines;//Объекты линий
    private Line[] linesHash;//Скрипты линий
    private Vector3 rot = new Vector3(90f, 0f, 0f);//Константа для поворота линий
    private bool isOneStartSet = false;//Только один раз активирует cut
    private bool isSingleStartCoin = true;
    private int[] idsAdd;//ID линий
    private Lines lins;
    private Vector3 cubeBorders;
    private GameObject canvas;

    private void Start()
    {
        idsAdd = new int[0];
        lins = GetComponent<Lines>();
        canvas = GameObject.FindWithTag("Canvas");
        ct = canvas.GetComponent<Cut>();
        ct.lines = lins;
        d = canvas.GetComponent<Data>();
        m = canvas.GetComponent<Menu>();
        lines = new GameObject[lineRendererIds.Length];
        linesHash = new Line[lineRendererIds.Length];
        coins = new GameObject[0];
        for (int i = 0; i < lineRendererIds.Length; i++)
        {
            lines[i] = Instantiate(line, transform);
            linesHash[i] = lines[i].GetComponent<Line>();
            linesHash[i].id = i;
            linesHash[i].lines = lins;
        }
        cubeBorders = d.cubeBorders;
        for (int i = 0; i < gameGraphs.Length; i++)
        {
            Graph grph = gameGraphs[i].GetComponent<Graph>();
            grph.cubeBorders = cubeBorders;
            grph.menu = m;
        }
    }
    
    private GameObject g;
    private GameObject obj1;
    private GameObject obj2;
    public void ReBuildLine(int id)//Перестройка порезанной линии
    {
        Time.timeScale = 0f;
        Vector2 v2 = CoordinatesNewElementCreate(id);
        Vector3 v = LinesNewElementCreate(id, v2);
        GraphsNewElementCreate(id, v);
        Time.timeScale = 1f;
        return;
    }
    private Vector2 CoordinatesNewElementCreate(int id)//Координата для новой линии
    {
        int length = gameGraphs.Length;
        Vector2[] l = lineRendererIds;
        Vector2 v = l[id];
        Vector2[] ln = new Vector2[l.Length + 1];
        for (int i = 0; i < l.Length; i++)
        { ln[i] = l[i]; }
        ln[id] = new Vector2(v.x, length);
        ln[l.Length] = new Vector2(length, v.y);
        lineRendererIds = ln;
        return v;
    }
    private Vector3 LinesNewElementCreate(int id, Vector2 v2)//Создание новой линии
    {
        Vector3 r1 = gameGraphs[(int)v2.x].transform.position;
        Vector3 r2 = gameGraphs[(int)v2.y].transform.position;
        Vector3 v = (r1 + r2) / 2f;

        GameObject[] nl = new GameObject[lines.Length + 1];
        Line[] nlh = new Line[lines.Length + 1];

        for (int i = 0; i < lines.Length; i++)
        { nl[i] = lines[i]; nlh[i] = linesHash[i]; }

        g = nl[id];

        obj1 = Instantiate(line, transform);
        nl[lines.Length] = obj1;
        nlh[lines.Length] = obj1.GetComponent<Line>();
        nlh[lines.Length].lines = lins;
        nlh[lines.Length].id = lines.Length;
        
        obj2 = Instantiate(line, transform);
        nl[id] = obj2;
        nlh[id] = obj2.GetComponent<Line>();
        nlh[id].lines = lins;
        nlh[id].id = id;

        linesHash = nlh;
        lines = nl;
        Destroy(g);
        return v;
    }
    private void GraphsNewElementCreate(int id, Vector3 v)//Создание нового графа
    {
        int length = gameGraphs.Length;
        GameObject[] ng = new GameObject[length + 1];
        for (int i = 0; i < length; i++)
        { ng[i] = gameGraphs[i]; }
        ng[length] = Instantiate(graph, v, Quaternion.identity, transform);
        Graph grph = ng[length].GetComponent<Graph>();
        grph.cubeBorders = d.cubeBorders;
        speedGraph++;
        grph.speed = speedGraph;
        grph.lines = lins;
        grph.id = length;
        grph.menu = m;
        gameGraphs = ng;
        return;
    }

    public void LineGet(int id)//Метод при порезе линии
    {
        if (isOneStartSet == false)
        {
            isOneStartSet = true;
            StartCoroutine(SetCut());
        }
        idsAdd = Add(id);
    }

    private IEnumerator SetCut()//Перестраивает линии и добавляет новые очки
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (isLife)
        {
            for (int i = 0; i < idsAdd.Length; i++)
            {
                ReBuildLine(idsAdd[i]);
            }
            m.PlusCount(idsAdd.Length);
            if (PlayerPrefs.GetInt("MaxCutSimpleScore") < idsAdd.Length)
            {
                PlayerPrefs.SetInt("MaxCutSimpleScore", idsAdd.Length);
            }
            idsAdd = new int[0];
        }
        isOneStartSet = false;
        isLife = true;
    }

    public int[] Add(int c)//Добавляет объект в массив
    {
        int[] o = idsAdd;
        if (o.Length == 0) { return new int[] { c }; }
        int[] n = new int[o.Length + 1];
        for (int i = 0; i < o.Length; i++)
        { n[i] = o[i]; }
        n[o.Length] = c;
        return n;
    }

    private bool isLife = true;
    public void GraphGet(int id, MeshRenderer mr, Transform tr)//Метод при порезе графа
    {
        isLife = false;
        Time.timeScale = 0f;
        m.PreLose(mr, tr);
    }

    private void SigleUpdate(int id)//Функция Update для отдельного объекта
    {
        Vector3 p1 = gameGraphs[(int)lineRendererIds[id].x].transform.position;
        Vector3 p2 = gameGraphs[(int)lineRendererIds[id].y].transform.position;

        Vector3 p = (p1 + p2) / 2f;
        float scale = Vector3.Distance(p1, p2) / 2f;

        lines[id].transform.position = p;
        lines[id].transform.LookAt(p2);
        Vector3 r = lines[id].transform.localEulerAngles;
        lines[id].transform.localEulerAngles = new Vector3(r.x + rot.x, r.y + rot.y, r.z + rot.z);
        lines[id].transform.localScale = new Vector3(0.04f, scale - 0.05f, 0.04f);
        return;
    }

    private void Update()//Просчитывает движения игровых объектов
    {
        for (int i = 0; i < lineRendererIds.Length; i++)
        {
            Vector3 p1 = gameGraphs[(int)lineRendererIds[i].x].transform.position;//Позиция 1 графа
            Vector3 p2 = gameGraphs[(int)lineRendererIds[i].y].transform.position;//Позиция 2 графа
            Vector3 p = (p1 + p2) / 2f;//Точка между двух графов
            float scale = Vector3.Distance(p1, p2) / 2f;//Высота линии (в scale)
            Transform tr = lines[i].transform;
            tr.position = p;//Задаёт position линии
            tr.LookAt(p2);//Задаёт rotation линии
            Vector3 r = tr.localEulerAngles;//Rotation линии
            tr.localEulerAngles = new Vector3(r.x + rot.x, r.y + rot.y, r.z + rot.z);//Вращение линии
            tr.localScale = new Vector3(1f, scale - 0.05f, 1f);//Присваивание линии размер
            if (lines[i].activeSelf == false) { lines[i].SetActive(true); }//Активирует линию
        }
    }

    public float allActivity = 0f;
    public void AddActivity(float activity)//Добавляет активность
    {
        allActivity += activity;
    }
    public void StartCoin()//Запускает спаун монет
    {
        if (isSingleStartCoin == true)
        {
            isSingleStartCoin = false;
            StartCoroutine(CoinSpawn());
        }
        return;
    }
    public IEnumerator CoinSpawn()//Снаунер монет
    {
        float aa = allActivity;
        if (aa != 0)
        {
            float end = 1f / aa;
            if (end < 0.5f) { end = 0.5f; }
            yield return new WaitForSeconds(end);
            GameObject g = Instantiate(coin, transform);
            float x = Random.Range(-cubeBorders.x, cubeBorders.x);
            float y = Random.Range(-cubeBorders.y, cubeBorders.y);
            g.transform.position = new Vector3(x, y, 10f);
            g.GetComponent<Coin>().menu = canvas.GetComponent<Menu>();
            allActivity = 0f;
            StartCoroutine(CoinSpawn());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(CoinSpawn());
        }
    }
}