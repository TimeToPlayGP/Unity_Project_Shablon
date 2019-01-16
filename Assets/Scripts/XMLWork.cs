using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Xml.Linq;

public class XMLWork : MonoBehaviour
{
    private TextAsset TextAssetForDate;     //Файл с данными

    int CurrentLevel                        //Текущий уровень
    {
        get;
        set;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="currentLevel">Текущий уровень</param>
    /// <param name="textAsset">Файл с данными</param>
    public XMLWork(int currentLevel, TextAsset textAsset)
    {
        CurrentLevel = currentLevel;
        TextAssetForDate = textAsset;
    }

    //Возврат значений
    public string[] LoadXML()
    {

        var doc = new XmlDocument();
        doc.Load(new StringReader(TextAssetForDate.text));

        // меняем атрибут
        XmlNodeList adds = doc.GetElementsByTagName("Level" + CurrentLevel);

        string[] _holder = new string[adds.Count];
        int j = 0;
        foreach (XmlNode add in adds)
        {
            if (add.Attributes["Name"].Value == "Row" + (j + 1))
            {
                _holder[j] = add.Attributes["name"].Value;
            }
            j++;
        }
        return _holder;
    }
}
