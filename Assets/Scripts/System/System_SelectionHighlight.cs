using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class System_SelectionHighlight : MonoBehaviour {
    /// <summary>
    /// Sets selection highlight effect
    /// </summary>
    public void SelectionHighlight(){
		//GetComponent<MeshRenderer>().material.color = Color.blue;
	}

    /// <summary>
    /// Sets mouse over highlight effect
    /// </summary>
	public void MouseOverHighlight(){
		//GetComponent<MeshRenderer>().material.color = Color.green;
	}

    /// <summary>
    /// Clears all effects from this object
    /// </summary>
	public void Clear(){
		//GetComponent<MeshRenderer>().material.color = Color.white;
	}
}
