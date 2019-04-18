using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyWindowManager : MonoBehaviour {
    public Dropdown PlayerSelector;
    public xUI_WindowOpener WindowOpener;

	// Use this for initialization
	void Start () {
        SetTag();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTag() {
        WindowOpener.Tags[0] = PlayerSelector.options[PlayerSelector.value].text;
    }
}
