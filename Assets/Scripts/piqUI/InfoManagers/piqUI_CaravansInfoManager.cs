using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piqUI_CaravansInfoManager : MonoBehaviour
{
    private List<GameObject> playerCaravans;

    void Start() {
        FillPlayerCaravans();
    }

    public void FillPlayerCaravans() {
        playerCaravans = new List<GameObject>();
        GameObject[] caravans = GameObject.FindGameObjectsWithTag("Caravan");

        foreach (GameObject c in caravans) {
            if (c.GetComponent<Actor_Resources>().Owner.gameObject.name == "Player")    //This is really bad, we make a better way to check if an invetory belongs to player
                playerCaravans.Add(c);
        }
    }

    //This will also update UI later
    //Probably would be a good idea to overload this function to take different components of caravan (like Actor_Resources, CaravanMovement, etc)
    public void AddCaravan(GameObject c) {
        playerCaravans.Add(c);
    }

    public string PrintCaravans() {
        if (playerCaravans.Count == 0)
            return "No Caravans";

        string res = string.Empty;
        foreach (GameObject c in playerCaravans) {
            res += c.name + "| Load: " + c.GetComponent<Actor_Resources>().GetAllResources() + " | Size: " + c.GetComponent<Caravan_UnitManager>().ShipList.Count;
        }


        return res;
    }
}
