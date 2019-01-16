using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Данный атрибутом паказывает какой компонент мы редактируем
[CustomEditor(typeof(ButtonClickAndSize))]
[CanEditMultipleObjects]

public class ButtonClickAndSizeEditor : BaseButtonEditor
{ 
    protected override void OnEnable()
    {
        subject = target as ButtonClickAndSize;

        OptionAction = serializedObject.FindProperty("OptionAction");

        VisibleObject = serializedObject.FindProperty("VisibleObject");
        HideObject = serializedObject.FindProperty("HideObject");

        OpenScene = serializedObject.FindProperty("OpenScene");
    }
}
