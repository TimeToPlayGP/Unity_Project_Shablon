using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelOpen : LevelAndModOpen
{
    [Header("*****LevelOpen - Script*****")]
    [Header("Флаги")]
    public bool isVisibleStars;                     //Показать звезды?
    public bool isVisibleRecord;                    //Показать рекорд?

    [Header("Объекты")]
    public GameObject[] Stars;                      //Массив звезд
    public GameObject Record;                       //Рекорд
    public GameObject Lock;                         //Замок - закрытый уровень

    public Sprite StarsOn;
    public Sprite StarsOff;

    public override void StartMetod()
    {
        base.StartMetod();

        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].SetActive(isVisibleStars);
        }

        SetSettings();
    }

    private void SetSettings()
    {
        //Номер уровня - мод+уровень
        int getLevel = BaseProfile.Instance.GetLevels(int.Parse(BaseProfile.Instance.CurrentMode.ToString() + NumberLevel));

        if (getLevel == 0)//Уровень не открыт
        {
            Lock.SetActive(true);                           //Открыть замок
            Record.SetActive(false);                        //Скрыть рекорд
            TextName.SetActive(false);                      //Скрыть номер уровня
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i].SetActive(false);                  //Скрыть звезды
            }
        }
        else//Уровень открыт
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Click);

            Lock.SetActive(false);                          //Скрыть замок
            Record.SetActive(isVisibleRecord);              //Если уровень isVisibleRecord

            int numberStar = BaseProfile.Instance.GetLevelStars(NumberLevel);//Проверяем сколько звезд у уровня
            if (isVisibleStars)
            {
                for (int i = 0; i < Stars.Length; i++)
                {
                    Stars[i].GetComponent<Image>().sprite = numberStar > i ? StarsOn : StarsOff;
                }
            }
        }
    }
}
