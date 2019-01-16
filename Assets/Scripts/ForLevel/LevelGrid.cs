using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HorisontalOrVertikal { None, Horisontal, Vertical }         //Прокрутка скролла

public sealed class LevelGrid : LevelSettings
{
    [Header("*****LevelGrid - Script*****")]
    public HorisontalOrVertikal horisontalOrVertikal = HorisontalOrVertikal.None; //Объект Enum

    [Header("Флаги")]

    [Header("Объекты")]
    public ScrollRect ScrollRecrObject;                             //Объект скролла прокрутки
    public GameObject ParentObjectsWithGridLayoutGroup;

    [Header("Переменные")]
    public int CountLevelInPanel = 10;

    private float DistanceBetweenItems;                         //Расстояние между элментами меню
    private GameObject[] ItemsMenu;                                 //Сами моды
    private Vector3 PositionItems;                                  //Стандартная позиция элемента, в частности необходимо - y,z

    protected override int CountPanel
    {
        get
        {
            return BaseProfile.CountLevelsInEachMod[BaseProfile.Instance.CurrentMode - 1] / CountLevelInPanel;
        }
    }

    public override void Start()
    {
        StartCreateAndPosotionPanelWithLevel();                         //Настройка начальной позиции и кол-ва панелей с уровнями

        base.Start();                                                   //Вызов начального метода базового класса

        //Если нет прокрутки
        if (horisontalOrVertikal == HorisontalOrVertikal.None)
        {
            ScrollRecrObject.horizontal = false;
            ScrollRecrObject.vertical = false;
        }//Если прокрутка горизонтальная
        else if (horisontalOrVertikal == HorisontalOrVertikal.Vertical)
        {
            ScrollRecrObject.horizontal = false;
            ScrollRecrObject.vertical = true;
        }//Если прокрутка вертикальная
        else if (horisontalOrVertikal == HorisontalOrVertikal.Horisontal)
        {
            ScrollRecrObject.horizontal = true;
            ScrollRecrObject.vertical = false;
        }

        Destroy(ObjectsWithGridLayoutGroup);
    }

    private void StartCreateAndPosotionPanelWithLevel()
    {
        int _countPanel = CountPanel;

        if (_countPanel != 1) 
        Metod_PositionItemsAndSize(_countPanel);        //Создание, позиция контейнеров

        //Создание уровней в каждом контейнере
        int StartName = 0;
        for (int i = 0; i < ItemsMenu.Length; i++)
        {
            GameObject[] g = new Level(ItemsMenu[i], ExampleLevelPrefab, CountLevelInPanel, (StartName + 1)).GetObjectLevels;

            for (int j = 0; j < g.Length; j++)
            {
                g[j].GetComponent<LevelOpen>().StartMetod();
            }

            StartName += CountLevelInPanel;
        }

        //Удалить лишнии уровни в каждом контейнере
        foreach (var item in ItemsMenu)
        {
            Destroy(item.gameObject.transform.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// Позиция каждого контейнера
    /// </summary>
    /// <param name="count"></param>
    private void Metod_PositionItemsAndSize(int count)
    {
        ItemsMenu = new GameObject[count];        //Создать Массив

        //Создать копии контейнера с уровнями, а после удалить контейнер
        ItemsMenu = new Level(ParentObjectsWithGridLayoutGroup, ObjectsWithGridLayoutGroup, count, 0).GetObjectLevels;


        PositionItems = ObjectsWithGridLayoutGroup.transform.localPosition; //Позиция - 1-ого элемента

        DistanceBetweenItems = Canvas.transform.GetComponent<RectTransform>().sizeDelta.x / 2;//Размер по ширине 

        //Позиция 1-ого элемента
        ItemsMenu[0].transform.localPosition = new Vector3(DistanceBetweenItems, PositionItems.y, PositionItems.z);

        //Каждый элемент становится на свою позицию
        for (int i = 1; i < count; i++)
        {
            ItemsMenu[i].transform.localPosition = new Vector3((i + 1) * DistanceBetweenItems + ((i > 0) ? DistanceBetweenItems * i : 0), PositionItems.y, PositionItems.z);
        }

        //Размер контейнера с элементами меню
        ParentObjectsWithGridLayoutGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(
            DistanceBetweenItems * (count - 1) + DistanceBetweenItems * (count - 1),
            ParentObjectsWithGridLayoutGroup.GetComponent<RectTransform>().sizeDelta.y);
    }
}
