using UnityEngine;
using System.Collections;

/// <summary>
/// Вешать на объекты, при включении которых, должно останавливаться время
/// </summary>
public class TimeScale : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (gameObject.name == "Main Camera") Time.timeScale = 1;
	}

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
