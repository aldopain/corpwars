using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCaravanWindowManager : MonoBehaviour {
    public Ship_BaseInfo[] AvailableShips;
    public GameObject[] TownList;
    public GameObject CaravanPrefab;
    public GameObject ButtonPrefab;

    //UI elements
    public Dropdown UnitSelector;
    public Text ShipListText;
    //public Transform ShipList;
    public Dropdown TownSelector;
    //public Transform RouteList;
    public Text RouteListText;

    public List<Ship_BaseInfo> currentShips;
    public List<NavigationNode> currentRoute;
    // Use this for initialization
	void Start () {
        //ShipList.GetComponent<RectTransform>().sizeDelta = new Vector2(ShipList.GetComponent<RectTransform>().sizeDelta.x, ButtonPrefab.GetComponent<RectTransform>().sizeDelta.y);

        //ButtonPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(ShipList.GetComponent<RectTransform>().sizeDelta.x, ButtonPrefab.GetComponent<RectTransform>().sizeDelta.y);
        SetupUnitSelector();
        SetupTownSelector();
	}

    void SetupUnitSelector() {
        UnitSelector.ClearOptions();
        List<string> shipNames = new List<string>();
        foreach (Ship_BaseInfo ship in AvailableShips) {
            shipNames.Add(ship.name);
        }

        UnitSelector.AddOptions(shipNames);
    }

    void SetupTownSelector() {
        TownList = GameObject.FindGameObjectsWithTag("Town");
        TownSelector.ClearOptions();

        List<string> townNames = new List<string>();
        foreach (GameObject town in TownList) {
            townNames.Add(town.name);
        }

        TownSelector.AddOptions(townNames);
    }

    public void AddShip() {
        currentShips.Add(AvailableShips[UnitSelector.value]);
        //ShipList.GetComponent<RectTransform>().sizeDelta = new Vector2(ShipList.GetComponent<RectTransform>().sizeDelta.x, ShipList.GetComponent<RectTransform>().sizeDelta.y + ButtonPrefab.GetComponent<RectTransform>().sizeDelta.y);
        //GameObject tmp = Instantiate(ButtonPrefab, ShipList);
        //tmp.GetComponent<xUI_ListButton>().ButtonText.text = AvailableShips[UnitSelector.value].name;
        //AddListener_ShipButton(tmp.GetComponent<xUI_ListButton>().CloseButton, currentShips.Count - 1);
        ShipListText.text += AvailableShips[UnitSelector.value].name + "\n";
    }

    public void AddWaypoint() {
        if (currentRoute.Count != 0 && currentRoute[currentRoute.Count - 1] == TownList[TownSelector.value].GetComponent<NavigationNode>())
            return;
        currentRoute.Add(TownList[TownSelector.value].GetComponent<NavigationNode>());
        //RouteList.GetComponent<RectTransform>().sizeDelta = new Vector2(RouteList.GetComponent<RectTransform>().sizeDelta.x, RouteList.GetComponent<RectTransform>().sizeDelta.y + ButtonPrefab.GetComponent<RectTransform>().sizeDelta.y);
        //GameObject tmp = Instantiate(ButtonPrefab, RouteList);
        //tmp.GetComponent<xUI_ListButton>().ButtonText.text = TownList[TownSelector.value].name;
        RouteListText.text += TownList[TownSelector.value].name + "\n";
    }

    public void CreateCaravan() {
        if (currentRoute.Count == 1) {
            //Put unit in town instead of spawning it on route
        }

        GameObject caravan = Instantiate(CaravanPrefab, currentRoute[0].transform.position, new Quaternion(0,0,0,0));
        caravan.GetComponent<Caravan_UnitManager>().ShipList = currentShips;
        caravan.GetComponent<Caravan_UnitManager>().CopyShipList();
        caravan.GetComponent<Caravan_UnitManager>().SetCaravanSpeed();

        caravan.GetComponent<Actor_CaravanMovement>().Route = NavigationRoute.CreateRoute(currentRoute[0], currentRoute[currentRoute.Count - 1]);

        caravan.GetComponent<Actor_Resources>().Owner = GameObject.Find("Player").GetComponent<Player_Manager>();
        Clear();
    }

    public void Clear() {
        currentRoute.Clear();
        RouteListText.text = "";
        currentShips.Clear();
        ShipListText.text = "";
    }

    public void RemoveShip(int i) {
        if (i < 0)
            return;
        currentShips.RemoveAt(i);
        //TODO: Properly remove line from the string, intead of refreshing the whole text
        ShipListText.text = "";
        foreach (var v in currentShips) {
            ShipListText.text += v.name + "\n";
        }
    }

    public void RemoveLastShip() {
        RemoveShip(currentShips.Count - 1);
    }

    public void AddListener_ShipButton(Button b, int i) {
        b.onClick.AddListener(() => RemoveShip(i));
    }

    public void RemoveWaypoint(int i) {
        if (i < 0)
            return;
        currentRoute.RemoveAt(i);

        //TODO: Properly remove line from the string, intead of refreshing the whole text
        RouteListText.text = "";
        foreach (var v in currentRoute) {
            RouteListText.text += v.name + "\n";
        }
    }

    public void RemoveLastWaypoint() {
        RemoveWaypoint(currentRoute.Count - 1);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
