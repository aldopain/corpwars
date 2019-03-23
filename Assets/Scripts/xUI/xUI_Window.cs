using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_Window : MonoBehaviour {
    public Button CloseButton;
    public RectTransform TopPanel;
    public RectTransform ContentPanel;
    //[HideInInspector]
    public xUI_WindowManager manager;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameController").GetComponent<xUI_WindowManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowOnTop()
    {
        manager.PutOnTop(this);
    }

    public void DragWindow()
    {
        ShowOnTop();
        Vector2 pos = Input.mousePosition;
        pos.y += transform.position.y - TopPanel.position.y;
        transform.position = pos;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
