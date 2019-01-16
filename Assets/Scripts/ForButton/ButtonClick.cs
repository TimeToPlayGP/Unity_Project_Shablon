using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ButtonClick : MonoBehaviour {

    //public Select selest = Select.BeginMetod;

    [Header("Строка")]
    public string ValueString;

    [Header("Объект")]
    public GameObject ValueGameobject;

    [Header("Сцена")]
    //public Scenes ValueScenes;

    private Vector3 StartPositionScale;
	// Use this for initialization
	/*void Start ()
    {
        StartPositionScale = transform.localScale;

        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { LeanTween.scale(this.gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0.15f).setLoopPingPong(1).setIgnoreTimeScale(true).setOnComplete(Click); });
        trigger.triggers.Add(entry);
    }

    void Click()
    {
        if (gameObject.transform.localScale.x != 1) gameObject.transform.localScale = Vector3.one;
        GameObject.Find("Sounds").GetComponent<Sounds>().playSoundClick();
        switch ((int)selest)
        {
            case 0:
                if (ValueString == "Next")
                {
                    BaseProfile.Instance.CurrentLevel++;
                    if (BaseProfile.Instance.CurrentLevel > BaseProfile.CountLevelsInEachMod[BaseProfile.Instance.CurrentMode])
                    {
                        BaseProfile.Instance.CurrentMode++;
                        BaseProfile.Instance.CurrentLevel = 1;
                        if (BaseProfile.Instance.CurrentMode > BaseProfile.CountLevelsInEachMod.Length) { BaseProfile.Instance.CurrentMode = 1; Sound(); SceneManager.LoadScene(Scenes.Menu.ToString()); break; }
                        else { Sound(); SceneManager.LoadScene(Scenes.Game.ToString()); break; }
                    }
                    Sound();
                    SceneManager.LoadScene(Scenes.Game.ToString()); break;
                }
                //else if (ValueString == "NArrow") ptSliderMenuScri.nextLevel();
                //else if (ValueString == "BArrow") ptSliderMenuScri.backLevel();
                break;
            case 1: ValueGameobject.SetActive(true); if (ValueGameobject.name == "Pause") Time.timeScale = 0; break;
            case 2: ValueGameobject.SetActive(false); if (ValueGameobject.name == "Pause") Time.timeScale = 1; break;
            case 3:
                if (ValueScenes == Scenes.LevelGrid || ValueScenes == Scenes.LevelList)
                {
                    if (BaseProfile.Instance.FirstStart == 0)
                    {
                        BaseProfile.Instance.FirstStart = 1;
                        Sound();
                        SceneManager.LoadScene(Scenes.Game.ToString());
                    }
                    else
                    {
                        Sound();
                        SceneManager.LoadScene(ValueScenes.ToString());
                    }
                }
                else
                {
                    Sound();
                    SceneManager.LoadScene(ValueScenes.ToString());
                }
                break;
        }
    }

    void Sound()
    {
        GameObject.Find("Sounds").GetComponent<Sounds>().playSoundClick();
    }*/
}
