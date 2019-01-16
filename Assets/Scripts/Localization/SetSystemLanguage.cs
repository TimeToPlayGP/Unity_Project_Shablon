using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSystemLanguage : MonoBehaviour {

    void SetLanguage(int language_id)
    {
        PlayerPrefs.SetInt("language_id", language_id);
    }

    public void GuessLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                SetLanguage(TranslatableString.LANG_EN);
                return;
            case SystemLanguage.Russian:
                SetLanguage(TranslatableString.LANG_RU);
                return;
            case SystemLanguage.German:
                SetLanguage(TranslatableString.LANG_DE);
                return;
        }
    }
}
