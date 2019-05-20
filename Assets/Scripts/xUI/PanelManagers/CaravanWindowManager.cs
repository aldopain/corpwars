using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CaravanWindowManager : MonoBehaviour {
    public GameObject CaravanPanel;
    public Button NewCaravanButton;

    [Header("Caravan Performance Panel")]
    public Text StatsText;
    public Text UnitListText;
    public Text UnitInventoryText;

    private List<GameObject> playerCaravans;
    private List<xUI_ButtonPanel> panels;
    private GameObject CaravanPanelPrefab;
    private string CARAVAN_PANEL_PREFAB_NAME = "xUI_ButtonPanel";
    private string NEW_CARAVAN_WINDOW = "CreateCaravanWindow";
    private GameObject selectedCaravan;

	void Start () {
        SetupCaravanPanel();
        AddListener_CreateCaravan(NewCaravanButton);
	}

    void SetupPanelPrefab()
    {
        CaravanPanelPrefab = Resources.Load<GameObject>("xUI_Windows\\" + CARAVAN_PANEL_PREFAB_NAME);
    }

    void FindPlayerCaravans()
    {
        playerCaravans = new List<GameObject>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Caravan"))
        {
            if(go.GetComponent<Actor_Resources>().Owner.gameObject.name == "Player")
            {
                playerCaravans.Add(go);
            }
        }
    }

    void SetupCaravanPanel()
    {
        SetupPanelPrefab();
        FindPlayerCaravans();

        CaravanPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(CaravanPanel.GetComponent<RectTransform>().sizeDelta.x, playerCaravans.Count * CaravanPanelPrefab.GetComponent<RectTransform>().sizeDelta.y);

        panels = new List<xUI_ButtonPanel>();

        //GameObject newCaravan = Instantiate(CaravanPanelPrefab, CaravanPanel.transform);
        //newCaravan.GetComponent<xUI_ButtonPanel>().Set("New Caravan");
        //AddListener_CreateCaravan(newCaravan.GetComponent<Button>());
        //panels.Add(newCaravan.GetComponent<xUI_ButtonPanel>());

        foreach (var car in playerCaravans)
        {
            GameObject go = Instantiate(CaravanPanelPrefab, CaravanPanel.transform);
            go.GetComponent<xUI_ButtonPanel>().Set(car.name, car.GetComponent<Actor_CaravanMovement>().Route.ToString());
            panels.Add(go.GetComponent<xUI_ButtonPanel>());
        }

        for (int i = 1; i < panels.Count; i++) {
            //panels[i].Button.onClick.AddListener(() => SetSelectedCaravan(i));   This leaves you with the same value on each button, don't try fixing the existing implementation with this
            AddListenerToButton(panels[i].Button, i - 1);
        }

        //"else" should probably return from this function before foreach
        if (playerCaravans.Count > 0) {
            SetSelectedCaravan(0);
        }
    }

    //This is wrong, but it is the only way I got it to work as intended
    void AddListenerToButton(Button b, int i) {
        b.onClick.AddListener(() => SetSelectedCaravan(i));
    }

    void AddListener_CreateCaravan(Button b) {
        b.onClick.AddListener(() => GameObject.Find("GameController").GetComponent<xUI_WindowManager>().CreateWindow(NEW_CARAVAN_WINDOW));
    }

	// Update is called once per frame
	void Update () {

	}

    void SetStats()
    {
        StatsText.text = "Stats for " + selectedCaravan.name + "\n";
        StatsText.text += "Power Rating:" + selectedCaravan.GetComponent<Caravan_UnitManager>().PowerRating + "\n";
        StatsText.text += "Ships: " + selectedCaravan.GetComponent<Caravan_UnitManager>().ShipList.Count + "\n";
        StatsText.text += "Route: PLACEHOLDER\n";
        StatsText.text += "Next Point: " + selectedCaravan.GetComponent<Actor_CaravanMovement>().currentRoutePoint.gameObject.name + "\n";
    }

    void SetUnitList() {
        UnitListText.text = "Ships:\n";
        for (int i = 0; i < selectedCaravan.GetComponent<Caravan_UnitManager>().ShipList.Count; i++) {
            UnitListText.text += selectedCaravan.GetComponent<Caravan_UnitManager>().ShipList[i].Health + "\n";
        }
        //foreach (var ship in selectedCaravan.GetComponent<Caravan_UnitManager>().ShipList) {
        //    UnitListText.text += ship.Name + "\n";
        //}
    }

    void SetInventory() {
        UnitInventoryText.text = "Cargo:\n";
        Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
        for (int i = 0; i < gl.ResourcesList.Length; i++) {
            UnitInventoryText.text += gl.ResourcesList[i].Name + ": " + selectedCaravan.GetComponent<Actor_Resources>().GetResource(i) + "\n";
        }
    }

    void SetSelectedCaravan(int i) {
        selectedCaravan = playerCaravans[i];

        SetStats();
        SetUnitList();
        SetInventory();
    }
}
