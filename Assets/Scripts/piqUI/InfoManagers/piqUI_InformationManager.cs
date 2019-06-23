using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piqUI_InformationManager : MonoBehaviour
{
    public piqUI_ShipInfoManager ships;
    public piqUI_TownInfoManager towns;
    public Resource_GlobalList resources;

    void Awake() {
        GetChildManagers();
    }

    private void GetChildManagers() {
        ships = GetComponent<piqUI_ShipInfoManager>();
        towns = GetComponent<piqUI_TownInfoManager>();
    }


    /* THIS FUNCTION IS FOR DEBUGGING PURPOSES ONLY
     * AVOID USING THIS TO UPDATE INFO THAT COULD BE UPDATED WITH EVENTS
     * 
     * Use this function to update information that doesn't have events attached to it yet
     */
    private void Debug_UpdateInfo() {

    }

    void Update() {
        Debug_UpdateInfo();
    }
}
