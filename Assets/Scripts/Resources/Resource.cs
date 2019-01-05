using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource{
	public string Name;
	[Multiline]
	public string Description;
	public Resource_ConvertionRecipe Recipe;
}
