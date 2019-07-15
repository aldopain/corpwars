using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation_Route
{
    List<Navigation_Way> ways;
    bool isLooped;
    int currentWayIndex, currentNodeIndex;

    public Navigation_Route(List<Navigation_Way> w, bool l){
        ways = w;
        currentNodeIndex = 0;
        currentWayIndex = 0;
        isLooped = l;
    }

    public Transform GetCurrentTarget() {
        return ways[currentWayIndex].points[currentNodeIndex].transform;
    }

    public bool IsNextTargetExists() {
        if (++currentNodeIndex != ways[currentWayIndex].points.Count) {
            return true;
        } else if (++currentWayIndex != ways.Count) {
            if (isLooped) {
                currentNodeIndex = 0;
                currentWayIndex = 0;
                return true;
            } else {
                return false;
            }
        } else {
            currentNodeIndex = 0;
            return true;
        }
    }
}
