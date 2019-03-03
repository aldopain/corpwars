using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Event", menuName = "In-Game Event")]
public class Events_Base : ScriptableObject {
	public string Label;
	[Multiline]
	public string Description;
	public Sprite Image;
	public Events_ChoiceButton[] Buttons;
}
