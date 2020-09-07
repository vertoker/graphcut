using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cut : MonoBehaviour//Обрабатывает свайпы в физические касания
{
    public Lines lines;
    public float activity = 0f;
    public GameObject startCut;
    public GameObject endCut;
    public GameObject lineCut;
    public GameObject searchCut;
    public bool isGame = false;//Игрок в игре или в меню
    public int count { get; private set; }//Количество порезанных линий за раз
    public bool isActiveActivity = false;//Используется для спауна монет
    private bool isMove = false;//Движение пальца по экрану
    public float c = 191.9704f;//Перевод из ui в world
    private Vector2 scr = new Vector2(1080f, 1920f);//Координаты экрана в ui
    private RectTransform trStart;
    private RectTransform trEnd;
    private LineRenderer lr;
    public bool isTeaching = false;//В обучении ли игра
    AspectRatio aspectRatio;
    Menu menu; Teach teach;

    private void Start()
    {
        aspectRatio = GetComponent<AspectRatio>();
        teach = GetComponent<Teach>();
        menu = GetComponent<Menu>();
        scr = new Vector2(1080f, 1080f / aspectRatio.cam.aspect);
        trStart = startCut.GetComponent<RectTransform>();
        trEnd = endCut.GetComponent<RectTransform>();
        lr = lineCut.GetComponent<LineRenderer>();
        count = 0;
        StartCoroutine(RemovingActivity());
        Started();
    }

    public void Started()//Обнуление объектов после нажатия
    {
        count = 0;
        startCut.SetActive(false);
        endCut.SetActive(false);
        lineCut.SetActive(false);
        trStart.localPosition = new Vector2(0f, 0f);
        trEnd.localPosition = new Vector2(0f, 0f);
        lines.StartCoin();
    }

    public void ColorUpdate(ColorsGame color)//Смена цвета (магазин)
    {
        startCut.GetComponent<Image>().color = color.colorTouches;
        endCut.GetComponent<Image>().color = color.colorTouches;
        lr.startColor = color.colorCutLine;
        lr.endColor = color.colorCutLine;
    }

    public void DownCut()//При нажатии на экран
    {
        if (isGame == true)
        {
            startCut.SetActive(true);
            trStart.localPosition = PosCut();
            menu.EnableOffRenderGameUI();
        }
        if (isTeaching == true)
        {
            teach.Down(trStart.localPosition);
        }
    }

    public void BeginDragCut()//При старте движения по экрану
    {
        if (isGame == true)
        {
            lineCut.SetActive(true);
            endCut.SetActive(true);
        }
        if (isTeaching == true)
        {
            teach.BeginDrag();
        }
    }

    public void DragCut()//При движения по экрану
    {
        if (isGame == true)
        {
            isMove = true;
            trEnd.localPosition = PosCut();
            Vector2 p1 = trStart.localPosition / c;
            Vector2 p2 = trEnd.localPosition / c;
            lr.SetPositions(new Vector3[] { p1, p2 });
            menu.EnableOffRenderGameUI();
        }
        if (isTeaching == true)
        {
            teach.Drag();
        }
    }

    public void UpCut()//При окончания нажатия на экран
    {
        Vector2 ps = trEnd.localPosition;
        if (isGame == true)
        {
            if (isMove == true)//Если было движение
            {
                Vector2 p1 = trStart.localPosition / c;
                Vector2 p2 = trEnd.localPosition / c;
                Vector2 p = (p1 + p2) / 2f;
                //Создаёт триггер, эмулирующий резание
                GameObject g = Instantiate(searchCut, p, Quaternion.identity);
                g.transform.up = g.transform.position - (Vector3)p2;
                g.transform.localScale = new Vector3(g.transform.localScale.x, Vector3.Distance(p1, p2), 18f);
                StartCoroutine(DestroyCoroutine(g));
            }
            Started();
        }
        if (isTeaching == true)
        {
            teach.Up(ps);
        }
        else
        {
            menu.EnableOffRenderGameUI();//Продливает активность ui
        }
        isMove = false;
    }

    public IEnumerator DestroyCoroutine(GameObject g)//Удаление объекта
    {
        yield return new WaitForSecondsRealtime(0.05f);
        Destroy(g);
    }

    private Vector2 PosCut()//Модифицирует позицию из ui в world
    {
        Vector2 pos = Input.mousePosition;
        pos = new Vector2(pos.x / Screen.width * scr.x, pos.y / Screen.height * scr.y);
        pos = new Vector2(pos.x - (scr.x / 2f), pos.y - (scr.y / 2f));
        return pos;
    }
    
    public void AddingActivity(int countInput, int plus)//Добавление активности игрока
    {
        count = countInput;
        activity += plus;
        return;
    }

    public IEnumerator RemovingActivity()//Спаун монет в зависимости от активности игрока
    {
        yield return new WaitForSecondsRealtime(0.25f);
        if (isActiveActivity == true)
        {
            float c = count * 0.005f;
            if (c > 0.98f) { c = 0.98f; }
            float c2 = count * 0.01f;
            if (c2 > 1f) { c2 = 1f; }
            activity = activity * c2 - (0.99f - c);
            if (activity < 0f) { activity = 0f; }
        }
        lines.AddActivity(activity);
        StartCoroutine(RemovingActivity());
    }
}