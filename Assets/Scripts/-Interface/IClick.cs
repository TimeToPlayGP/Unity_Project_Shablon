using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нажатие на элемент
/// </summary>
public interface IClick
{
    //Выполнять действия при старте сцены (P.S. событие на метод Click())
    void Start();

    //Нажатие на элемент
    void Click();

    //При нажатии всегда воспроизводить музыку
    void PlaySound();
}
