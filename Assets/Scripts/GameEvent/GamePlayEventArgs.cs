using System;

public class GamePlayEventArgs : EventArgs
{
    //Количество звезд при выйгрыше
    public int? CountStars { get; }

    //Лучший рекорд
    public string BestRecord { get; }
    //Текущий рекорд
    public string CurrentRecord { get; }

    /// <summary>
    /// Контруктор без параметров
    /// </summary>
    public GamePlayEventArgs() : this(null, null, null) { }

    /// <summary>
    /// Конструктор, чтобы показать только звезды, без рекордов
    /// </summary>
    /// <param name="countStars">Кол-во звезд</param>
    public GamePlayEventArgs(int countStars) : this(countStars, null, null) { }

    /// <summary>
    /// Конструктор, чтобы показать только рекорды, без звезд
    /// </summary>
    /// <param name="bestRecord">Лучший рекорд за игру</param>
    /// <param name="currentRecord">Текущий рекорд</param>
    public GamePlayEventArgs(string bestRecord, string currentRecord) : this(null, bestRecord, currentRecord) { }

    /// <summary>
    /// Конструктор, для показа и звезд и рекорда
    /// </summary>
    /// <param name="countStars">Кол-во звезд</param>
    /// <param name="bestRecord">Лучший рекорд за игру</param>
    /// <param name="currentRecord">Текущий рекорд</param>
    public GamePlayEventArgs(int? countStars, string bestRecord, string currentRecord)
    {
        CountStars = countStars;
        BestRecord = bestRecord;
        CurrentRecord = currentRecord;
    }
}
