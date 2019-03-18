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

    void Start()
    {
        SetupTooltip();
    }

    void SetupTooltip()
    {
        UI_Tooltip tt = GetComponent<UI_Tooltip>();
        Resource_GlobalList resourceList = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();

        tt.TooltipText += name + "\n";
        foreach (TradeInput ti in _TradeInput)
        {
            foreach (Resource_Input input in ti.trade)
            {
                tt.TooltipText += resourceList.ResourcesList[input.inputID].Name + ": " + input.amount + "\n";
            }
        }
    }

    ///<summary>
    ///Trade with a town in _TradeInput array  
    ///</summary>
    ///<param name="townIndex">index in _TradeInputArray</param>
    public void Trade(NavigationNode point, int index){
		Economy_Local town = point.GetComponent<Economy_Local>();
		foreach(Resource_Input input in _TradeInput[index].trade){
			town.Trade(input, GetComponent<Actor_Resources>());
		}
        GetComponent<Caravan_UnitManager>().SetCaravanSpeed();
	}

    public float StopLength(int index)
    {
        return _TradeInput[index].trade.Length;
    }
}
