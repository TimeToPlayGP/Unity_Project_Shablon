using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour {
    public AudioMixer _music;

    public delegate void A();
    public static A PlaySoundMenu;
    public static A PlaySoundGame;
    public static A PlaySoundClick;

    [Header("Разное")]
    public AudioSource Menu;
    public AudioSource Game;
    public AudioSource Click;

    public static GameObject _sounds;

    void Start()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
            if (Menu.playOnAwake && _sounds.GetComponent<Sounds>().Menu.playOnAwake)
            {
                Destroy(gameObject);
                return;
            }

            else if (!Menu.playOnAwake && _sounds.GetComponent<Sounds>().Menu.playOnAwake)
            {
                Destroy(_sounds);
            }
            else if (Menu.playOnAwake && _sounds.GetComponent<Sounds>().Game.playOnAwake)
            {
                Destroy(_sounds);
            }
            else if (Game.playOnAwake && _sounds.GetComponent<Sounds>().Game.playOnAwake)
            {
                Destroy(_sounds);
            }
        //else Destroy((GameObject)_sounds);
        _sounds = this.gameObject;

        if (BaseProfile.Instance.Muisc < -39) { _music.SetFloat("ParamA", -80); }
        else { _music.SetFloat("ParamA", BaseProfile.Instance.Muisc); }

        if (BaseProfile.Instance.Sounds < -39) { _music.SetFloat("ParamB", -80); }
        else { _music.SetFloat("ParamB", BaseProfile.Instance.Sounds); }

        PlaySoundMenu = playSoundMenu;
        PlaySoundGame = playSoundGame;
        PlaySoundClick = playSoundClick;
        
    }

    private void playSoundMenu()
    {
        Menu.Play();
    }

    private void playSoundGame()
    {
        Game.Play();
    }

    public void playSoundClick()
    {
        Click.Play();
    }

}
