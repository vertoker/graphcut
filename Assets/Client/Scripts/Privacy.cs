using UnityEngine;

public class Privacy : MonoBehaviour
{
    private const string BlockString = "< : >";
    private const int LengthPassword = 20;
    private string[] dict = new string[]
    {
        "0", "A", "B", "C", "D", "1", "E", "F", "G", "H",
        "2", "I", "J", "K", "L", "3", "M", "N", "O", "P",
        "4", "Q", "R", "S", "T", "5", "U", "V", "W", "X",
        "6", "Y", "Z", "a", "b", "7", "c", "d", "e", "f",
        "8", "g", "h", "i", "j", "9", "k", "l", "m", "n",
        ".", "o", "p", "r", "?", ",", "s", "t", "u", "|",
        "<", "v", "w", "x", "!", ">", "y", "z", "+", "-",
        "_", "А", "а", "Б", "б", "=", "В", "в", "Г", "г",
        "(", "Д", "д", "Е", "е", ")", "Ж", "ж", "З", "з",
        "{", "И", "и", "Й", "й", "}", "К", "к", "Л", "л",
        "[", "М", "м", "Н", "н", "]", "О", "о", "П", "п",
        "*", "Р", "р", "С", "с", "/", "Т", "т", "У", "у",
        "&", "Ф", "ф", "Х", "х", ":", "Ц", "ц", "Ч", "ч",
        "^", "Ш", "ш", "Щ", "щ", "%", "Ь", "ь", "Ы", "ы",
        "$", "Ъ", "ъ", "Э", "э", ";", "Ю", "ю", "Я", "я",
        "@", "Ё", "ё", "№", "~"
    };
    
    public void DeletePassword()
    {
        for (int i = 0; i < LengthPassword; i++)
        {
            PlayerPrefs.DeleteKey("ID" + (i + 1));
        }
    }
    public void CreatePassword()
    {
        int[] r = new int[LengthPassword];
        for (int i = 0; i < LengthPassword; i++)
        {
            r[i] = Random.Range(0, dict.Length - 1);
        }
        for (int i = 0; i < LengthPassword; i++)
        {
            PlayerPrefs.SetInt("ID" + (i + 1), r[i]);
        }
    }
    public string Crypt(int count)
    {
        string p = GetPassword();
        string c = "";
        string p1 = "";
        string p2 = "";
        for (int i = 0; i < count.ToString().Length; i++)
        {
            p1 = p1 + p.Substring(i, 1);
            p2 = p2 + p.Substring(p.Length - 1 - i, 1);
        }
        c = VishenerCrypt(count.ToString(), p2, false);
        c = VishenerCrypt(c, p1, false);
        return c;
    }
    public int EnCrypt(string cryptText)
    {
        string p = GetPassword();
        string c = "";
        string p1 = "";
        string p2 = "";
        for (int i = 0; i < cryptText.Length; i++)
        {
            p1 = p1 + p.Substring(i, 1);
            p2 = p2 + p.Substring(p.Length - 1 - i, 1);
        }
        c = VishenerCrypt(cryptText, p1, true);
        if (c == BlockString) { return -1; }
        c = VishenerCrypt(c, p2, true);
        if (c == BlockString) { return -1; }
        return int.Parse(c);
    }

    private string GetPassword()
    {
        string p = "";
        for (int i = 0; i < LengthPassword; i++)
        {
            p = p + dict[PlayerPrefs.GetInt("ID" + (i + 1))];
        }
        return p;
    }
    private int SearchDict(string cont)
    {
        for (int i = 0; i < dict.Length; i++)
        {
            if (dict[i] == cont)
            {
                return i;
            }
        }
        return -1;
    }
    private int Stabilization(int w)
    {
        while (w < 0 || w >= dict.Length)
        {
            if (w >= dict.Length) { w = w - dict.Length; }
            else if (w < 0) { w = w + dict.Length; }
        }
        return w;
    }
    private string VishenerCrypt(string text, string password, bool isEncrypt)
    {
        string ct = "";
        for (int i = 0; i < text.Length; i++)
        {
            int w1 = SearchDict(text.Substring(i, 1));
            if (w1 == -1) { return BlockString; }
            int w2 = SearchDict(password.Substring(i, 1));
            if (w2 == -1) { return BlockString; }
            int w = 0;
            if (!isEncrypt) { w = Stabilization(w1 + w2); }
            else { w = Stabilization(w1 - w2); }
            ct = ct + dict[w];
        }
        return ct;
    }
}