using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_WindowManager : MonoBehaviour {
    public Canvas windowCanvas;
        
    public void CreateWindow(string prefabName)
    {
        GameObject windowPrefab = Resources.Load<GameObject>("xUI_Windows\\" + prefabName);
        Vector2 spawnPos = Input.mousePosition;
        spawnPos.y -= windowPrefab.GetComponent<xUI_Window>().TopPanel.position.y;
        GameObject tmp = Instantiate(windowPrefab, spawnPos, new Quaternion(0,0,0,0), windowCanvas.transform) as GameObject;
    }

    public void ClearAllWindows()
    {
        for(int i = 0; i < windowCanvas.transform.childCount; i++)
        {
            windowCanvas.transform.GetChild(i).GetComponent<xUI_Window>().Close();
        }
    }

    public void PutOnTop(xUI_Window window)
    {
        window.transform.SetAsLastSibling();
    }
}
