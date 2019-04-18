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
    public List<string> SenderTags;
    public bool isStatic;
    Vector2 mouseDragOffset;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameController").GetComponent<xUI_WindowManager>();
	}
	
    public void ShowOnTop()
    {
        manager.PutOnTop(this);
    }

    public void SetOffset()
    {
        mouseDragOffset = Input.mousePosition - TopPanel.position;
    }

    public void DragWindow()
    {
        if (isStatic)
            return;

        ShowOnTop();
        Vector2 pos = Input.mousePosition;
        pos.y += transform.position.y - TopPanel.position.y;

        /*  Uncomment for a buggy implementation of screen escaping prevention
         * 
         *         Vector2 overlap = GetOverlap();
         *         if (overlap.x < 0)
         *              pos.x += overlap.x;
         *          if (overlap.y < 0)
         *              pos.y += overlap.y;
         */


        transform.position = pos - mouseDragOffset;
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    Vector2 GetOverlap() {
        Vector2 ol;
        ol.x = Screen.width - (TopPanel.GetComponent<RectTransform>().rect.xMax + TopPanel.position.x);
        ol.y = Screen.height - (TopPanel.GetComponent<RectTransform>().rect.yMax + TopPanel.position.y);
        return ol;
    }

    public void SetLabel(string label) {
        TopPanel.GetComponentInChildren<Text>().text = label;
    }

    public void SetDraggable(bool d) {
        isStatic = !d;
        TopPanel.GetComponent<Image>().color = Color.grey;
    }
}
