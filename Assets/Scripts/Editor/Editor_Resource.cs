using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Resource))]
public class Editor_Resource : Editor{
    int inputLength = 0;
    int inputLengthPrev = 0;

    int outputLength = 0;
    int outputLengthPrev = 0;

    void OnEnable(){
        Resource editTarget = (Resource)target;

        inputLength = editTarget.Recipe.input.Length;
        outputLength = editTarget.Recipe.output.Length;

        inputLengthPrev = inputLength;
        outputLengthPrev = outputLength;
    }

    public override void OnInspectorGUI()
    {
        Resource editTarget = (Resource)target;

        GUI_ShowVisuals(editTarget);

        inputLength = EditorGUILayout.DelayedIntField("Input Amount", inputLength);
        outputLength = EditorGUILayout.DelayedIntField("Output Amount", outputLength);
        if(inputLength != inputLengthPrev)
        {
            System.Array.Resize(ref editTarget.Recipe.input, inputLength);
        }

        if(outputLength != outputLengthPrev)
        {
            System.Array.Resize(ref editTarget.Recipe.output, outputLength);
        }

        GUI_ShowRecipeInterface(editTarget);

        inputLengthPrev = inputLength;
        outputLengthPrev = outputLength;
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
    }

    void GUI_ShowRecipeInterface(Resource editTarget)
    {
        EditorGUILayout.LabelField("Recipe");
        EditorGUILayout.LabelField("Input", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Input ID");
        EditorGUILayout.LabelField("Amount");
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < inputLength; i++)
        {
            GUI_ShowRecipeInput(editTarget.Recipe.input[i]);
        }

        EditorGUILayout.LabelField("Output", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Input ID");
        EditorGUILayout.LabelField("Amount");
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < outputLength; i++)
        {
            GUI_ShowRecipeOutput(editTarget.Recipe.output[i]);
        }
    }

    void GUI_ShowRecipeInput(Resource_Input editTarget)
    {
        EditorGUILayout.BeginHorizontal();

        editTarget.inputID = EditorGUILayout.DelayedIntField(editTarget.inputID);
        editTarget.amount = EditorGUILayout.DelayedDoubleField(editTarget.amount);

        EditorGUILayout.EndHorizontal();
    }

    void GUI_ShowRecipeOutput(Resource_Input editTarget){
        EditorGUILayout.BeginHorizontal();

        editTarget.inputID = EditorGUILayout.DelayedIntField(editTarget.inputID);
        editTarget.amount = EditorGUILayout.DelayedDoubleField(editTarget.amount);

        EditorGUILayout.EndHorizontal();
    }
}
