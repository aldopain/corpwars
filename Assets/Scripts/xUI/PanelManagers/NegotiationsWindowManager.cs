using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NegotiationsWindowManager : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        GetComponent<xUI_Window>().SetLabel("Negotiations with " + GetComponent<xUI_Window>().SenderTags[0]);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
