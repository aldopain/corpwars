using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class System_SelectionManager : MonoBehaviour {
    public string[] RaycastTargets;

    public UnityEvent OnNewSelect;
    public UnityEvent OnNewMouseOver;

    GameObject selectedObject;
    GameObject mouseOverObject;

    Camera cam;

    UI_Tooltip activeTooltip;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        GetMouseInputs();

        if (Input.GetMouseButtonDown(1))
        {
            SetSelected(null);
        }
    }

    void GetMouseInputs()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && isRaycastTarget(hit.collider.tag))
        {
            SetMouseOver(hit.collider.gameObject);
            //ShowTooltip(hit.collider.gameObject, cam.WorldToScreenPoint(hit.point));
        }
        else
        {
            SetMouseOver(null);
            //ShowTooltip(null,Vector2.zero);
        }

        if (Input.GetMouseButtonUp(0) && mouseOverObject)
        {
            SetSelected(mouseOverObject);
        }
    }

    /// <summary>
    /// Sets selected object
    /// </summary>
    /// <param name="go">New selected object</param>
    void SetSelected(GameObject go)
    {
        if(go != selectedObject)
        {
            OnNewSelect.Invoke();
            Clear(selectedObject);
            selectedObject = go;
            Highlight_Selected();
        }
    }

    /// <summary>
    /// Sets mouse over object
    /// </summary>
    /// <param name="go">Mouse over object</param>
    void SetMouseOver(GameObject go)
    {
        if (mouseOverObject != selectedObject)
        {
            OnNewMouseOver.Invoke();
            Clear(mouseOverObject);
        }
        mouseOverObject = go;
        if (mouseOverObject != selectedObject) Highlight_MO();
    }

    /// <summary>
    /// Sets GameObject's color
    /// </summary>
    /// <param name="go">Object</param>
    /// <param name="_color">Color</param>
    void Highlight(GameObject go, Color _color)
    {
        if(go) go.GetComponent<Renderer>().material.SetColor("_Color", _color);
    }

    /// <summary>
    /// Sets GameObject's color to white
    /// </summary>
    /// <param name="go">Object</param>
    void Clear(GameObject go)
    {
        if(go) go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    void ShowSelectionMenu()
    {

    }

    void ShowTooltip(GameObject go, Vector2 position)
    {
        UI_Tooltip tt;

        if (go && (tt = go.GetComponent<UI_Tooltip>()))
        {
            if (!activeTooltip)
            {
                activeTooltip = tt;
            }
            else if(tt != activeTooltip)
            {
                activeTooltip.Hide();
                activeTooltip = tt;
            }
        
            activeTooltip.Show(position);
        }
        else
        {
            if (activeTooltip)
            {
                activeTooltip.Hide();
                activeTooltip = null;
            }
        }
    }

    void Highlight_MO()
    {
        if(mouseOverObject) mouseOverObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

    void Highlight_Selected()
    {
        if (selectedObject) selectedObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }

    void Clear_MO()
    {
        if (mouseOverObject) mouseOverObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    void Clear_Selected()
    {
        if (selectedObject) selectedObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    /// <summary>
    /// Checks if tag is a Raycast target
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    bool isRaycastTarget(string tag)
    {
        foreach(string s in RaycastTargets)
        {
            if (s == tag) return true;
        }

        return false;
    }
}
