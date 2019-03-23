using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopPanelManager : MonoBehaviour {
    public Text MoneyText;
    public Text DateText;

    private Player_Money money;
    private System_Time timeController;
    void Start()
    {
        money = GameObject.Find("Player").GetComponent<Player_Money>();
        timeController = GameObject.Find("GameController").GetComponent<System_Time>();
    }
	void Update () {
        MoneyText.text = money.Amount.ToString();
        DateText.text = timeController.ToString();
	}
}
