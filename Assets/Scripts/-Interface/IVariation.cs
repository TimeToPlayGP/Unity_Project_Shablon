using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Изменение состояния объекта
/// </summary>
public interface IVariation
{
    //Координаты/размеры/положение объекта, как надо изменить размер/положение
    Vector3 Vector3to { get; set; }

    //Скорость, с которой изменять размер/положение объекта
    float speedVariation { get; set; }

    //Вернуть объект в исходное состояние?
    bool isReverseState { get; set; }

    //В данном методе описывается конкретное изменение объекта
    void variationObject();
}
