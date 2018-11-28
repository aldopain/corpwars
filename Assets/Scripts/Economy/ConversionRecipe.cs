using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversionRecipe{
	public TradeResource.Types[] input;
	public float[] inputAmount;

	public TradeResource.Types output;
	public float outputAmount;
}
