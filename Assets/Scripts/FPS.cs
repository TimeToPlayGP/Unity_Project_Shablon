using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

    private float deltaTime = 0.0f;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //BaseProfile.Instance.countMoney = 2000;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        style.alignment = TextAnchor.LowerRight;
        style.fontSize = (int)(Screen.height * 0.06);
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} fps", fps);
        GUI.Label(rect, text, style);
    }

    // Update is called once per frame
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
}
