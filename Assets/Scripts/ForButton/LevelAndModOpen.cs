using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

interface OnLevelAndButton
{
    void Click();
    void LoadScene();
}


public abstract class LevelAndModOpen : MonoBehaviour, OnLevelAndButton
{
    //public Scenes ValueScenes;              //Какую сцену загрузить при клике
        
    public GameObject TextName;             //Текст с номером мода/уровня

    protected int NumberLevel { get; set; }    //Номер уровня/мода

    // Use this for initialization
    public virtual void StartMetod()
    {
        NumberLevel = int.Parse(gameObject.name);
        TextName.GetComponent<Text>().text = NumberLevel.ToString();
    }

    public void Click()
    {
        float _scale = gameObject.transform.localScale.x - 0.2f;
        LeanTween.scale(gameObject, new Vector3(_scale, _scale, _scale), 0.15f).setLoopPingPong(1).setOnComplete(LoadScene);
    }
    public void LoadScene()
    {
        BaseProfile.Instance.CurrentLevel = NumberLevel;                        //Запомнить выбранный уровень
        GameObject.Find("Sounds").GetComponent<Sounds>().playSoundClick();
        //SceneManager.LoadScene(ValueScenes.ToString());                         //Открыть сцену
    }
}
