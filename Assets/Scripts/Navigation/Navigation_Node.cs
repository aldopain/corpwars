using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation_Node : MonoBehaviour
{
    public List<Navigation_Node> Neighbours;

    void Awake() {

    }

    void Start() {

    }

    void Update() {

    }

    // TODO - check if node is by enemy control or city
    public float SafeDistance(Navigation_Node p) {
        if(Neighbours.IndexOf(p) != -1)
            return Vector3.Distance(transform.position, p.transform.position);
        return Mathf.Infinity;
    }

    public float Distance(Navigation_Node p) {
        return Vector3.Distance(transform.position, p.transform.position);
    }
}
