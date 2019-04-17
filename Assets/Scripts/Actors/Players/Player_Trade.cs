using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Trade : MonoBehaviour {
	public Player_Money money;
	public Actor_Resources resources;
	public string name;
	public System_EventPool events;
	public GameObject gm;

	void Start () {
		resources = GetComponent<Actor_Resources>();
		money = GetComponent<Player_Money>();
		events = gm.GetComponent<System_EventPool>();
	}

	public void TransferMoney(int amount, Player_Manager other) {
		this.money.Transfer(amount, other.money);
	}

	public void TransferResources(int index, double amount, Player_Manager other) {
		this.resources.Transfer(index, amount, other.resources);
	}
}