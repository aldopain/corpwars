using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xUI_WindowOpener : MonoBehaviour {
    public string WindowName;
    public List<string> Tags;
    public xUI_Window LinkedWindow;

    public void CreateWindow() {
        xUI_WindowManager wm = GameObject.Find("GameController").GetComponent<xUI_WindowManager>();
        wm.CreateWindow(WindowName);
        wm.lastCreated.SenderTags = Tags;

        if (LinkedWindow != null) {
            Vector2 pos;
            pos.x = LinkedWindow.transform.position.x + LinkedWindow.TopPanel.GetComponent<RectTransform>().rect.xMax + wm.lastCreated.TopPanel.GetComponent<RectTransform>().rect.xMax;
            pos.y = LinkedWindow.TopPanel.transform.position.y - LinkedWindow.TopPanel.GetComponent<RectTransform>().rect.yMax/2 - LinkedWindow.ContentPanel.GetComponent<RectTransform>().rect.yMax/2;     //Fix incorrect placement
            wm.lastCreated.transform.position = pos;
            wm.lastCreated.transform.SetParent(LinkedWindow.transform);
            wm.lastCreated.SetDraggable(false);
        }
    }
}
