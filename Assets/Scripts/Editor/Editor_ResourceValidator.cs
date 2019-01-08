using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_ResourceValidator : EditorWindow {
    int resourcesAmount = 0;
    string globalListLocation = "GameController";
    string[] PlayerList;
    string PlayerString;
    

    //Editor variables
    int spaceHeight = 10;
    Vector2 scrollPos_Towns;
    Vector2 scrollPos_Caravans;
    GUIStyle Invalid;

    [MenuItem("Debug/Resource Validator")]
    static void Init()
    {
        Editor_ResourceValidator window = new Editor_ResourceValidator();
        window.Show();
        window.titleContent = new GUIContent("ResValidator");
    }

    void OnEnable()
    {
        Invalid = new GUIStyle();
        Invalid.normal.textColor = Color.red;
        Invalid.fontStyle = FontStyle.Bold;

        GetResourcesAmount();
        GetPlayers();
    }

    void OnGUI()
    {
        GUI_ResourceAmount();
        GUILayout.Space(spaceHeight);
        GUI_ShowPlayers();
        GUILayout.Space(spaceHeight);
        GUI_ShowAllTowns();
        GUILayout.Space(spaceHeight);
        GUI_ShowAllCaravans();
    }

    void GetResourcesAmount()
    {
        resourcesAmount = GameObject.Find(globalListLocation).GetComponent<Resource_GlobalList>().ResourcesList.Length;
    }

    void GetPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerList = new string[players.Length];
        PlayerString = "";

        for(int i = 0; i < players.Length; i++)
        {
            PlayerList[i] = players[i].name;
            PlayerString += players[i].name + "; ";
        }
    }


    void FixAllTowns()
    {
        GameObject[] caravans = GameObject.FindGameObjectsWithTag("Town");
        foreach (GameObject go in caravans)
        {
            FixTown(go);
        }
    }

    void FixTown(GameObject go)
    {
        Economy_Local le = go.GetComponent<Economy_Local>();
        if (le.produced.Length != resourcesAmount) le.produced = new double[resourcesAmount];
        if (le.bought.Length != resourcesAmount) le.bought = new double[resourcesAmount];
        if (le.sold.Length != resourcesAmount) le.sold = new double[resourcesAmount];
        if (le.priceModifiers.Length != resourcesAmount) le.priceModifiers = new double[resourcesAmount];

        if(go.GetComponent<Actor_Resources>().Debug_GetResourceLength() != resourcesAmount || go.GetComponent<Actor_Resources>().Owner == string.Empty)
            Debug.LogFormat("Actor_Resources fields cannot be fixed by this tool. You have to set ownership and array length by hand. Set array length to {0}", resourcesAmount, go);
    }

    void FixProductionArrays()
    {

    }

    void GUI_ResourceAmount()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resources Amount");
        resourcesAmount = EditorGUILayout.DelayedIntField(resourcesAmount);
        if (GUILayout.Button("Get From Resource List"))
        {
            GetResourcesAmount();
        }
        EditorGUILayout.EndHorizontal();
    }

    void GUI_ShowPlayers()
    {
        EditorGUILayout.LabelField("Players:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField(PlayerString);
    }

    void GUI_ShowAllTowns()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Towns:", EditorStyles.boldLabel);
        if (GUILayout.Button("Fix All"))
        {
            FixAllTowns();
        }
        EditorGUILayout.EndHorizontal();

        scrollPos_Towns = EditorGUILayout.BeginScrollView(scrollPos_Towns);
        GameObject[] towns = GameObject.FindGameObjectsWithTag("Town");
        foreach (GameObject go in towns)
        {
            GUI_Town(go);
        }
        EditorGUILayout.EndScrollView();
    }

    void GUI_Town(GameObject go)
    {
        EditorGUILayout.LabelField(go.name, EditorStyles.boldLabel);

        //Owner
        if (go.GetComponent<Actor_Resources>().Owner == string.Empty)
        {
            EditorGUILayout.LabelField("Owner: " + go.GetComponent<Actor_Resources>().Owner, Invalid);
        }
        else
        {
            EditorGUILayout.LabelField("Owner: " + go.GetComponent<Actor_Resources>().Owner);
        }

        //Resources
        if (go.GetComponent<Actor_Resources>().Debug_GetResourceLength() == resourcesAmount)
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Debug_GetResourceLength().ToString());
        }
        else
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Debug_GetResourceLength().ToString(), Invalid);
        }

        //Produced
        if (go.GetComponent<Economy_Local>().produced.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Produced: " + go.GetComponent<Economy_Local>().produced.Length.ToString());
        }
        else
        {
            EditorGUILayout.LabelField("Produced: " + go.GetComponent<Economy_Local>().produced.Length.ToString(), Invalid);
        }

        //Bought
        if (go.GetComponent<Economy_Local>().bought.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Bought: " + go.GetComponent<Economy_Local>().bought.Length.ToString());
        }
        else
        {
            EditorGUILayout.LabelField("Bought: " + go.GetComponent<Economy_Local>().bought.Length.ToString(), Invalid);
        }

        //Sold
        if (go.GetComponent<Economy_Local>().sold.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Sold: " + go.GetComponent<Economy_Local>().sold.Length.ToString());
        }
        else
        {
            EditorGUILayout.LabelField("Sold: " + go.GetComponent<Economy_Local>().sold.Length.ToString(), Invalid);
        }

        //Price Modifier
        if (go.GetComponent<Economy_Local>().priceModifiers.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Sold: " + go.GetComponent<Economy_Local>().priceModifiers.Length.ToString());
        }
        else
        {
            EditorGUILayout.LabelField("Sold: " + go.GetComponent<Economy_Local>().priceModifiers.Length.ToString(), Invalid);
        }

        //Production
        GUI_TownProduction(go);
    }


    void GUI_TownProduction(GameObject go)
    {
        Town_Production production = go.GetComponent<Town_Production>();
        List<string> errors = new List<string>();
        for(int i = 0; i < production.resourceID.Length; i++)
        {
            if(production.resourceID[i] > resourcesAmount || production.resourceID[i] < 0) 
            {
                errors.Add(i + ": " + production.resourceID[i]);
            }
        }

        Debug.Log(name + ": " + errors.Count);
        if(errors.Count == 0)
        {
            EditorGUILayout.LabelField("Production: Correct");
        }
        else
        {
            EditorGUILayout.LabelField("Production: Errors(" + errors.Count + ") :", Invalid);
            foreach (string s in errors)
            {
                EditorGUILayout.LabelField(s, Invalid);
            }
        }
    }

    void GUI_ShowAllCaravans()
    {
        EditorGUILayout.LabelField("Caravans:", EditorStyles.boldLabel);

        scrollPos_Caravans = EditorGUILayout.BeginScrollView(scrollPos_Caravans);
        GameObject[] caravans = GameObject.FindGameObjectsWithTag("Caravan");
        foreach(GameObject go in caravans)
        {
            GUI_ShowCaravan(go);
        }
        EditorGUILayout.EndScrollView();
    }

    void GUI_ShowCaravan(GameObject go)
    {
        EditorGUILayout.LabelField(go.name, EditorStyles.boldLabel);
        //Owner
        if (go.GetComponent<Actor_Resources>().Owner == string.Empty)
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Owner, Invalid);
        }
        else
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Owner);
        }

        //Resources
        if (go.GetComponent<Actor_Resources>().Debug_GetResourceLength() == resourcesAmount)
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Debug_GetResourceLength().ToString());
        }
        else
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Debug_GetResourceLength().ToString(), Invalid);
        }
    }
}
