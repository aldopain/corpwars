using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class System_SelectionManager : MonoBehaviour {
    public UnityEvent OnNewSelect;
    public UnityEvent OnNewMouseOver;

    GameObject selectedObject;
    GameObject mouseOverObject;

    Camera cam;

    UI_Tooltip activeTooltip;
    UI_UnitCommand unitCommand;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        unitCommand = GameObject.Find("Canvas_UnitControl").GetComponent<UI_UnitCommand>();
	}

    void Update(){
        if((!mouseOverObject && Input.GetMouseButtonDown(1)) || Input.GetKeyDown(KeyCode.Escape)){
            if(selectedObject){
                selectedObject.GetComponent<System_SelectableObject>().Deselect();
                selectedObject = null;
            }
        }
    }

    /// <summary>
    /// Sets selected object
    /// </summary>
    /// <param name="go">New selected object</param>
    public void SetSelected(GameObject go)
    {
        if (go == null)
        {
            selectedObject = null;
            unitCommand.Disable();
            return;
        }
        if(go != selectedObject)
        {
            OnNewSelect.Invoke();
            if (selectedObject)
            {
                selectedObject.GetComponent<System_SelectableObject>().Deselect();      
            }
            unitCommand.Enable(go);
            selectedObject = go;
        }
    }

    /// <summary>
    /// Sets mouse over object
    /// </summary>
    /// <param name="go">Mouse over object</param>
    public void SetMouseOver(GameObject go)
    {
        if (mouseOverObject != selectedObject)
        {
            OnNewMouseOver.Invoke();
        }
        mouseOverObject = go;
    }
}
