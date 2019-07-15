using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Move : MonoBehaviour
{
    public Navigation_Node start, end;
    Rigidbody rb;
    Navigation_Route route;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        var ways = new List<Navigation_Way>(){ GameObject.Find("PathFinder").GetComponent<Navigation_PathFinder>().GetWay(start, end) };
        route = new Navigation_Route(ways, false);
    }

    void Move(Transform target) {
        rb.MovePosition(transform.position + Vector3.Normalize(target.position - transform.position) * Time.deltaTime);
    }

    void TryToMove() {
        var target = route.GetCurrentTarget();
        if(Vector3.Distance(transform.position, target.position) > 0.01f || route.IsNextTargetExists()) {
            Move(target);
        }
    }

    void FixedUpdate() {
        TryToMove();
    }
}
