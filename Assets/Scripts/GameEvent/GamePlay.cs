using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Как показывать панель, с анимацией или без
/// </summary>
public enum ShowPanelType
{
    LeanTween,
    None
}

public sealed class GamePlay : MonoBehaviour
{
    [Header("Панель выйгрыша")]
    public GameObject WinPanel;
    public GameObject Stars;                //Звезды
    public Text BestScore;                  //Лучший рекорд
    public Text CurrentRecord;              //Текущий рекорд
    public Sprite StarOn;                   //Спрайт звезд, для активных звезд

    [Header("Панель проигрыша")]
    public GameObject LoosePanel;

    [Header("Панель паузы")]
    public GameObject PausePanel;

    [Header("Настройки показа")]
    [Header("Показ панели с анимацией или без:")]
    public ShowPanelType showPanelType = ShowPanelType.LeanTween;

    public delegate void MyEventHandler<GamePlayEventArgs>(GamePlayEventArgs e);

    //Кол-во звезд при выйгрыше, если=0, то метод с показом звезд не вызывается
    private int countStars { get; set; }

    //Показать панель выйгрыша
    private static event MyEventHandler<GamePlayEventArgs> ShowPanelWin;
    //Показать панель проигрыша
    private static event MyEventHandler<GamePlayEventArgs> ShowPanelLoose;
    //Показать панель паузы
    private static event MyEventHandler<GamePlayEventArgs> ShowPanelPause;

    void Start()
    {
        if (ShowPanelWin == null)
            ShowPanelWin = ShowWinPanel;
        if (ShowPanelLoose == null)
            ShowPanelLoose = ShowLoosePanel;
        if (ShowPanelPause == null)
            ShowPanelPause = ShowPausePanel;
    }

    //Вызов события без показа рекорда и звезд
    public static void OnShowWin() => ShowPanelWin?.Invoke(new GamePlayEventArgs());

    //Вызов события с показом звезд
    public static void OnShowWin(int stars) => ShowPanelWin?.Invoke(new GamePlayEventArgs(stars));

    //Вызов события с показом рекорда
    public static void OnShowWin(string _bestRecord, string _currentRecord) =>
        ShowPanelWin?.Invoke(new GamePlayEventArgs(_bestRecord, _currentRecord));

    //Вызов события с показом рекорда и звезд
    public static void OnShowWin(int stars, string _bestRecord, string _currentRecord) =>
        ShowPanelWin?.Invoke(new GamePlayEventArgs(stars,_bestRecord, _currentRecord));

    //Вызов события показа панели проигрыша
    public static void OnShowLoose() => ShowPanelLoose?.Invoke(new GamePlayEventArgs());

    //Вызов события показа панели паузы
    public static void OnShowPause() => ShowPanelPause?.Invoke(new GamePlayEventArgs());

    /// <summary>
    /// Метод выполняет присваивание переменных для выйгрыша, которые передаем с GamePlayEventArgs,
    /// и активацию панели выйгрыша
    /// </summary>
    /// <param name="e">Параметры, необходимые при показе выйгрыша</param>
    private void ShowWinPanel(GamePlayEventArgs e)
    {
        SetDisEnableObject(e);

        ShowPanelObject(WinPanel, showPanelType);
    }

    /// <summary>
    /// Метод выполняет присваивание переменных для проигрыша, которые передаем с GamePlayEventArgs,
    /// и активацию панели проигрыша
    /// </summary>
    /// <param name="e">Параметры, необходимые при показе проигрыша</param>
    private void ShowLoosePanel(GamePlayEventArgs e)
    {
        //В данном случае необходимости в показе информации для панели проигрыша нет
        //SetDisEnableObject(e);

        ShowPanelObject(LoosePanel, showPanelType);
    }

    /// <summary>
    /// Метод выполняет присваивание переменных для паузы, которые передаем с GamePlayEventArgs,
    /// и активацию панели паузы
    /// </summary>
    /// <param name="e">Параметры, необходимые при показе паузы</param>
    private void ShowPausePanel(GamePlayEventArgs e)
    {
        //В данном случае необходимости в показе информации для панели паузы нет
        //SetDisEnableObject(e);

        ShowPanelObject(PausePanel, showPanelType);
    }

    /// <summary>
    /// Активирует панель либо с анимацией, либо без
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="type"></param>
    private void ShowPanelObject(GameObject panel, ShowPanelType type)
    {
        switch (type)
        {
            case ShowPanelType.LeanTween : LeanTweenObject(panel); break;
            case ShowPanelType.None: panel.transform.localPosition = Vector3.zero;
                if (countStars != 0)
                    showStars(countStars); break;
        }
        panel.SetActive(true);
    }

    /// <summary>
    /// Метод для анимации панели leanObject
    /// С типом анимации setEase
    /// С игнорированием остановки времени в приложении setIgnoreTimeScale
    /// При завершении анимации setOnComplete вызовом метода showStars
    /// </summary>
    /// <param name="leanObject">Панель анимации </param>
    private void LeanTweenObject(GameObject leanObject)
    {
        leanObject.transform.localPosition = new Vector3(0, 500, 0);
        LeanTween.moveLocalY(leanObject, 0, 1)
                .setEase(LeanTweenType.easeOutBounce)
                .setIgnoreTimeScale(true)
                .setOnComplete(() =>
                {
                    if (countStars != 0)
                        showStars(countStars);
                });
    }

    /// <summary>
    /// Показать звезды с задержкой
    /// </summary>
    /// <param name="countStar">Кол-во звезд</param>
    private void showStars(int countStar)
    {
        StartCoroutine(ShowStars(countStar));
    }

    int k = 0;
    /// <summary>
    /// Показать звезды с эффектом ParticleSystem
    /// </summary>
    /// <param name="countStar">Кол-во звезд</param>
    /// <returns></returns>
    IEnumerator ShowStars(int countStar)
    {
        foreach (Transform child in Stars.transform)
        {
            if (countStar > k)
            {
                //Запускать каждые 0.5f секунд
                yield return new WaitForSeconds(0.5f);
                //Звезда теперь активная
                child.GetComponent<Image>().sprite = StarOn;
                //Выполнить эффект
                child.GetChild(0).GetComponent<ParticleSystem>().Play();
                k++;
            }
        }
    }

    /// <summary>
    /// Выставляем значение объектов для панелей
    /// </summary>
    /// <param name="e">Информация для панели</param>
    private void SetDisEnableObject(GamePlayEventArgs e)
    {
        //Кол-во звезд
        if (e.CountStars == null)
            Stars.gameObject.SetActive(false);
        else countStars = (int)e.CountStars;

        //Лучший рекорд
        if (e.BestRecord == null)
            BestScore.gameObject.SetActive(false);
        else BestScore.text = e.BestRecord;

        //Текущий рекорд
        if (e.CurrentRecord == null)
            CurrentRecord.gameObject.SetActive(false);
        else CurrentRecord.text = e.CurrentRecord;
    }
}