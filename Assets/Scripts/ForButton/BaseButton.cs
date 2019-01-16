using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour, IClick
{

    public OptionActionValue OptionAction;

    public GameObject VisibleObject;

    public GameObject HideObject;

    public SceneValue OpenScene = SceneValue.Game;

    //Какую музыку воспрозвести для данной кнопки?
    public PlaySoundButton playSound;

    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PlaySound);
        gameObject.GetComponent<Button>().onClick.AddListener(Click);
    }

    public virtual void Click()
    {
        switch (OptionAction)
        {
            case OptionActionValue.BeginMethod:break;

            case OptionActionValue.VisibleObject:
                VisibleObject.SetActive(true); break;

            case OptionActionValue.HideObject:
                HideObject.SetActive(false); break;

            case OptionActionValue.OpenScene:
                SceneManager.LoadScene(OpenScene.ToString()); break;
            default:
                break;
        }
    }

    public void PlaySound()
    {
        Debug.Log("Sound");
        switch (playSound)
        {
            case PlaySoundButton.playSoundClickMenu:
                break;
            case PlaySoundButton.playSoundClickGame:
                break;
            default:
                break;
        }
        //Изменить
        //GameObject.Find("Sounds").GetComponent<Sounds>().playSoundClick();
    }
}
