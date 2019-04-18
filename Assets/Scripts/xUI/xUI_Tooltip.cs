using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_Tooltip : MonoBehaviour {
    public RectTransform Panel;
    public Text TooltipGO;

	// Use this for initialization
	void Start () {
        Disable();
    }
	
	// Update is called once per frame
	void Update () {
        MoveToMousePosition();
	}

    public void Show(string tooltip)
    {
        ResizeToText(tooltip);
        TooltipGO.text = tooltip;
        Enable();
    }

    public void Hide()
    {
        Disable();
    }

    public void SetText(string text)
    {
        TooltipGO.text = text;
    }

    void Enable()
    {
        GetComponentInParent<Canvas>().enabled = true;
    }

    void Disable()
    {
        GetComponentInParent<Canvas>().enabled = false;
    }

    void MoveToMousePosition()
    {
        Vector2 pos = Input.mousePosition;
        pos.x += TooltipGO.rectTransform.sizeDelta.x/2;
        pos.y -= TooltipGO.rectTransform.sizeDelta.y;
        transform.position = pos;
    }

    void ResizeToText(string text)
    {
        Panel.sizeDelta = FindBackgroundDimensions(text) * TooltipGO.fontSize;
        TooltipGO.rectTransform.sizeDelta = FindBackgroundDimensions(text) * TooltipGO.fontSize;
    }

    Vector2 FindBackgroundDimensions(string text)
    {
        return new Vector2(text.Length, 3);
    }
}
