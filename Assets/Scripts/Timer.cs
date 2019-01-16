using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public enum Times {Timer, Stopwatch }//Выбор таймера или секундомера

public class Timer : MonoBehaviour
{
    public delegate void EndGames();
    public static EndGames end;             //Делегат на заверщение секундомера

    public delegate string GetTimeValue();
    public static GetTimeValue getTime;    //Получить текущее время

    private int timersecond; // проверка отчёта секунд в Inspector
    private float secondgametime; //(?) - нужно для работы Time.deltaTime
    private int minedgametime;// проверка отчёта минут в Inspector

    static bool isTimer;
    public GameObject Timers;     //Объект с Таймером
    public GameObject End;        //Конечный объект, на котором должно проиойти событие когда таймер закончится
    public Text TextTimer;        //Текст таймера
    public Text Level;

    public Times ValueTimer = Times.Timer; //Выбор таймера или секундомера

    public void Start()
    {
        if (Level != null)                                      //Показ уровня
            Level.text += BaseProfile.Instance.CurrentLevel;
        getTime = GetTime;
        end = EndGame;
        Starts(0);
    }

    public static void StartTimer()
    {
        isTimer = false;
    }

    /// <summary>
    /// Преобразование секунд в минуты, если секунды(timeSecond) превышают значение 60
    /// </summary>
    /// <param name="timeSecond">Полученные секунды</param>
    public void Starts(int timeSecond)
    {
        if (timeSecond > 60)                                    //
        {
            minedgametime = timeSecond / 60;
            timersecond = timeSecond - minedgametime * 60;
        }
        else
        {
            timersecond = timeSecond;
        }
        TextTimer.text = String.Format("{0}:{1}", minedgametime.ToString("00"), timersecond.ToString("00"));

        if (Timers != null)             //Показать таймер
            Timers.SetActive(true);
        isTimer = true;
    }

    /// <summary>
    /// Считаем таймер или секундомер
    /// </summary>
    void Update()
    {
        if (!isTimer) return;

        secondgametime += Time.deltaTime;

        if (secondgametime >= 1)
        {
            if (ValueTimer == Times.Timer)             //Таймер
            {
                if (timersecond == 0 && minedgametime == 0)
                {
                    isTimer = false;
                    /*End.GetComponent<LogicGame>().RunWatter(); //Выполнение действия, когда таймер закончился*/
                    return;
                }
                else if (timersecond == 0)
                {
                    minedgametime--;
                    timersecond = 60;
                }
                timersecond -= 1;
                secondgametime = 0;

                TextTimer.text = String.Format("{0}:{1}", minedgametime.ToString("00"), timersecond.ToString("00"));
            }
            else if (ValueTimer == Times.Stopwatch)     //Секундомер
            {
                if (timersecond == 59)
                {
                    minedgametime++;
                    timersecond = -1;
                }
                timersecond += 1;
                secondgametime = 0;
                TextTimer.text = String.Format("{0}:{1}", minedgametime.ToString("00"), timersecond.ToString("00"));
            }
        }
    }

    /// <summary>
    /// Проверка в конце игры рекорда, и зыпись нового рекорда, если он лучше предыдущего
    /// </summary>
    void EndGame()
    {
        int StartTime = 1000000;
        //Получаем старый рекорд и преобразуем строку (кол-во минут и секунд) в число
        string StartRecord = BaseProfile.Instance.GetLevelRecord(int.Parse(BaseProfile.Instance.CurrentMode.ToString() + BaseProfile.Instance.CurrentLevel));
        if (StartRecord.Length > 4)
        {
            int StartMinut = int.Parse(StartRecord.Substring(0, 2));
            int StartSecond = int.Parse(StartRecord.Substring(3, 2));
            StartTime = StartMinut * 60 + StartSecond;
        }
        int EndTime = minedgametime * 60 + timersecond;
        //Если новый рекорд не меньше старого, то возвращаем
        if (EndTime > StartTime) return;

        //Записываем новый рекорд
        string time = "";
        string _minedgametime = minedgametime.ToString();
        string _timersecond = timersecond.ToString();
        if (_minedgametime.Length == 1) _minedgametime = "0" + _minedgametime;
        if (_timersecond.Length == 1) _timersecond = "0" + _timersecond;
        time = _minedgametime + ":" + _timersecond;
        BaseProfile.Instance.SetLevelRecord(int.Parse(BaseProfile.Instance.CurrentMode.ToString() + BaseProfile.Instance.CurrentLevel), time);
    }

    /// <summary>
    /// Возврат текущего времени
    /// </summary>
    /// <returns></returns>
    string GetTime()
    {
        string time = "";
        string _minedgametime = minedgametime.ToString();
        string _timersecond = timersecond.ToString();
        if (_minedgametime.Length == 1) _minedgametime = "0" + _minedgametime;
        if (_timersecond.Length == 1) _timersecond = "0" + _timersecond;
        time = _minedgametime + ":" + _timersecond;
        return time;
    }
}
