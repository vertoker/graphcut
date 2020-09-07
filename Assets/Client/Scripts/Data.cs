using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour//Основные команды по смене цветовой темы
{
    public bool isPaused;
    public Vector3 cubeBorders = new Vector3(2.8f, 5f, 9f);//Барьеры
    public ColorsGame[] colors;
    public GameObject[] colorButs;
    public GameObject activeRender;
    public Material graph;
    public Material line;
    public Material coin;
    public Camera cam;
    public Market market;
    private int activeID;//ID активной цветовой темы
    Menu menu; Cut cut;
    
    private void Start()
    {
        menu = GetComponent<Menu>();
        cut = GetComponent<Cut>();
        cubeBorders = new Vector3(5f * cam.aspect, 5f, 9f);
        if (!PlayerPrefs.HasKey("ColorSelectTheme"))
        { PlayerPrefs.SetInt("ColorSelectTheme", 0); }
        activeID = PlayerPrefs.GetInt("ColorSelectTheme");

        SetColors(activeID);
        MenuColorRenderer();
        MenuColorIconRenderer();
        ColorRenderer();
    }
    public void SetColors(int id)//Меняет цветовую тему (основа)
    {
        PlayerPrefs.SetInt("ColorSelectTheme", id);
        activeID = PlayerPrefs.GetInt("ColorSelectTheme");
        MenuColorIconRenderer();
        MenuColorRendererLinesBackGround();
        ColorRenderer();
        market.Disable();
        market.Select(activeID);
    }
    public ColorsGame GetColors() { return colors[activeID]; }//Выдаёт активные цвета
    private void ColorRenderer()//Смена темы в игре
    {
        cam.backgroundColor = colors[activeID].colorBackGround;

        graph.color = colors[activeID].colorGraphs;
        line.color = colors[activeID].colorLines;
        coin.color = colors[activeID].colorLinesBackground;

        cut.ColorUpdate(colors[activeID]);
        menu.ColorUpdate(colors[activeID]);
    }
    private void MenuColorRenderer()//Смена темы в меню
    {
        for (int i = 0; i < colorButs.Length; i++)
        {
            Transform t = colorButs[i].transform.GetChild(0).GetChild(0);
            colorButs[i].transform.GetChild(0).GetComponent<Image>().color = colors[i].colorBackGroundBackGround;
            t.GetChild(0).GetComponent<Image>().color = colors[i].colorBackGround;
            t.GetChild(1).GetComponent<Image>().color = colors[i].colorGraphs;
            t.GetChild(2).GetComponent<Image>().color = colors[i].colorLines;
            t.GetChild(3).GetComponent<Image>().color = colors[i].colorTouches;
            t.GetChild(4).GetComponent<Image>().color = colors[i].colorCutLine;
            t.GetChild(5).GetComponent<Image>().color = colors[i].colorError;
            t.GetChild(6).GetComponent<Image>().color = colors[i].colorLinesBackground;
            t.GetChild(7).GetComponent<Image>().color = colors[i].colorInfoIcons;
        }
        MenuColorRendererLinesBackGround();
        return;
    }
    private void MenuColorRendererLinesBackGround()//Смена цвета у линий
    {
        for (int i = 0; i < colorButs.Length; i++)
        {
            colorButs[i].GetComponent<Image>().color = colors[activeID].colorLinesBackground;
        }
        return;
    }
    private void MenuColorIconRenderer()//Смена цвета у иконок
    {
        int i = activeID;
        Transform t = activeRender.transform.GetChild(0).GetChild(0);
        activeRender.GetComponent<Image>().color = colors[i].colorLinesBackground;
        activeRender.transform.GetChild(0).GetComponent<Image>().color = colors[i].colorBackGroundBackGround;
        t.GetChild(0).GetComponent<Image>().color = colors[i].colorBackGround;
        t.GetChild(1).GetComponent<Image>().color = colors[i].colorGraphs;
        t.GetChild(2).GetComponent<Image>().color = colors[i].colorLines;
        t.GetChild(3).GetComponent<Image>().color = colors[i].colorTouches;
        t.GetChild(4).GetComponent<Image>().color = colors[i].colorCutLine;
        t.GetChild(5).GetComponent<Image>().color = colors[i].colorError;
        t.GetChild(6).GetComponent<Image>().color = colors[i].colorLinesBackground;
        t.GetChild(7).GetComponent<Image>().color = colors[i].colorInfoIcons;
        return;
    }
}

[System.Serializable]
public class ColorsGame//Цветовая тема
{
    public Color colorBackGroundBackGround = Color.grey;
    public Color colorBackGround = Color.white;
    public Color colorGraphs = Color.white;
    public Color colorLines = Color.white;
    public Color colorTouches = Color.white;
    public Color colorCutLine = Color.white;
    public Color colorError = Color.white;
    public Color colorLinesBackground = Color.black;
    public Color colorInfoIcons = Color.black;
}