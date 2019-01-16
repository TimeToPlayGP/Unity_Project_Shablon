using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void HandlerArgs();                             //вызов методов - показ панелей
public delegate void HandlerArgsStars(int Stars);               //выйгрыш со звездами
public delegate void HandlerArgsRecord(string R1, string R2);   //выйгрыш с рекордом

enum WinType
{
    Win,                    //Показать без LeanTween
    WinStars,               //Показать с звездами
    WinRecord,              //Показать с рекордом
    Loose,                  //Показать с LeanTween
    Pause                   //Показать без LeanTween
}

public class GamePlay : MonoBehaviour
{
    [Header("Панель выйгрыша")]
    public GameObject WinPanel;
    public GameObject Stars;                //Звезды
    public GameObject BestScore;            //Лучший рекорд
    public GameObject LastTry;              //Текущий рекорд
    public Sprite StarOn;                   //Спрайт звезд, которые активны

    [Header("Панель проигрыша")]
    public GameObject LoosePanel;

    [Header("Панель паузы")]
    public GameObject PausePanel;

    private int CountStartsWin = -1;             //Кол-во звезд при выйгрыше

    //Показать панель выйгрыша без параметров
    private static event HandlerArgs OnShowPanelWin;
    //Показать панель проигрыша
    private static event HandlerArgs OnShowPanelLoose;
    //Показать панель паузы
    private static event HandlerArgs OnShowPanelPause;
    //Показать панель выйгрыша со звездами
    private static event HandlerArgsStars OnShowPanelWinStars;
    //Показать панель с рекордом
    private static event HandlerArgsRecord OnShowPanelWinRecord;

    void Start()
    {
        OnShowPanelWin += ShowWinPanel;
        OnShowPanelLoose += ShowLoosePanel;
        OnShowPanelPause += ShowPausePanel;
        OnShowPanelWinStars += ShowWinPanel;
        OnShowPanelWinRecord += ShowWinPanel;
    }

    //Срабатывание события без параметров
    public static void CallHandler(HandlerArgs handler) => handler();
    //Срабатывание события с параметром (int)
    public static void CallHandler(HandlerArgsStars handler, int a) => handler(a);
    //Срабатывание события с параметрами (string, string)
    public static void CallHandler(HandlerArgsRecord handler, string R1, string R2) => handler(R1, R2);

    //Вызов события
    public static void OnShowWin() => CallHandler(OnShowPanelWin);
    //Вызов события
    public static void OnShowWin(int Stars) => CallHandler(OnShowPanelWinStars, Stars);
    //Вызов события
    public static void OnShowWin(string R1, string R2) => CallHandler(OnShowPanelWinRecord, R1, R2);
    //Вызов события
    public static void OnShowLoose() => CallHandler(OnShowPanelLoose);
    //Вызов события
    public static void OnShowPause() => CallHandler(OnShowPanelPause);

    //Вызов метода по событию
    private void ShowWinPanel()
    {
        SetDisEnableObject(Stars, BestScore, LastTry);
        ShowPanelObject(WinPanel, WinType.Win);
    }
    //Вызов метода по событию
    private void ShowWinPanel(int CountStars)
    {
        SetDisEnableObject(BestScore, LastTry);
        CountStartsWin = CountStars;
        ShowPanelObject(WinPanel, WinType.WinStars);
    }
    //Вызов метода по событию
    private void ShowWinPanel(string R1, string R2)
    {
        SetDisEnableObject(Stars);
        BestScore.GetComponent<Text>().text = "Best Score: " + R1;
        LastTry.GetComponent<Text>().text = "Last try: " + R2;
        ShowPanelObject(WinPanel, WinType.WinRecord);
    }
    //Вызов метода по событию
    private void ShowLoosePanel()
    {
        ShowPanelObject(LoosePanel, WinType.Loose);
    }
    //Вызов метода по событию
    private void ShowPausePanel()
    {
        ShowPanelObject(PausePanel, WinType.Loose);
    }

    //Переопределяем метод, если выйгрыш со звездами, то показать анимацию звезд в конце
    private void ShowPanelObject(GameObject panel, WinType type)
    {
        switch (type)
        {
            case WinType.Win:
                WinPanel.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
                break;
            case WinType.WinStars:
                LeanTweenObject(panel);
                break;
            case WinType.WinRecord:
                goto case WinType.WinStars;
                break;
            case WinType.Loose:
                goto case WinType.WinStars;
                break;
            case WinType.Pause:
                break;
            default:
                break;
        }
        panel.SetActive(true);
    }

    private void LeanTweenObject(GameObject o)
    {
            LeanTween.moveLocalY(o.transform.GetChild(0).gameObject, 0, 1)
                .setEase(LeanTweenType.easeOutBounce)
                .setIgnoreTimeScale(true)
                .setOnComplete(() =>
                {
                    if (CountStartsWin != -1)                  //Если метод с показом звезд
                        showStars(CountStartsWin);             //Показать звезды
                });
    }

    private void showStars(int countStar)
    {
        Time.timeScale = 1;
        StartCoroutine(ShowStars(countStar));                  //Показать звезды
    }

    // Показать звезды
    int k = 0;
    IEnumerator ShowStars(int countStar)
    {
        foreach (Transform child in Stars.transform)
        {
            if (countStar > k)
            {
                yield return new WaitForSeconds(0.5f);
                child.GetComponent<Image>().sprite = StarOn;
                foreach (Transform item in child.transform)
                {
                    item.GetComponent<ParticleSystem>().Play();
                }
                k++;
            }
        }
    }

    //Скрывает объекты
    private void SetDisEnableObject(params GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i]?.SetActive(false);
        }
    }
}