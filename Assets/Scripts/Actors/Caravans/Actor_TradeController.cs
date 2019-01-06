using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_TradeController : MonoBehaviour {
	Actor_CaravanMovement movement;
	MeshRenderer mesh;
	[HideInInspector]
	public bool isTrading;

	//Wrapping Resource_Input into its own struct so Unity would correctly show it in inspector
	[System.Serializable]
	public struct TradeInput{
		public Resource_Input[] trade;
	}

	[Tooltip("Positive amount means that town is buying from a trader; negative means town is selling to a trader")]
	public TradeInput[] _TradeInput;

	void Awake(){
		movement = GetComponent<Actor_CaravanMovement>();
		mesh = GetComponent<MeshRenderer>();
	}

	///<summary>
	///Trade with a town in _TradeInput array  
	///</summary>
	///<param name="townIndex">index in _TradeInputArray</param>
	public void Trade(int townIndex){
		Economy_Local town = GetComponent<Actor_CaravanMovement>().Route[townIndex].GetComponent<Economy_Local>();
		foreach(Resource_Input input in _TradeInput[townIndex].trade){
			town.Trade(input, GetComponent<Actor_Resources>());
		}
	}
}
