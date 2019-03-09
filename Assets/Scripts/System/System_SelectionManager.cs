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
        unitCommand.manager = this;
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
        //Deselect
        if (go == null)
        {
            ProcessDeselectedObject(selectedObject);
            selectedObject = null;
            return;
        }

        //Select
        if(go != selectedObject)
        {
            OnNewSelect.Invoke();
            if (selectedObject)
            {
                selectedObject.GetComponent<System_SelectableObject>().Deselect();      
            }
            ProcessSelectedObject(go);
            selectedObject = go;
        }
    }

    public GameObject GetSelected()
    {
        return selectedObject;
    }

    public void ProcessSelectedObject(GameObject go)
    {
        switch (go.tag)
        {
            case "Caravan":
                OnSelectUnit(go);
                break;
            case "Town":
                OnSelectTown();
                break;
            default:
                Debug.LogError("Click on " + go.name + " cannot be processed");
                break;
        }
    }

    public void ProcessDeselectedObject(GameObject go)
    {
        switch (go.tag)
        {
            case "Caravan":
                OnDeselectUnit();
                break;
            case "Town":
                OnDeselectTown();
                break;
            default:
                Debug.LogError("Deselect of " + go.name + " cannot be processed");
                break;
        }
    }

    public void OnSelectUnit(GameObject go)
    {
        unitCommand.Enable(go);
    }

    public void OnDeselectUnit()
    {
        unitCommand.Disable();
    }

    public void OnSelectTown()
    {
        //Placeholder
    }

    public void OnDeselectTown()
    {
        //Placeholder
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
