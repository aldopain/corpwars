using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {
	public LayerMask MouseOverMask;
	private Unit selectedUnit;
	private GameObject MouseOverObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetMouseOverObject();

		if(Input.GetMouseButtonUp(1) && selectedUnit != null && MouseOverObject != null){
			selectedUnit.Goto(MouseOverObject.transform.position, true);
		}		
	}

	public void SetSelectedUnit(Unit _new){
		selectedUnit = _new;
	}

	void GetMouseOverObject(){
 		RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MouseOverMask)) {
			MouseOverObject = hit.collider.gameObject;

			if(Input.GetMouseButtonUp(0)){
				if(hit.collider.tag == "Unit"){
					selectedUnit = hit.collider.GetComponent<Unit>();
				}
			}

        }else{
			MouseOverObject = null;

			if(Input.GetMouseButtonUp(0)){
				selectedUnit = null;
			}			
		}
	}
}
