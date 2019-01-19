using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Resource", menuName = "Resource")]
public class Resource:ScriptableObject{
	public string Name;
	[Multiline]
	public string Description;
	public Resource_ConvertionRecipe Recipe;
    public double ConvertionOutput = 1; //how many of this resource is created on each convertion
}
