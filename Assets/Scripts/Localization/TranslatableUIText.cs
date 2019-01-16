using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatableUIText : MonoBehaviour
{
    public TranslatableString translatableString;

    public void Start()
    {
        GetComponent<UnityEngine.UI.Text>().text = translatableString;
    }
}