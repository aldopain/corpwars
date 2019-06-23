using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Add text formatting in properties
public class piqUI_ResourceLine : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _Amount;
    [SerializeField] private Text _ProductionProfit;
    [SerializeField] private Text _ProductionLoss;
    [SerializeField] private Text _TradeProfit;
    [SerializeField] private Text _TradeLoss;

    public string Name {
        get {
            return _name.text;
        }

        set {
            _name.text = value;
        }
    }

    public string Amount {
        get {
            return _Amount.text;
        }

        set {
            _Amount.text = value;
        }
    }

    public string ProductionProfit {
        get {
            return _ProductionProfit.text;
        }

        set {
            _ProductionProfit.text = value;
        }
    }

    public string ProductionLoss {
        get {
            return _ProductionLoss.text;
        }

        set {
            _ProductionLoss.text = value;
        }
    }

    public string TradeProfit {
        get {
            return _TradeProfit.text;
        }

        set {
            _TradeProfit.text = value;
        }
    }

    public string TradeLoss {
        get {
            return _TradeLoss.text;
        }

        set {
            _TradeLoss.text = value;
        }
    }
}
