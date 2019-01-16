using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [Header("Файл с данными")]
    public TextAsset assetWithDate;

    private string[] DataXML;               //Уровень

    //Текущий уровень
    int NumberLevel
    {
        get;
        set;
    }

    // Use this for initialization
    void Start ()
    {
        NumberLevel = BaseProfile.Instance.CurrentMode + BaseProfile.Instance.CurrentLevel;
        DataXML = GetDateFromXMLFile();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Выйгрыш
    public void Win()
    {
        GamePlay.OnShowWin(3);
    }

    //Проигрыш
    public void Lose()
    {
        GamePlay.OnShowLoose();
    }

    /// <summary>
    /// Возврат данных из файла
    /// </summary>
    /// <returns></returns>
    public string[] GetDateFromXMLFile()
    {
        XMLWork xml = new XMLWork(NumberLevel, assetWithDate);
        return xml.LoadXML();
    }
}
