using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class LevelList : LevelSettings
{
    [Header("*****LevelList - Script*****")]
    [Header("Объекты")]
    public ScrollRect ScrollR;     //ScrollRect текущий

    [Header("Vector3")]
    public Vector3 ScaleItemsSelection = Vector3.one;                     //Размер выбранного элемента
    public Vector3 ScaleItemsNextAndBack = new Vector3(0.8f, 0.8f, 0.8f); //Размер следующего и предыдущего элемента меню
    public Vector3 PositionItems = new Vector3(0, -150, 0);               //Стандартная позиция элемента

    [Header("Переменные")]
    public float PositionSelectionItem = -207;                              //Позиция на Y выбранного элемента меню выше
    public float DistanceBetweenItems = 0;                                  //Расстояние между элментами меню
    public float MinimalDistanceItems = 200;//200,Минимальная "скорость" при прокрутке, чтобы приблизить выбранный эле-нт

    private Transform[] ItemsMenu;                                          //Сами моды
    private float VelosityScroll;                                           //Скорость Scroll
    private static int curentLevel;                                         //Текущий эл-нт меню
    private bool flagIsScroll = false;

    protected override int CountPanel
    {
        get
        {
            return BaseProfile.CountLevelsInEachMod.Length;
        }
    }


    public override void Start()
    {
        //Создать моды
        GameObject[] l = new Level(ObjectsWithGridLayoutGroup, ExampleLevelPrefab, CountPanel, 1).GetObjectLevels;

        Destroy(ExampleLevelPrefab);                                //Уничтожить префаб

        ItemsMenu = new Transform[CountPanel];
        for (int i = 0; i < CountPanel; i++)
        {
            l[i].GetComponent<ModOpen>().StartMetod();
            ItemsMenu[i] = l[i].transform;                          //Получить transform модов
        }

        StartPositionItems();

        //Вызов метода при прокрутки
        ScrollbarHorizontal.GetComponent<Scrollbar>().onValueChanged.AddListener(delegate { ScrollItemsMenu(); });

        base.Start();

        LeanTween.value(gameObject, updateValueExampleCallback, ScrollbarHorizontal.value, (BaseProfile.Instance.CurrentMode - 1) / (CountPanel - 1f), 0.5f).setEase(LeanTweenType.easeInOutSine);//Выбрать положение элемента
    }

    void StartPositionItems()
    {
        //Первый элемент выше остальных
        ItemsMenu[0].localPosition = new Vector3(
            ScrollR.GetComponent<RectTransform>().sizeDelta.x / 2,
            PositionSelectionItem,
            PositionItems.z);

        //Первый элемент размера ScaleItemsSelection
        ItemsMenu[0].localScale = ScaleItemsSelection;

        //Каждый элемент становится на свою позицию
        for (int i = 1; i < CountPanel; i++)
        {
            ItemsMenu[i].localPosition = new Vector3(
                ItemsMenu[i - 1].localPosition.x + ItemsMenu[i - 1].GetComponent<RectTransform>().sizeDelta.x + DistanceBetweenItems,
                PositionItems.y,
                PositionItems.z);
            //Кроме первого, меньше размера, i=1
            ItemsMenu[i].localScale = ScaleItemsNextAndBack;
        }

        //Размер контейнера с модами
        ObjectsWithGridLayoutGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(
            ExampleLevelPrefab.GetComponent<RectTransform>().sizeDelta.x * (CountPanel - 1) + DistanceBetweenItems * (CountPanel - 1),
            ObjectsWithGridLayoutGroup.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void ScrollItemsMenu()   //Вызов метода при движении ScrollBar
    {
        VelosityScroll = ScrollR.velocity.magnitude;

        curentLevel = int.Parse(Mathf.Round(ScrollbarHorizontal.value * (CountPanel - 1)).ToString()); //Номер элемента меню

        float value = ScrollbarHorizontal.value * (CountPanel - 1);                                    //Позиция слайдера над элементом

        //Перед и после элемента стандартный размер
        ItemsMenu[(curentLevel - 1) < 0 ? (curentLevel + 1) : (curentLevel - 1)].localScale = ScaleItemsNextAndBack;
        ItemsMenu[(curentLevel + 1) >= CountPanel ? (curentLevel - 1) : (curentLevel + 1)].localScale = ScaleItemsNextAndBack;

        //Перед и после элемента стандартная позиция
        ItemsMenu[(curentLevel - 1) < 0 ? (curentLevel + 1) : (curentLevel - 1)].localPosition = new Vector3(ItemsMenu[(curentLevel - 1) < 0 ? (curentLevel + 1) : (curentLevel - 1)].localPosition.x, PositionItems.y, PositionItems.z);
        ItemsMenu[(curentLevel + 1) >= CountPanel ? (curentLevel - 1) : (curentLevel + 1)].localPosition = new Vector3(ItemsMenu[(curentLevel + 1) >= CountPanel ? (curentLevel - 1) : (curentLevel + 1)].localPosition.x, PositionItems.y, PositionItems.z);

        //Текущий элемент изменить в размере - увеличить
        float CalculateScale = Mathf.Abs(value - curentLevel) * ((ScaleItemsSelection.x - ScaleItemsNextAndBack.x) * 2);
        ItemsMenu[curentLevel].localScale = new Vector3(ScaleItemsSelection.x - CalculateScale, ScaleItemsSelection.x - CalculateScale, ScaleItemsSelection.z);

        //Текущий элемент изменить позицию - поднять выше
        ItemsMenu[curentLevel].localPosition = new Vector3(ItemsMenu[curentLevel].localPosition.x, PositionSelectionItem - ((Mathf.Abs(PositionItems.y) - Mathf.Abs(PositionSelectionItem)) * Mathf.Abs(value - curentLevel) * 2), PositionItems.z);

        if (VelosityScroll < MinimalDistanceItems && flagIsScroll) //Если меньше значения, то выровнять текущий элемент
        {
            flagIsScroll = false;//Не перемещается Scroll больше
            LeanTween.value(gameObject, updateValueExampleCallback, ScrollbarHorizontal.value, curentLevel / (CountPanel - 1f), 0.5f).setEase(LeanTweenType.easeInOutSine);//Выбрать положение элемента
        }

        if (isVisibleUICurrentLevel) SpriteOff(curentLevel);            //Точка снизу Активная
    }

    void updateValueExampleCallback(float value) //Изменить Scroll
    {
        ScrollbarHorizontal.value = value;
    }

    // Update is called once per frame
    public static bool isTouch = false;      //Нажали ли?
    void FixedUpdate()
    {
        if (Input.anyKey && !isTouch)
        { isTouch = true; VelosityScroll = ScrollR.velocity.magnitude; LeanTween.cancel(gameObject); } //Если нажали, отменить все анимации
        if (flagIsScroll && Input.anyKey)
            flagIsScroll = false;                  //Проверка на нажатие и изменить flagIsScroll
        //Если изменялся Scroll и начинать замедляться, то разрешить анимацию LeanTween
        if (!flagIsScroll && !Input.anyKey && VelosityScroll > MinimalDistanceItems)
        { flagIsScroll = true; isTouch = false; }
        else if (!flagIsScroll && !Input.anyKey && isTouch) //Если не было "Быстрой" прокрутки
        {
            isTouch = false;
            curentLevel = int.Parse(Mathf.Round(ScrollbarHorizontal.value * (CountPanel - 1)).ToString()); //Номер элемента меню
            LeanTween.value(gameObject, updateValueExampleCallback, ScrollbarHorizontal.value, curentLevel / (CountPanel - 1f), 0.5f).setEase(LeanTweenType.easeInOutSine);
        }
    }


}
