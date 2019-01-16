using UnityEngine;
using System.Collections;

public class WorkAdmobADS : MonoBehaviour
{

    public bool HideBanner;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        if (HideBanner) AdmobADS.HideBanner();
        else AdmobADS.ShowBanner();
    }
}
