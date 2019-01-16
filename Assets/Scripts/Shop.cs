using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    public GameObject PanelNotInternet; //Нет соединения
    public GameObject PanelNotReclamy;  //Не загрузилась реклама

    // Use this for initialization
    void Start()
    {

    }

    public void OnInternet()
    {
        PanelNotInternet.SetActive(true);
    }

    public void OnReclamy()
    {
        PanelNotReclamy.SetActive(true);
    }
}
