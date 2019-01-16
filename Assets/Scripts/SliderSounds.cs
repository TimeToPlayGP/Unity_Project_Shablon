using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public enum SliderVibor { Sound, Music }

public class SliderSounds : MonoBehaviour
{
    public SliderVibor _slider = SliderVibor.Sound;

    public AudioMixer _music;

    private Slider sliderValue;
    private Image image;

    [Header("Спрайты Вкл/Откл")]
    public Sprite SpriteOn;
    public Sprite SpriteOff;

    // Use this for initialization
    void Start()
    {
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { Click(); });
        trigger.triggers.Add(entry);

        image = GetComponent<Image>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Slider>())
            {
                sliderValue = child.GetComponent<Slider>();
            }

        }

        if (_slider == SliderVibor.Music)
        {
            sliderValue.value = BaseProfile.Instance.Muisc;
            if (BaseProfile.Instance.Muisc < -39) { image.sprite = SpriteOff; _music.SetFloat("ParamA", -80); }
            else { image.sprite = SpriteOn; _music.SetFloat("ParamA", BaseProfile.Instance.Muisc); }
        }
        else
        {
            sliderValue.value = BaseProfile.Instance.Sounds;
            if (BaseProfile.Instance.Sounds < -39) { image.sprite = SpriteOff; _music.SetFloat("ParamB", -80); }
            else { image.sprite = SpriteOn; _music.SetFloat("ParamB", BaseProfile.Instance.Sounds); }
        }
    }

    public void Slider()
    {
        if (_slider == SliderVibor.Music)
        {
            BaseProfile.Instance.Muisc = sliderValue.value;
            if (sliderValue.value < -39) { _music.SetFloat("ParamA", -80); image.sprite = SpriteOff; }
            else
            {
                _music.SetFloat("ParamA", sliderValue.value);
                image.sprite = SpriteOn;
            }
        }
        else
        {
            BaseProfile.Instance.Sounds = sliderValue.value;
            if (sliderValue.value < -39) { _music.SetFloat("ParamB", -80); image.sprite = SpriteOff; }
            else
            {
                _music.SetFloat("ParamB", sliderValue.value);
                image.sprite = SpriteOn;
            }
        }
    }

    void Click()
    {
        if (_slider == SliderVibor.Music)
        {
            if (sliderValue.value > -39)
            {
                sliderValue.value = sliderValue.minValue; _music.SetFloat("ParamA", -80); image.sprite = SpriteOff; BaseProfile.Instance.Muisc = sliderValue.value;
            }
            else
            {
                sliderValue.value = sliderValue.maxValue; _music.SetFloat("ParamA", 0); image.sprite = SpriteOn; BaseProfile.Instance.Muisc = sliderValue.value;
            }
        }
        else
        {
            if (sliderValue.value > -39)
            {
                sliderValue.value = sliderValue.minValue; _music.SetFloat("ParamB", -80); image.sprite = SpriteOff; BaseProfile.Instance.Sounds = sliderValue.value;
            }
            else
            {
                sliderValue.value = sliderValue.maxValue; _music.SetFloat("ParamB", 0); image.sprite = SpriteOn; BaseProfile.Instance.Sounds = sliderValue.value;
            }
        }
    }
}
