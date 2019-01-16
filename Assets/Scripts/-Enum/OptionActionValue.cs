using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вариант выбора действия, при нажатии на Button
/// </summary>
public enum OptionActionValue : byte
{
    //Выполнить метод
    BeginMethod,

    //Сделать видимым объект
    VisibleObject,

    //Сделать скрытым объект
    HideObject,

    //Открыть сцену
    OpenScene,
}
