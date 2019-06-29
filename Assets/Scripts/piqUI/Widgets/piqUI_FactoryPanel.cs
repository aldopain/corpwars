using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piqUI_FactoryPanel : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _type;
    [SerializeField] private Text _input;
    [SerializeField] private Text _output;
    [SerializeField] private Image _statusImage;

    public string Name {
        get {
            return _name.text;
        }

        set {
            _name.text = value;
        }
    }

    public Type FactoryType {
        set {
            _type.text = System.Enum.GetName(typeof(Type), value);
        }
    }

    public List<string> Input {
        get {     
            return new List<string>(_input.text.Split('\n'));
        }
    }

    public List<string> Output {
        get {
            return new List<string>(_output.text.Split('\n'));
        }
    }

    public void ClearInput() {
        _input.text = "";
    }

    public void ClearOutput() {
        _output.text = "";
    }

    public void AddInput(string s) {
        _input.text += s;
        if (s[s.Length - 1] != '\n')
            _input.text += '\n';
    }

    public void AddOutput(string s) {
        _output.text += s;
        if (s[s.Length - 1] != '\n')
            _output.text += '\n';
    }
}
