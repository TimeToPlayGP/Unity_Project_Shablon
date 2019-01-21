using UnityEditor;
using UnityEngine;

//Данный атрибутом паказывает какой компонент мы редактируем
[CustomEditor(typeof(ButtonClick))]
[CanEditMultipleObjects]

public class ButtonClickEditor : Editor
{
    //Наш класс
    protected ButtonClick subject;                     

    //Данные класса, которые необходимо изменять в Inspector
    protected SerializedProperty OptionAction;    //Выбор
    protected SerializedProperty VisibleObject;   //Показать объект
    protected SerializedProperty HideObject;      //Скрыть объект
    protected SerializedProperty OpenScene;       //Какую сцену открыть

    //Присваиваем this.переменным - переменные класса
    protected virtual void OnEnable()
    {
        subject = target as ButtonClick;

        OptionAction = serializedObject.FindProperty("OptionAction");

        VisibleObject = serializedObject.FindProperty("VisibleObject");
        HideObject = serializedObject.FindProperty("HideObject");

        OpenScene = serializedObject.FindProperty("OpenScene");
    }

    //Переопределяем событие отрисовки 
    public override void OnInspectorGUI()
    {
        //Данный метод необходим в начале, т.к. происходит очистка и 
        //заного отрисовка Inspector
        serializedObject.Update();

        //Отрисовать поле с выпадающим списком
        EditorGUILayout.PropertyField(OptionAction);

        //Какой пункт мы выбрали из выпадающего списка 
        if (subject.OptionAction == OptionActionValue.BeginMethod)
        {
            //Вывод в редактор слайдера
            //EditorGUILayout.PropertyField();
        }
        else if (subject.OptionAction == OptionActionValue.HideObject)
        {
            EditorGUILayout.PropertyField(HideObject);
            //Присвоить начальное значение, если необходимо
            //HideObject.intValue = 55;
        }
        else if (subject.OptionAction == OptionActionValue.VisibleObject)
        {
            EditorGUILayout.PropertyField(VisibleObject);
        }
        else if (subject.OptionAction == OptionActionValue.OpenScene)
        {
            EditorGUILayout.PropertyField(OpenScene);
        }

        //Данный метод необходим в конце
        serializedObject.ApplyModifiedProperties();
    }
}