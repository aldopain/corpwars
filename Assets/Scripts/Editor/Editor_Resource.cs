using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Resource))]
public class Editor_Resource : Editor{
    int inputLength = 0;
    int inputLengthPrev = 0;

    public override void OnInspectorGUI()
    {
        Resource editTarget = (Resource)target;

        GUI_ShowVisuals(editTarget);

        inputLength = EditorGUILayout.DelayedIntField("Input Amount", inputLength);
        if(inputLength != inputLengthPrev)
        {
            System.Array.Resize(ref editTarget.Recipe.input, inputLength);
        }

        if(inputLength == 0)
        {
            EditorGUILayout.LabelField("This is a base resource");
        }
        else
        {
            GUI_ShowRecipeInterface(editTarget);
        }

        inputLengthPrev = inputLength;
    }

    void GUI_ShowVisuals(Resource editTarget)
    {
        EditorGUILayout.LabelField("Visuals", EditorStyles.boldLabel);
        GUILayout.Space(64);
        Rect spriteRect = new Rect(Screen.width - 70, 64, 64, 64);
        editTarget.Image = (Sprite)EditorGUI.ObjectField(spriteRect, editTarget.Image, typeof(Sprite), false);

        editTarget.Name = EditorGUILayout.TextField("Name", editTarget.Name);
        EditorGUILayout.LabelField("Description");
        editTarget.Description = EditorGUILayout.TextArea(editTarget.Description);
        //EditorGUI.obj
        EditorGUILayout.LabelField("Recipe");
        editTarget.ConvertionOutput = EditorGUILayout.DoubleField("Convertion Output", editTarget.ConvertionOutput);
    }

    void GUI_ShowRecipeInterface(Resource editTarget)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Input ID");
        EditorGUILayout.LabelField("Amount");

        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < inputLength; i++)
        {
            GUI_ShowRecipeInput(editTarget.Recipe.input[i]);
        }
    }

    void GUI_ShowRecipeInput(Resource_Input editTarget)
    {
        EditorGUILayout.BeginHorizontal();

        editTarget.inputID = EditorGUILayout.DelayedIntField(editTarget.inputID);
        editTarget.amount = EditorGUILayout.DelayedDoubleField(editTarget.amount);

        EditorGUILayout.EndHorizontal();
    }
}
