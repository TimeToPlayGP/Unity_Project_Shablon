using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelSettings : MonoBehaviour
{
    [Header("Объекты")]
    public GameObject ObjectsWithGridLayoutGroup;       //Объект с компонентом "GridLayoutGroup"
    public Scrollbar ScrollbarHorizontal;
    public GameObject ExampleLevelPrefab;               //Пример уровня
    public GameObject Canvas;                           //Канвас с объектами
    public GameObject UICurrentLevel;                   //Снизу точки (уровни)
    public Sprite UICurrentLevelOn;                     //Снизу точки (уровни) - активны
    public Sprite UICurrentLevelOff;                    //Снизу точки (уровни) - не активны

    [Header("Кнопки прокрутки")]
    [Header("[Кнопки-Стрелки]")]
    public Button LeftButtonScrolling;                  //Кнопка-Стрелка левая
    public Button RigthBurronScrolling;                 //Кнопка-Стрелка правая
    [Header("[Кнопки-Полосы]")]
    public Button ScrollBarLeft;                        //Левая полоса прокрутки
    public Button ScrollBarRigth;                       //Правая полоса прокрутки


    [Header("Флаги")]
    [Header("[Точки-подсказки внизу]")]
    public bool isVisibleUICurrentLevel;                         //Снизу точки под модами, активные и нет
    [Header("[Кнопки-Стрелки левая-правая]")]
    public bool isActiveButtonScrolling;                         //Активны левая и правая кнопки прокрутки?
    [Header("[Полоса прокрутки внизу]")]
    public bool isActiveImageScroll;                             //Активна полоса прокрутки?
    [Header("[Полосы по бокам, как кнопки]")]
    public bool isAvtiveScrollBarLeftAndRigth;                   //Активны полосы прокрутки слева и справа?

    private Image[] UICurrentLevelMassiv;                        //Массив точек снизу
    private int curentLevelSelected;                             //Текущий выбранный уровень(мод)

    /*
    Свойства
    */
    protected abstract int CountPanel { get; }             //Кол-во панелей (в LevelGrid - панель с уровнями, в LevelList - моды)


    public virtual void Start()
    {
        //Видимость нижней полосы прокрутки
        IsActiveImageScroll(isActiveImageScroll);

        //Активность кнопок по бокам стрелок левая-правая
        IsActiveButtonScrolling(isActiveButtonScrolling);

        //Активность полос на экране слева и справа, как и кнопки стрелки
        IsAvtiveScrollBarLeftAndRigth(isAvtiveScrollBarLeftAndRigth);

        //Создание точек снизу
        CreateUILevelPoints(isVisibleUICurrentLevel);
    }

    private void ClickButtonLeft()
    {
        if (ClickButton()) return;
        if (curentLevelSelected - 1 < 0)//Если елементов слева нет, то выбираем последний элемент
        {
            LeanLevel(1);         //Перемещение Скролла
            if (isVisibleUICurrentLevel) SpriteOff(CountPanel - 1);     //Точка снизу Активная
            return;
        }
        LeanLevel(--curentLevelSelected / (CountPanel - 1f));           //Перемещение Скролла
        if (isVisibleUICurrentLevel) SpriteOff(curentLevelSelected);    //Точка снизу Активная
    }

    private void ClickButtonRigth()
    {
        if (ClickButton()) return;
        if (curentLevelSelected + 1 > CountPanel - 1) //Если елементов справа нет, то выбираем 1 элемент
        {  
            LeanLevel(0);                                               //Перемещение Скролла
            if (isVisibleUICurrentLevel) SpriteOff(0);                  //Точка снизу Активная
            return;
        }
        LeanLevel(++curentLevelSelected / (CountPanel - 1f));           //Перемещение Скролла
        if (isVisibleUICurrentLevel) SpriteOff(curentLevelSelected);    //Точка снизу Активная
    }

    //Нажали на кнопку
    private bool ClickButton()
    {
        LevelList.isTouch = false;
        if (CountPanel == 1) return true;
        curentLevelSelected = Mathf.RoundToInt(ScrollbarHorizontal.value * (CountPanel - 1));//Подсчет позиции скролла
        return false;
    }

    //Перемещение на другую панель
    private void LeanLevel(float NumberLevel)
    {
        LeanTween.value(gameObject, updateValueExampleCallback, ScrollbarHorizontal.value,
                NumberLevel, 0.5f)
                .setEase(LeanTweenType.easeInOutSine);//Выбрать положение элемента
    }

    private void updateValueExampleCallback(float value) //Изменить Scroll
    {
        ScrollbarHorizontal.value = value;
    }

    //Скрытие всех точек снизу и Активность одной выбранной
    protected void SpriteOff(int CurentOn)
    {
        for (int i = 0; i < UICurrentLevelMassiv.Length; i++)
        {
            UICurrentLevelMassiv[i].sprite = UICurrentLevelOff;
        }
        UICurrentLevelMassiv[CurentOn].sprite = UICurrentLevelOn;
    }

    /// <summary>
    /// Видимость нижней полосы прокрутки
    /// </summary>
    /// <param name="flags"></param>
    private void IsActiveImageScroll(bool flags)
    {
        ScrollbarHorizontal.gameObject.GetComponent<Image>().enabled = flags;
        ScrollbarHorizontal.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().enabled = flags;
    }

    /// <summary>
    /// Активность кнопок стрелок левая-правая
    /// </summary>
    /// <param name="flag"></param>
    private void IsActiveButtonScrolling(bool flags)
    {
        LeftButtonScrolling.gameObject.SetActive(flags);
        RigthBurronScrolling.gameObject.SetActive(flags);

        if (flags)
        {
            LeftButtonScrolling.onClick.AddListener(ClickButtonLeft);                 //Нажата левая кнопка прокрутки
            RigthBurronScrolling.onClick.AddListener(ClickButtonRigth);               //Нажата правая кнопка прокрутки
        }
    }

    /// <summary>
    /// Активность полос экрана левая правая
    /// </summary>
    /// <param name="flags"></param>
    private void IsAvtiveScrollBarLeftAndRigth(bool flags)
    {
        ScrollBarLeft.gameObject.SetActive(flags);            //Левая полоса прокрутки
        ScrollBarRigth.gameObject.SetActive(flags);           //Правая полоса прокрутки

        //Если активны полосы прокрутки, то срабатывает метод как и кнопок прокрутки
        if (flags)
        {
            ScrollBarLeft.onClick.AddListener(ClickButtonLeft);                       //Нажата левая полоса прокрутки
            ScrollBarRigth.onClick.AddListener(ClickButtonRigth);                     //Нажата правая полоса прокрутки
        }
    }

    /// <summary>
    /// Создание точек снизу
    /// </summary>
    private void CreateUILevelPoints(bool flags)
    {
        UICurrentLevel.transform.parent.gameObject.SetActive(flags);            //Видимость точек

        if (!flags) return;                                                     //Если не активна, завершить

        GameObject[] g =                                                        //Создание точек
            new Level(UICurrentLevel.transform.parent.gameObject, UICurrentLevel, CountPanel - 1, 2).GetObjectLevels;

        UICurrentLevelMassiv = new Image[CountPanel];
        UICurrentLevelMassiv[0] = UICurrentLevel.GetComponent<Image>();
        UICurrentLevelMassiv[0].sprite = UICurrentLevelOn;

        for (int i = 0; i < g.Length; i++)                                      //Все точки в массив
        {
            UICurrentLevelMassiv[i + 1] = g[i].GetComponent<Image>();
        }
    }
}

class Level
{
    private readonly GameObject[] _Levels;
                //Родитель объекта          //Объект                //Кол-во копий          //Имя инкремент
    public Level(GameObject ParentForLevel, GameObject LevelPrafab, int CountLevelOnPanel, int StartLevelStart)
    {
        _Levels = new GameObject[CountLevelOnPanel];

        for (int i = 0; i < CountLevelOnPanel; i++)
        {
            GameObject g = GameObject.Instantiate(LevelPrafab);
            g.transform.SetParent(ParentForLevel.transform);           
            g.transform.localScale = Vector3.one;                                       //Начальный размер
            g.transform.localPosition = Vector3.zero;                                   //Начальная позиция
            g.name = StartLevelStart + i + "";                                          //Имя

            _Levels[i] = g;
        }
    }

    public GameObject[] GetObjectLevels
    {
        get { return _Levels; }
    }
}