using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Use xUI_PointerEvents instead")]
public class System_SelectableObject : MonoBehaviour {
	private System_SelectionManager manager;
	private System_SelectionHighlight highlight;
	private UI_Tooltip tooltip;

	private bool _isMO;
	private bool _isSelected;

	public bool isMouseOver { get{ return _isMO;}}
	public bool isSelected { 
		get{ return _isSelected;}
	}

	// Use this for initialization
	void Start () {
		if(GetComponent<UI_Tooltip>()) tooltip = GetComponent<UI_Tooltip>();
		manager = GameObject.Find("GameController").GetComponent<System_SelectionManager>();
		highlight = GetComponent<System_SelectionHighlight>();
	}
	
    /// <summary>
    /// Is called every frame when mouse is over this Selectable Object
    /// </summary>
	void OnMouseOver(){
		manager.SetMouseOver(gameObject);
        if(tooltip) tooltip.Show(Input.mousePosition);
		if(!isSelected && !isMouseOver) highlight.MouseOverHighlight();

        _isMO = true;

        if (Input.GetMouseButtonDown(0)){
			ProcessClick();
		}
		if(Input.GetMouseButtonDown(1)){
			ProcessAltClick();
		}
    }

    /// <summary>
    /// Is called when mouse leaves this Selectable Object
    /// </summary>
    void OnMouseExit(){
		_isMO = false;
		manager.SetMouseOver(null);
		if(!isSelected) highlight.Clear();
        if(tooltip) tooltip.Hide();
    }

    /// <summary>
    /// Select this object
    /// </summary>
	public void Select(){
		_isSelected = true;
		manager.SetSelected(gameObject);
		highlight.SelectionHighlight();
	}

    /// <summary>
    /// Deselect this object
    /// </summary>
	public void Deselect(){
		_isSelected = false;
		manager.SetSelected(null);
		highlight.Clear();
	}

    /// <summary>
    /// Function that is called, when player left-clicks on this Selectable Object
    /// </summary>
	void ProcessClick(){
		Select();
	}

    /// <summary>
    /// PLACEHOLDER: Function that is called, when player right-clicks on this Selectable Object
    /// </summary>
	void ProcessAltClick(){
        //Placeholder
	}
}