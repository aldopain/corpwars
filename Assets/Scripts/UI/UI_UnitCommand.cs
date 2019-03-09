using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitCommand : MonoBehaviour {
    Canvas UC_Canvas;
    Text UnitName;
    Button UnitAttack;
    Button UnitAutoTrade;
    Button UnitMovement;
    public System_SelectionManager manager;

	// Use this for initialization
	void Start () {
        InitUI();
        AssignFunctions();
    }
	
    void InitUI()
    {
        GameObject baseGO = GameObject.Find("Canvas_UnitControl");
        UC_Canvas = baseGO.GetComponent<Canvas>();
        baseGO = baseGO.transform.Find("Panel").gameObject;
        UnitName = baseGO.transform.Find("UC_UnitName").GetComponent<Text>();
        UnitAttack = baseGO.transform.Find("UC_UnitAttack").GetComponent<Button>();
        UnitAutoTrade = baseGO.transform.Find("UC_UnitAutoTrade").GetComponent<Button>();
        UnitMovement = baseGO.transform.Find("UC_UnitMovement").GetComponent<Button>();
    }

    void AssignFunctions()
    {
        UnitAttack.onClick.AddListener(PLACEHOLDER_ATTACK);
        UnitAutoTrade.onClick.AddListener(PLACEHOLDER_AUTOTRADE);
        UnitMovement.onClick.AddListener(PLACEHOLDER_MOVEMENT);
    }

    public void Enable(GameObject go)
    {
        UC_Canvas.enabled = true;
        UnitName.text = go.name;
    }

    public void Disable()
    {
        UC_Canvas.enabled = false;
    }

    public void UpdateInfo(GameObject go)
    {
        UnitName.text = go.name;
    }

    public void PLACEHOLDER_ATTACK()
    {
        print("Attack:" + manager.GetSelected().name);
    }

    public void PLACEHOLDER_AUTOTRADE()
    {
        print("Auto Trade:" + manager.GetSelected().name);
    }

    public void PLACEHOLDER_MOVEMENT()
    {
        print("Movement:" + manager.GetSelected().name);
    }
}
