using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_Logic : MonoBehaviour {
	public void TestFunction1(){
		print("Test1");
	}

	public void TestFunction2(){
		print("Test2");
	}

	public void PrintString(string[] s){
		print(s[0]);
	}

	public void Sqr(string[] a){
		int i = System.Convert.ToInt32(a[0]);
		print(i * i);
	}

	public void Add(string[] args){
		int a = System.Convert.ToInt32(args[0]);
		int b = System.Convert.ToInt32(args[1]); 
		print(a + b);
	}
}
