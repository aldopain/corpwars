using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class xUI_Tab : MonoBehaviour {
    public Button LabelButton;
    public RectTransform Content;

    public void Enable()
    {
        LabelButton.interactable = false;
        Content.gameObject.SetActive(true);
    }

    public void Disable()
    {
        LabelButton.interactable = true;
        Content.gameObject.SetActive(false);
    }
}
