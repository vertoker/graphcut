using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Market : MonoBehaviour
{
    public GameObject[] locks;//Объекты замков
    public Image[] selects;//Объект подстветки выбора
    public int[] sales;//Массив цен
    public GameObject buy;//Кнопка покупки
    public Text sale;
    public Menu menu;
    public Data data;
    private int select = -1;//ID активной темы
    private int selectUnlock = 0;//ID разблокированной темы
    private int selectLock = -1;//ID заблокированной тем
    private int lengthSelects = 0;//Количество тем

    public void Start()
    {
        lengthSelects = selects.Length;
        if (!PlayerPrefs.HasKey("Lock0"))
        { menu.PlayerPrefsStartGenerate2(); }
        LocksSet();
        StartCoroutine(Sel());
    }

    public IEnumerator Sel()
    {
        yield return new WaitForSeconds(1f);
        Select(PlayerPrefs.GetInt("ColorSelectTheme"));
    }

    public void LocksSet()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i].SetActive(PlayerPrefs.GetString("Lock" + i) == "locked");
        }
        return;
    }

    public void UpdateColors()
    {
        ColorsGame colorsGame = data.GetColors();
        for (int i = 0; i < lengthSelects; i++)
        {
            selects[i].color = colorsGame.colorLinesBackground;
        }
        if (selectLock >= 0)
        {
            selects[selectLock].color = colorsGame.colorGraphs;
        }
        if (selectUnlock >= 0)
        {
            selects[selectUnlock].color = colorsGame.colorLines;
        }
        return;
    }

    public void Select(int id)//Обрабатывает нажатие на разблокированный объект
    {
        selectUnlock = id;
        selectLock = -1;
        UpdateColors();
    }

    public void SelectLock(int id)//Обрабатывает нажатие на заблокированный объект
    {
        if (selectLock != id)
        { selectLock = id; }
        else { selectLock = -1; }
        UpdateColors();
    }

    public void ButLock(int id)//Обрабатывает нажатие на заблокированный объект
    {
        if (select != id)
        {
            Enable(id);
        }
        else
        {
            Disable();
        }
    }

    public void Enable(int id)//Активация выделения кнопки
    {
        select = id;
        sale.text = sales[select].ToString();
        buy.SetActive(true);
        return;
    }

    public void Disable()//Деактивация выделения кнопки
    {
        select = -1;
        buy.SetActive(false);
        return;
    }

    public void Buy()//Покупка темы
    {
        if (sales[select] <= menu.countMoney)
        {
            menu.MinusBuying(sales[select]);
            PlayerPrefs.SetString("Lock" + select, "opened");
            PlayerPrefs.SetInt("RemoveMoney", PlayerPrefs.GetInt("RemoveMoney") + sales[select]);
            menu.PlaySound(5);
            data.SetColors(select + 1);
            LocksSet();
            buy.SetActive(false);
        }
    }
}