using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavigationNode : MonoBehaviour
{
    public List<NavigationNode> Neighbours;
    public List<NavigationWay> ways;
    private int neighbourLength;

    void Awake()
    {
        neighbourLength = Neighbours.Count;
    }

    void Update()
    {
         if (neighbourLength < Neighbours.Count)
        {
            OnAddNeighbour();
            neighbourLength = Neighbours.Count;
        }

        if (neighbourLength > Neighbours.Count)
        {
            neighbourLength = Neighbours.Count;
        }
    }

    public override string ToString(){
        return gameObject.name;
    }

    public float Distance(NavigationNode p)
    {
        return Vector3.Distance(transform.position, p.transform.position);
    }

    void OnAddNeighbour()
    {
        Neighbours[neighbourLength].neighbourLength++;

        var tmp = new GameObject();
        GameObject _navLine = Instantiate(tmp, Vector3.zero, new Quaternion(0, 0, 0, 0));
        _navLine.AddComponent<NavigationLine>().Init(this, Neighbours[neighbourLength]);
        DestroyImmediate(tmp);

        Neighbours[neighbourLength].Neighbours.Add(this);
    }

    void OnRemoveNeighbour()
    {
        print("Something fucked with this node");
    }
}