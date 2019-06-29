using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piqUi_TownWindowManager : piqUI_Window
{
    //Units and buildings
    [Header("Units and buildings")]
    public List<Button> ButtonGrid; //Should probably be its own class, but it'll do for now

    //Population Panel
    //Class selector
    public enum PopClass {
        Poor,
        Middle,
        Rich
    }

    [Header("Population class selector")]
    public Button PoorClassButton;
    public Button MiddleClassButton;
    public Button RichClassButton;

    //Status
    [Header("Status")]
    public Text PopulationText;
    public Slider ProductivitySlider;
    public Slider HappinessSlider;

    //Needs
    [Header("Needs")]
    public Text BasicNeeds;
    public Text LuxuryNeeds;


    //Production Panel
    [Header("Production")]
    public List<piqUI_FactoryPanel> FactoryPanels;

    //Resources Panel
    [Header("Resources")]
    public List<piqUI_ResourceLine> ResourceLines;

    public piqUI_InformationManager manager;

    public override void Show() {
        InitUI();
        base.Show();
    }

    void Start() {
        manager = transform.root.GetComponent<piqUI_InformationManager>();
        SetupStatus();
        InitUI();
    }

    private void SetupStatus() {
        ProductivitySlider.maxValue = manager.towns.GetMaxProductivity();
        HappinessSlider.maxValue = manager.towns.GetMaxHappiness();
    }

    private void InitUI() {
        ShowPopulation(PopClass.Poor);
        ShowResourcesList();
    }

    //This is done to circumvent Unity's inability to show enums as dropdown in inspector
    public void ShowPopulation_Poor() {
        ShowPopulation(PopClass.Poor);
    }

    public void ShowPopulation_Middle() {
        ShowPopulation(PopClass.Middle);
    }

    public void ShowPopulation_Rich() {
        ShowPopulation(PopClass.Rich);
    }

    void ShowPopulation(PopClass c) {
        piqUI_TownInfoManager.PopClassInfo info = manager.towns.GetPopClassInfo(c);
        PopulationText.text = "Population: " + info.population;
        HappinessSlider.value = info.happiness;
        ProductivitySlider.value = info.productivity;

        string basicNeeds = "";
        for (int i = 0; i < info.basicNeeds.Length; i++) {
            if (info.basicNeeds[i] != 0) {
                basicNeeds += manager.resources.ResourcesList[i].Name + " x" + info.basicNeeds[i] +  "\n";
            }
        }
        BasicNeeds.text = basicNeeds;

        string luxuryNeeds = "";
        for (int i = 0; i < info.luxuryNeeds.Length; i++) {
            if (info.luxuryNeeds[i] != 0) {
                luxuryNeeds += manager.resources.ResourcesList[i].Name + " x" + info.luxuryNeeds[i] + "\n";
            }
        }
        LuxuryNeeds.text = luxuryNeeds;
    }

    public void ShowResourcesList() {
        piqUI_TownInfoManager.TownInventoryInfo inv = manager.towns.GetInventoryInfo();
        for (int i = 0; i < manager.resources.ResourcesList.Length; i++) {
            ResourceLines[i].Name = manager.resources.ResourcesList[i].name;
            ResourceLines[i].Amount = inv.Amount[i].ToString();
        }
    }

    public void ShowFactories() {
        List<piqUI_TownInfoManager.FactoryInfo> info = manager.towns.GetFactoriesInfo();

        //Debug, create dynamic factories list
        int count;
        if (info.Count < manager.resources.ResourcesList.Length) {
            count = info.Count;
        } else {
            count = manager.resources.ResourcesList.Length;
        }

        for (int i = 0; i < count; i++) {
            FactoryPanels[i].FactoryType = info[i]._type;
            foreach (var _input in info[i].input) {
                FactoryPanels[i].AddInput(manager.resources.ResourcesList[_input.inputID].Name + "x" + _input.amount);
            }

            foreach (var _out in info[i].input) {
                FactoryPanels[i].AddOutput(manager.resources.ResourcesList[_out.inputID].Name + "x" + _out.amount);
            }
        }
    }
}
