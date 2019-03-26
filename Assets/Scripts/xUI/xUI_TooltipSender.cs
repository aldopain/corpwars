using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(xUI_PointerEvents))]
public class xUI_TooltipSender : MonoBehaviour {
    [Multiline]
    public string TooltipMessage;

    xUI_PointerEvents trigger;
    xUI_Tooltip tooltip;

	// Use this for initialization
	void Start () {
        tooltip = GameObject.Find("Canvas_Tooltips").GetComponentInChildren<xUI_Tooltip>();
        SetupTrigger();
	}

    void SetupTrigger()
    {
        trigger = GetComponent<xUI_PointerEvents>();
        SetupOnEnter();
        SetupOnExit();
    }

    void SetupOnEnter()
    {
        trigger.OnMouseEnterEvent.AddListener(ShowTooltip);
    }

    void SetupOnExit()
    {
        trigger.OnMouseExitEvent.AddListener(tooltip.Hide);
    }

    void ShowTooltip()
    {
        print("Fuck");
        tooltip.Show(TooltipMessage);
    }
}
