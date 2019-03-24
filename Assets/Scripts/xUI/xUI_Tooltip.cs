using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_Tooltip : MonoBehaviour {
    public RectTransform Panel;
    public Text TooltipGO;

	// Use this for initialization
	void Start () {
        print(Panel.sizeDelta);
	}
	
	// Update is called once per frame
	void Update () {
        print(Panel.sizeDelta);
        MoveToMousePosition();
	}

    public void Show(string tooltip)
    {
        ResizeToText(tooltip);
        TooltipGO.text = tooltip;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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
