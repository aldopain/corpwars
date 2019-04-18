using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_ButtonPanel : MonoBehaviour {
    public Text Label;
    public Text Content;
    public Button Button;
    public Image AttachedImage;

	// Use this for initialization
	void Start () {
        Button = GetComponent<Button>();	
	}

    public void Set(string _label, string _content, Sprite _image)
    {
        Label.text = _label;
        Content.text = _content;
        AttachedImage.sprite = _image;

    }

    public void Set(string _label, string _content)
    {
        Label.text = _label;
        Content.text = _content;
    }

    public void Set(string _label)
    {
        Label.text = _label;
        Content.text = "";
    }

    public void Set(Resource r)
    {
        Label.text = r.Name;
        Content.text = r.Description;
        AttachedImage.sprite = r.Image;
    }
}
