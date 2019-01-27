using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ship_BaseInfo))]
public class Editor_Ship_BaseInfo : Editor{
    public override void OnInspectorGUI()
    {
        Ship_BaseInfo editTarget = (Ship_BaseInfo)target;

        editTarget.name = EditorGUILayout.TextField("Ship Name", editTarget.name);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Trading Settings", EditorStyles.boldLabel);
        editTarget.Speed = EditorGUILayout.FloatField("Speed", editTarget.Speed);
        editTarget.InventorySize = EditorGUILayout.DoubleField("Inventory Size", editTarget.InventorySize);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Combat Settings", EditorStyles.boldLabel);
        editTarget.Health = EditorGUILayout.DoubleField("Max Health", editTarget.Health);
        editTarget.Attack = EditorGUILayout.DoubleField("Attack", editTarget.Attack);
        editTarget.Defence = EditorGUILayout.DoubleField("Defence", editTarget.Defence);

        if (GUILayout.Button("Randomize")) Randomize(editTarget);
    }

    void Randomize(Ship_BaseInfo ship)
    {
        ship.Speed = Random.Range(1, 10);
        ship.InventorySize = Random.Range(0, 10);
        ship.Health = Random.Range(1, 10);
        ship.Attack = Random.Range(0, 10);
        ship.Defence = Random.Range(0, 10);
        
    }

    void GUI_ShowResource(Ship_BaseInfo editTarget)
    {

    }
}
