using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Набор сцен в текущем проекте
/// </summary>
public enum SceneValue : byte
{
    //Меню
    Menu = 1,

    //Игра
    Game = 2,

    //Выбор уровня в таблице
    LevelGrid = 3,

    //Выбор уровня в списке
    LevelList = 4,

    //Магазин
    Shop = 5,
}
