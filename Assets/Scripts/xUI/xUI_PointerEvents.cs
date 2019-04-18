using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class xUI_PointerEvents : MonoBehaviour {
    public UnityEvent OnMouseEnterEvent;
    public UnityEvent OnMouseExitEvent;
    public UnityEvent OnClick;
    public UnityEvent OnAltClick;

    void OnMouseEnter()
    {
        OnMouseEnterEvent.Invoke();
    }

    void OnMouseExit()
    {
        OnMouseExitEvent.Invoke();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) OnClick.Invoke();
        else if (Input.GetMouseButtonDown(1)) OnAltClick.Invoke();
    }
}
