using UnityEngine;

public class Coin : MonoBehaviour//Скрипт для объекта монеты
{
    public Menu menu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SearchCut"))
        {
            menu.PlusMoney();//Команда добавления монет
            Destroy(gameObject);//Удалить объект
        }
    }
}