using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tooltip : MonoBehaviour {
    [Multiline]
    public string TooltipText;
    public string ParentCanvas = "Canvas";
    public int fontSize = 14;
    GameObject textGO;
    GameObject backgroundGO;

    bool isDestroying;

    void OnMouseOver(){
        Show(Input.mousePosition);
    }

    void OnMouseExit(){
        Hide();
    }

    public void Show(Vector2 position)
    {
        if (textGO && backgroundGO)
        {
            textGO.GetComponent<RectTransform>().position = position;
            textGO.SetActive(true);

            backgroundGO.GetComponent<RectTransform>().position = position;
            backgroundGO.GetComponent<RectTransform>().sizeDelta = FindBackgroundDimensions() * fontSize;
            backgroundGO.SetActive(true);
        }
        else
        {
            Create();
        }
    }

    public void Hide()
    {
        textGO.SetActive(false);
        backgroundGO.SetActive(false);
    }

    void Create()
    {
        CreateBackground();
        CreateText();
    }

    void Destroy()
    {
        Destroy(textGO);
        Destroy(backgroundGO);
    }

    void CreateText()
    {
        if (!textGO)
        {
            textGO = new GameObject();
            textGO.SetActive(false);
            textGO.name = name + " Tooltip Text";
            textGO.transform.parent = GameObject.Find(ParentCanvas).transform;
            textGO.AddComponent(typeof(RectTransform));
            textGO.AddComponent(typeof(CanvasRenderer));
            textGO.AddComponent(typeof(Text));

            Text t = textGO.GetComponent<Text>();
            t.text = TooltipText;
            t.font = Font.CreateDynamicFontFromOSFont("Arial", fontSize);
            t.alignment = TextAnchor.MiddleCenter;
        }
    }

    void CreateBackground()
    {
        if (!backgroundGO)
        {
            backgroundGO = new GameObject();
            backgroundGO.SetActive(false);
            backgroundGO.name = name + " Tooltip Background";
            backgroundGO.transform.parent = GameObject.Find(ParentCanvas).transform;
            backgroundGO.AddComponent(typeof(RectTransform));
            backgroundGO.AddComponent(typeof(CanvasRenderer));
            backgroundGO.AddComponent(typeof(Image));

            Image i = backgroundGO.GetComponent<Image>();
            i.color = new Color(0,0,0);
        }
    }

    Vector2 FindBackgroundDimensions()
    {
        string[] tt = TooltipText.Split('\n');
        int maxLen = 0;
        foreach(string s in tt)
        {
            if (s.Length > maxLen) maxLen = s.Length;
        }

        return new Vector2(maxLen, tt.Length);
    }
}
