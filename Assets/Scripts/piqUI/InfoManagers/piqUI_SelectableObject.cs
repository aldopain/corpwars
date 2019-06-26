using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class piqUI_SelectableObject : MonoBehaviour
{
    private piqUI_InformationManager manager;
    //Events
    public UnityEvent OnClick;

    //Initialize manager
    void Start() {
        manager = GameObject.Find("piqUI").GetComponent<piqUI_InformationManager>();
    }

    void OnMouseOver() {
        //TODO: Mouseover
        //...
        manager.selection.SetHighlightedObject(gameObject);
        
        //Select
        if (Input.GetMouseButtonDown(0)) {          //Left click
            manager.selection.SetSelectedObject(gameObject);
            OnClick.Invoke();
        }
    }
}
