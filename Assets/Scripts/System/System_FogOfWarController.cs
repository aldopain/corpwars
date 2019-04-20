using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_FogOfWarController : MonoBehaviour {
    public Player_Manager pm;

	// Update is called once per frame
	void Update () {
        var caravans = pm.caravans;
        var vectors = new List<Vector4>();
        foreach (var caravan in caravans) {
            vectors.Add(caravan.transform.position);
        }
        Renderer ren = GetComponent<Renderer>();
        ren.sharedMaterial.SetInt("_Count", vectors.Count);
        ren.sharedMaterial.SetVectorArray("_Position", vectors);
        ren.sharedMaterial.SetFloat("_Radius", 3);

        //material.SetVector("_Position", v);
        //fogOfWar.GetComponent<MeshRenderer>().materials[0].SetVector("_Position", v);
    }
}
