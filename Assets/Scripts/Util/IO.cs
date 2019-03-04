using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO {
	public static void Out<T>(List<T> p){
		Debug.Log(string.Join("", p.ConvertAll(i => i.ToString()).ToArray()));
	}
}
