using UnityEngine;
using System.Collections;

public class BaseProfile
{
    //Кол-во уровней в каждом моде, если мод=1 - удалить остальные
    public static int[] CountLevelsInEachMod = { 20, 30, 40, 50, 60 };

    //Уровни, которые должны быть открытыми 11- №мод+№уровня
    private string[] _opensLevel = { "11", "21", "31", "41", "51", "61", "", "", "" };

    private static BaseProfile _baseProfile;

    public static BaseProfile Instance
    {
        get
        {
            if (_baseProfile != null) return _baseProfile;
            else return (_baseProfile = new BaseProfile());
        }
    }

    //Первый запуск
    public int FirstStart
    {
        get { return PlayerPrefs.GetInt("FirstStart", 0); }
        set { PlayerPrefs.SetInt("FirstStart", value); }
    }

    //Текущий № режима
    public int CurrentMode
    {
        get { return PlayerPrefs.GetInt("CurrentMode", 1); }
        set { PlayerPrefs.SetInt("CurrentMode", value); }
    }

    //Текущий № уровня
    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt("CurrentLevel", 1); }
        set { PlayerPrefs.SetInt("CurrentLevel", value); }
    }

    //0-Закрыт уровень
    //1-Открыт уровень
    public int GetLevels(int NumberLevel)
    {
        if (_opensLevel.Contains(NumberLevel.ToString())) return 1;
        return PlayerPrefs.GetInt("Level" + NumberLevel, 0);
    }
    //Метод открытия уровня
    public void SetOpenLevels(int NumberLevel)
    {
        PlayerPrefs.SetInt("Level" + NumberLevel, 1);
    }

    //Кол-во звезд у уровня
    //0-Нет звезд
    //1-1 Звезда
    //2-2 Звезды
    //3-3 Звезды
    public int GetLevelStars(int NumberLevel)
    {
        return PlayerPrefs.GetInt("LevelStars" + NumberLevel, 0);
    }

    public void SetLevelStars(int NumberLevel, int ValueLevelStars)
    {
        PlayerPrefs.SetInt("LevelStars" + NumberLevel, ValueLevelStars);
    }

    //Звук
    public float Sounds
    {
        get { return PlayerPrefs.GetFloat("Sounds", 0); }
        set { PlayerPrefs.SetFloat("Sounds", value); }
    }
    //Музыка
    public float Muisc
    {
        get { return PlayerPrefs.GetFloat("Muisc", -20f); }
        set { PlayerPrefs.SetFloat("Muisc", value); }
    }

    //Рекорд уровня
    public void SetLevelRecord(int NumberLevel, string Record)
    {
        PlayerPrefs.SetString("LevelRecord" + NumberLevel, Record);
    }

    public string GetLevelRecord(int NumberLevel)
    {
        return PlayerPrefs.GetString("LevelRecord" + NumberLevel, "");
    }

    //Текущее кол-во очков
    public void SetLevelScore(int NumberLevel, string Score)
    {
        PlayerPrefs.SetString("LevelScore" + NumberLevel, Score);
    }

    public string GetLevelScore(int NumberLevel)
    {
        return PlayerPrefs.GetString("LevelScore" + NumberLevel, "");
    }

}

//Класс для создания метода расширения
//Проверка, имеется ли в массиве строк str - строка s
public static class StringExtension
{
    public static bool Contains(this string[] str, string s)        //Метод расширения
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == s) return true;
        }
        return false;
    }
}
