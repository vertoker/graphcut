using UnityEngine;

public class Graph : MonoBehaviour//Скрипт для графа
{
    public float speed = 75f;
    public Lines lines;
    public Menu menu;
    public Vector3 cubeBorders;
    public int id;
    public Transform tr;
    public MeshRenderer mr;
    private Vector3 target;
    private bool isOneTime = true;

    private void Start()//Стартовая рандомная позиция
    {
        float x = Random.Range(-cubeBorders.x, cubeBorders.x);
        float y = Random.Range(-cubeBorders.y, cubeBorders.y);
        float z = Random.Range(-cubeBorders.z, cubeBorders.z);
        target = new Vector3(x, y, z);
    }

    private void OnTriggerEnter(Collider other)//Проигрыш
    {
        if (other.CompareTag("SearchCut") && isOneTime == true)
        {
            isOneTime = false;
            mr.material = menu.error;
            tr.localScale = new Vector3(2f, 2f, 2f);
            lines.GraphGet(id, mr, tr);//Метод проигрыша
        }
    }
    
    private void Update()
    {
        float t = Time.timeScale;
        if (t != 0)
        {
            if (tr.position == target)//Рандомная позиция
            {
                float x = Random.Range(-cubeBorders.x, cubeBorders.x);
                float y = Random.Range(-cubeBorders.y, cubeBorders.y);
                float z = Random.Range(-cubeBorders.z, cubeBorders.z);
                target = new Vector3(x, y, z);
            }
            float s = Time.fixedDeltaTime * Time.deltaTime;//Стабилизация
            float c = (speed + menu.count * 0.1f) * s;
            tr.position = Vector3.MoveTowards(tr.position, target, c);//Движение
        }
    }
}