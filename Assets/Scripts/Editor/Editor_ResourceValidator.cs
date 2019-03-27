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
        GUI_GlobalEconomy();
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

        if(go.GetComponent<Actor_Resources>().Debug_GetResourceLength() != resourcesAmount || go.GetComponent<Actor_Resources>().Owner.name == string.Empty)
            Debug.LogFormat("Actor_Resources fields cannot be fixed by this tool. You have to set Owner.nameship and array length by hand. Set array length to {0}", resourcesAmount, go);

        FixTownDemands(go);
    }

    void FixTownDemands(GameObject go)
    {
        Town_Population pop = go.GetComponent<Town_Population>();
        if(pop.Poor.Demands.Length != resourcesAmount) pop.Poor.Demands = new double[resourcesAmount];
        if (pop.Middle.Demands.Length != resourcesAmount) pop.Middle.Demands = new double[resourcesAmount];
        if (pop.Rich.Demands.Length != resourcesAmount) pop.Rich.Demands = new double[resourcesAmount];
    }

    void FixProductionArrays()
    {

    }

    void FixGlobalEconomy()
    {
        Economy_Global ge = GameObject.Find(globalListLocation).GetComponent<Economy_Global>();

        if(ge.producedDay.Length != resourcesAmount) ge.producedDay = new double[resourcesAmount];
        if(ge.boughtDay.Length != resourcesAmount) ge.boughtDay = new double[resourcesAmount];
        if(ge.soldDay.Length != resourcesAmount) ge.soldDay = new double[resourcesAmount];

        if (ge.producedMonth.Length != resourcesAmount) ge.producedMonth = new double[resourcesAmount];
        if(ge.boughtMonth.Length != resourcesAmount) ge.boughtMonth = new double[resourcesAmount];
        if(ge.soldMonth.Length != resourcesAmount) ge.soldMonth = new double[resourcesAmount];

        if (ge.producedLifetime.Length != resourcesAmount) ge.producedLifetime = new double[resourcesAmount];
        if(ge.boughtLifetime.Length != resourcesAmount) ge.boughtLifetime = new double[resourcesAmount];
        if(ge.soldLifetime.Length != resourcesAmount) ge.soldLifetime = new double[resourcesAmount];

        if (ge.pricing.Length != resourcesAmount) ge.pricing = new int[resourcesAmount];
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
        if (go.GetComponent<Actor_Resources>().Owner.name == string.Empty)
        {
            EditorGUILayout.LabelField("Owner.name: " + go.GetComponent<Actor_Resources>().Owner.name, Invalid);
        }
        else
        {
            EditorGUILayout.LabelField("Owner.name: " + go.GetComponent<Actor_Resources>().Owner.name);
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

        //Population
        GUI_TownPopulation(go);
    }


    void GUI_TownPopulation(GameObject go)
    {
        Town_Population pop = go.GetComponent<Town_Population>();
        EditorGUILayout.LabelField("Population Demands: ");
        if(pop.Poor.Demands.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("\tPoor: " + pop.Poor.Demands.Length);
        }
        else
        {
            EditorGUILayout.LabelField("\tPoor: " + pop.Poor.Demands.Length, Invalid);
        }

        if (pop.Middle.Demands.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("\tMiddle: " + pop.Middle.Demands.Length);
        }
        else
        {
            EditorGUILayout.LabelField("\tMiddle: " + pop.Middle.Demands.Length, Invalid);
        }

        if (pop.Rich.Demands.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("\tRich: " + pop.Rich.Demands.Length);
        }
        else
        {
            EditorGUILayout.LabelField("\tRich: " + pop.Rich.Demands.Length, Invalid);
        }
    }

    void GUI_TownProduction(GameObject go)
    {
        Town_Production production = go.GetComponent<Town_Production>();
        List<string> errors = new List<string>();
        foreach(var factory in production.factories)
        {
            foreach(var input in factory.recipe.input) {
                if(input.amount > resourcesAmount || input.amount < 0) 
                {
                    errors.Add(input.inputID + ": " + input.amount);
                }
            }
        }

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
        if (go.GetComponent<Actor_Resources>().Owner.name == string.Empty)
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Owner.name, Invalid);
        }
        else
        {
            EditorGUILayout.LabelField("Resources: " + go.GetComponent<Actor_Resources>().Owner.name);
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

    void GUI_GlobalEconomy()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Global Economy:", EditorStyles.boldLabel);
        if (GUILayout.Button("Fix All")) FixGlobalEconomy();
        EditorGUILayout.EndHorizontal();

        Economy_Global ge = GameObject.Find(globalListLocation).GetComponent<Economy_Global>();

        //Produced
        if(ge.producedDay.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Produced (Day): " + ge.producedDay.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Produced (Day): " + ge.producedDay.Length, Invalid);
        }

        if (ge.producedMonth.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Produced (Month): " + ge.producedMonth.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Produced (Month): " + ge.producedMonth.Length, Invalid);
        }

        if (ge.producedLifetime.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Produced (Lifetime): " + ge.producedLifetime.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Produced (Lifetime): " + ge.producedLifetime.Length, Invalid);
        }

        //Bought
        if (ge.boughtDay.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Bought (Day): " + ge.boughtDay.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Bought (Day): " + ge.boughtDay.Length, Invalid);
        }

        if (ge.boughtMonth.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Bought (Month): " + ge.boughtMonth.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Bought (Month): " + ge.boughtMonth.Length, Invalid);
        }

        if (ge.boughtLifetime.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Bought (Lifetime): " + ge.boughtLifetime.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Bought (Lifetime): " + ge.boughtLifetime.Length, Invalid);
        }

        //Sold
        if (ge.soldDay.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Sold (Day): " + ge.soldDay.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Sold (Day): " + ge.soldDay.Length, Invalid);
        }

        if (ge.soldMonth.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Sold (Month): " + ge.soldMonth.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Sold (Month): " + ge.soldMonth.Length, Invalid);
        }

        if (ge.soldLifetime.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Sold (Lifetime): " + ge.soldLifetime.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Sold (Lifetime): " + ge.soldLifetime.Length, Invalid);
        }

        if (ge.pricing.Length == resourcesAmount)
        {
            EditorGUILayout.LabelField("Pricing: " + ge.pricing.Length);
        }
        else
        {
            EditorGUILayout.LabelField("Pricing: " + ge.pricing.Length, Invalid);
        }
    }
}
