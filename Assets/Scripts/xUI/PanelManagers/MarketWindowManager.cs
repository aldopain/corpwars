using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketWindowManager : MonoBehaviour {
    private Resource_GlobalList gl;
    public xUI_ButtonPanel[] Panels;

	// Use this for initialization
	void Start () {
        gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
        for(int i = 0; i < gl.ResourcesList.Length; i++)
        {
            Panels[i].Set(gl.ResourcesList[i]);
            Panels[i].Content.text = "Ты пидор!";
        }   	
	}

}
