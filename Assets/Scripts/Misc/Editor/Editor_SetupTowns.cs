using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_SetupTowns : MonoBehaviour {

	[MenuItem("Towns/Set Resources/Empty")]
	private static void SetupResources_All(){
		GameObject[] towns = GameObject.FindGameObjectsWithTag("TradePost");
		foreach(GameObject go in towns){
			if(go.GetComponent<LocalEconomy>().resources.Count != System.Enum.GetValues(typeof(TradeResource.Types)).Length){
				go.GetComponent<LocalEconomy>().resources.Clear();
				go.GetComponent<LocalEconomy>().SetResourcesList();
			}
		}
	}

	[MenuItem("Towns/Set Resources/Empty (Selected)")]
	private static void SetupResources_Selected(){
		GameObject[] towns = Selection.gameObjects;
		foreach(GameObject go in towns){
			if(go.tag == "TradePost" && go.GetComponent<LocalEconomy>().resources.Count != System.Enum.GetValues(typeof(TradeResource.Types)).Length){
				go.GetComponent<LocalEconomy>().resources.Clear();
				go.GetComponent<LocalEconomy>().SetResourcesList();
			}
		}
	}

	[MenuItem("Towns/Set Resources/Random")]
	private static void SetupRandomResources_All(){
		SetupResources_All();
		GameObject[] towns = GameObject.FindGameObjectsWithTag("TradePost");
		foreach(GameObject go in towns){
			foreach(TradeResource r in go.GetComponent<LocalEconomy>().resources){
					r.price = Random.Range(100, 500);
					r.stock = Random.Range(0, 50);
					r.produced = Random.Range(r.stock, 100);
					r.bought = Random.Range(r.stock, 100);
					r.sold = r.produced + r.bought - r.stock;
			}
		}
	}

	[MenuItem("Towns/Set Resources/Random (Selected)")]
	private static void SetupRandomResources_Selected(){
		SetupResources_Selected();
		GameObject[] towns = Selection.gameObjects;
		foreach(GameObject go in towns){
			if(go.tag == "TradePost"){
				foreach(TradeResource r in go.GetComponent<LocalEconomy>().resources){
					r.price = Random.Range(100, 500);
					r.stock = Random.Range(0, 50);
					r.produced = Random.Range(r.stock, 100);
					r.bought = Random.Range(r.stock, 100);
					r.sold = r.produced + r.bought - r.stock;
				}
			}
		}

	}

	private static void AddNeigbour(){

	}
}
