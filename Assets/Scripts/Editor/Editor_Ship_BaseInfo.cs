using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ship_BaseInfo))]
public class Editor_Ship_BaseInfo : Editor{
    Editor MeshEditor;
    Mesh prevShipMesh;

    public override void OnInspectorGUI()
    {
        Ship_BaseInfo editTarget = (Ship_BaseInfo)target;

        editTarget.name = EditorGUILayout.TextField("Ship Name", editTarget.name);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Trading Settings", EditorStyles.boldLabel);
        editTarget.Speed = EditorGUILayout.FloatField("Speed", editTarget.Speed);
        editTarget.InventorySize = EditorGUILayout.DoubleField("Inventory Size", editTarget.InventorySize);
        editTarget.Cost = EditorGUILayout.IntField("Cost", editTarget.Cost);
        editTarget.Maintenance = EditorGUILayout.IntField("Maintenance", editTarget.Maintenance);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Combat Settings", EditorStyles.boldLabel);
        editTarget.MaximumHealth = EditorGUILayout.DoubleField("Max Health", editTarget.MaximumHealth);
        editTarget.CurrentHealth = EditorGUILayout.DoubleField("Current Health", editTarget.CurrentHealth);
        editTarget.Attack = EditorGUILayout.DoubleField("Attack", editTarget.Attack);
        editTarget.Defence = EditorGUILayout.DoubleField("Defence", editTarget.Defence);

        if (GUILayout.Button("Randomize")) Randomize(editTarget);

        editTarget.mesh = (Mesh)EditorGUILayout.ObjectField(editTarget.mesh, typeof(Mesh), false);

        GUI_ShowModelPreview(editTarget);
        prevShipMesh = editTarget.mesh;
    }

    void Randomize(Ship_BaseInfo ship)
    {
        ship.Speed = Random.Range(1, 10);
        ship.InventorySize = Random.Range(0, 10);
        ship.MaximumHealth = Random.Range(1, 10);
        ship.CurrentHealth = Random.Range(1, 10);
        ship.Maintenance = Random.Range(1, 10);
        ship.Cost = Random.Range(1, 10);
        ship.Attack = Random.Range(0, 10);
        ship.Defence = Random.Range(0, 10);
    }

    void GUI_ShowResource(Ship_BaseInfo editTarget)
    {

    }

    void GUI_ShowModelPreview(Ship_BaseInfo ship)
    {
        GUIStyle bgStyle = new GUIStyle();
        bgStyle.normal.background = EditorGUIUtility.whiteTexture;

        if (ship.mesh != null)
        {
            if (MeshEditor == null || prevShipMesh != ship.mesh)
            {
                MeshEditor = Editor.CreateEditor(ship.mesh);
            }

            MeshEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgStyle);
        }
    }
}
