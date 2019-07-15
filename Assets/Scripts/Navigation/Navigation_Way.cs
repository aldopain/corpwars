using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Navigation_Way
{
    public List<Navigation_Node> points;

    public Navigation_Way(List<Navigation_Node> p) {
        points = p;
    }

    // TODO - research, why String.Join is old in Unity
    public override string ToString() {
        return String.Join(" - ", points.Select(x => x.name).ToArray());
    }
}
