using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piqUi_TownWindowManager : MonoBehaviour, IWindow
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
    //Fuck it for now

    //Resources Panel
    [Header("Resources")]
    public List<piqUI_ResourceLine> ResourceLines;

    public piqUI_InformationManager manager;

    public void Show() {
        gameObject.SetActive(true);
        InitUI();
    }

    public void Hide() {
        gameObject.SetActive(false);
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
}
