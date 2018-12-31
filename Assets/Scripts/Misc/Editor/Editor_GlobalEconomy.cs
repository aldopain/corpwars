using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_GlobalEconomy : MonoBehaviour {
	[MenuItem("Economy/Global/SetResources")]
	private static void SetupResources(){
		GlobalEconomy ge = GameObject.Find("GameController").GetComponent<GlobalEconomy>();
		if(ge.globalInfo.Count != System.Enum.GetValues(typeof(TradeResource.Types)).Length){
			ge.globalInfo.Clear();
			foreach(TradeResource.Types t in System.Enum.GetValues(typeof(TradeResource.Types))){
				ge.globalInfo.Add(new TradeResource(t));
				ge.globalInfo[ge.globalInfo.Count - 1].price = 1;
			}
		}
	}
}
