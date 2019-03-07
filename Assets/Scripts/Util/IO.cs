﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO {
	public static void Out<T>(List<T> p){
		Out(p, " ");
	}

	public static void Out<T>(List<T> p, string separator){
		Debug.Log(ListToString(p, separator));
	}

	public static string ListToString<T>(List<T> p){
		return ListToString(p, " ");
	}

	public static string ListToString<T>(List<T> p, string separator){
		var s = string.Join(separator, p.ConvertAll(i => i.ToString()).ToArray());
		return s;
	}
}