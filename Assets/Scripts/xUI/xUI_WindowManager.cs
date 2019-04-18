using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_WindowManager : MonoBehaviour {
    public Canvas windowCanvas;
    public xUI_Window lastCreated;
    
    public void CreateWindow(string prefabName)
    {
        GameObject windowPrefab = Resources.Load<GameObject>("xUI_Windows\\" + prefabName);
        Vector2 spawnPos = Input.mousePosition;
        spawnPos.y -= windowPrefab.GetComponent<xUI_Window>().TopPanel.position.y;

        float screenOverlap = Screen.width - (windowPrefab.GetComponent<xUI_Window>().TopPanel.GetComponent<RectTransform>().rect.xMax + spawnPos.x);
        if (screenOverlap < 0) {
            spawnPos.x += screenOverlap;
        }

        GameObject tmp = Instantiate(windowPrefab, spawnPos, new Quaternion(0,0,0,0), windowCanvas.transform) as GameObject;
        lastCreated = tmp.GetComponent<xUI_Window>();
        //Debug.LogFormat("SpawnPos:{0}, xMax:{1}, xMin: {2}, yMax: {3}, yMin{4}", spawnPos, tmp.transform.GetChild(0).GetComponent<RectTransform>().rect.xMax, tmp.transform.GetChild(0).GetComponent<RectTransform>().rect.xMin, tmp.transform.GetChild(0).GetComponent<RectTransform>().rect.yMax, tmp.transform.GetChild(0).GetComponent<RectTransform>().rect.yMin);
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
