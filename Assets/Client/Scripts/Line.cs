using System.Collections;
using UnityEngine;

public class Line : MonoBehaviour//Скрипт для линии
{
    public Lines lines;
    private bool isOneTime = true;
    public int id;

    public void StartCorotine()
    {
        StartCoroutine(OneTime());
    }

    private IEnumerator OneTime()//Offset для пореза
    {
        yield return new WaitForSeconds(0.1f);
        isOneTime = true;
    }

    private void OnTriggerEnter(Collider other)//Очко
    {
        if (other.CompareTag("SearchCut") && isOneTime == true)
        {
            isOneTime = false;
            lines.LineGet(id);
        }
    }
}
