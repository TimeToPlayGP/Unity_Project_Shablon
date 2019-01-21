using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кнопка, на выполнение действия и изменение своего размера(анимация)
/// </summary>
public class ButtonClickAndSize : ButtonClick, IClick, ISize
{
    //Размер, до которого изменить объект
    public Vector3 Vector3to { get { return _vector3to; } set { _vector3to = value; } }
    [SerializeField] private Vector3 _vector3to = new Vector3(0.8f, 0.8f, 0.8f);

    //С какой скоростью производить изменения
    public float speedVariation { get { return _speedVariation; } set { _speedVariation = value; } }
    [SerializeField] private float _speedVariation = 0.15f;

    //Вернуть на исходное состояние?
    public bool isReverseState { get { return _isReverseState; } set { _isReverseState = value; } }
    [SerializeField] private bool _isReverseState = true;

    /// <summary>
    /// Переопределенный метод клика
    /// </summary>
    public override void Click()
    {
        gameObject.transform.localScale = Vector3.one;              //Вернуть на начальное значение, если не успела
                                                                    //проиграть анимация LeanTween.scale
        variationObject();
    }

    public void variationObject()
    {
        LeanTween.scale(this.gameObject, Vector3to, speedVariation) //Изменить размер
            .setLoopPingPong((isReverseState) ? 1 : 0)              //Повтор
            .setIgnoreTimeScale(true)                               //Игнорировать TimeScale
            .setOnComplete(base.Click);                             //Вызвать базовый метод Click
    }
}
