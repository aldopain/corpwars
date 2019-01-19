using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Resource))]
public class Editor_Resource : Editor{
    public override void OnInspectorGUI()
    {
        Resource editTarget = (Resource)target;

        EditorGUILayout.LabelField("Visuals");
        editTarget.Name = EditorGUILayout.TextField("Name", editTarget.Name);
        EditorGUILayout.LabelField("Description");
        editTarget.Description = EditorGUILayout.TextArea(editTarget.Description);
        EditorGUILayout.LabelField("Recipe");
    }
}
