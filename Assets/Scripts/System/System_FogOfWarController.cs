using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class System_FogOfWarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = GameObject.FindGameObjectWithTag("Caravan").transform.position;
        Renderer ren = GetComponent<Renderer>();
        ren.sharedMaterial.SetVector("_Position",v);
        ren.sharedMaterial.SetFloat("_Radius", 3);

        //material.SetVector("_Position", v);
        //fogOfWar.GetComponent<MeshRenderer>().materials[0].SetVector("_Position", v);
        
    }
}
