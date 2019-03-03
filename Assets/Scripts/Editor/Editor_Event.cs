using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Events_Base))]
public class Editor_Event : Editor {
	int buttonLength = 0;
	int buttonLengthPrev = 0;

	void OnEnable(){
		Events_Base editTarget = (Events_Base)target;

		buttonLength = editTarget.Buttons.Length;
		buttonLengthPrev = buttonLength;
	}

	public override void OnInspectorGUI(){
		Events_Base editTarget = (Events_Base)target;

		GUI_ShowVisuals(editTarget);
		EditorGUILayout.LabelField("Buttons", EditorStyles.boldLabel);
		GetButtonLength(editTarget);

		for(int i = 0; i < editTarget.Buttons.Length; i++){
			GUI_ShowButtonEditor(editTarget.Buttons[i]);
		}

	}
	
	void GUI_ShowVisuals(Events_Base editTarget){
		EditorGUILayout.LabelField("Visuals", EditorStyles.boldLabel);
        GUILayout.Space(64);
        Rect spriteRect = new Rect(Screen.width - 70, 64, 64, 64);
        editTarget.Image = (Sprite)EditorGUI.ObjectField(spriteRect, editTarget.Image, typeof(Sprite), false);

        editTarget.Label = EditorGUILayout.TextField("Label", editTarget.Label);
        EditorGUILayout.LabelField("Description");
        editTarget.Description = EditorGUILayout.TextArea(editTarget.Description);
	}

	void GUI_ShowButtonEditor(Events_ChoiceButton editTarget){
		editTarget.Label = EditorGUILayout.DelayedTextField(editTarget.Label);
		editTarget.Tooltip = EditorGUILayout.TextArea(editTarget.Tooltip);
		editTarget.Action = EditorGUILayout.TextArea(editTarget.Action);
		EditorGUILayout.Space();
	}

	void GetButtonLength(Events_Base editTarget){
		buttonLength = EditorGUILayout.DelayedIntField("Choices Amount", buttonLength);
		if(buttonLength != buttonLengthPrev){
			System.Array.Resize(ref editTarget.Buttons, buttonLength);
			for(int i = buttonLengthPrev; i < buttonLength; i++){
				editTarget.Buttons[i] = new Events_ChoiceButton();
			}
		}
		buttonLengthPrev = buttonLength;
	}
}
