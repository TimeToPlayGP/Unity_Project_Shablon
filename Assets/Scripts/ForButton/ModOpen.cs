using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModOpen : LevelAndModOpen
{
    public bool isAllStars;                         //Показать общее кол-во звезд

    public Text textAllStars;                       //Показать общее кол-во звезд

    public override void StartMetod()
    {
        base.StartMetod();
        gameObject.GetComponent<Button>().onClick.AddListener(Click);

        int a = 0;//Счетчик
        if (isAllStars)         //Если показать, то выводит собранные/общие звезды на каждом моде
        {
            for (int i = 0; i < BaseProfile.CountLevelsInEachMod[NumberLevel - 1]; i++)
            {
                a += BaseProfile.Instance.GetLevels(int.Parse(gameObject.name + (i + 1)));
            }
            if (a < BaseProfile.CountLevelsInEachMod[NumberLevel - 1]) a--;
            textAllStars.text = a + "/" + BaseProfile.CountLevelsInEachMod[NumberLevel - 1];
        }
    }
}
