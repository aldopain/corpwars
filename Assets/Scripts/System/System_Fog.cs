using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class System_Fog : MonoBehaviour {
     
    public GameObject m_fogOfWarPlane;
    public Player_Manager player;
    public LayerMask m_fogLayer;
    public float m_radius = 2f;
    private float m_radiusSqr;
     
    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;
    public int[] indexes;
    private float defaultAlpha;
     
    // Use this for initialization
    void Start () {
        Initialize();
    }
     
    // Update is called once per frame
    void Update () {
        m_radiusSqr = m_radius * m_radius;
        indexes = new int[m_colors.Length];
        foreach (var unit in player.caravans)
        {
            var m = unit.GetComponent<Actor_CaravanMovement>();
            var points = new List<NavigationNode>();
            points.Add(m.currentRoutePoint);
            if (!m.prevRoutePoint.Equals(m.currentRoutePoint)) {
                points.Add(m.prevRoutePoint);
            } else {
                foreach (var point in m.currentRoutePoint.Neighbours) {
                    points.Add(point);
                }
            }
            foreach (var node in points) {
                Ray r = new Ray(node.transform.position, Vector3.up);
                RaycastHit hit;
                if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide)) {
                    for (int i = 0; i < m_vertices.Length; i++) {
                        Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                        float dist = Vector3.SqrMagnitude(v - hit.point);
                        if (dist < m_radiusSqr) {
                            indexes[i] = 1;
                        }
                    }
                }
            }
            for (int i = 0; i < m_colors.Length; i++) {
                if (indexes[i] == 1){
                    m_colors[i].a = 0f;
                } else {
                    m_colors[i].a = defaultAlpha;                
                }
            }
            UpdateColor();
        }
    }

    void Initialize() {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i=0; i < m_colors.Length; i++) {
            m_colors[i] = Color.black;
        }
        defaultAlpha = m_colors[0].a;
        UpdateColor();
    }
     
    void UpdateColor() {
        m_mesh.colors = m_colors;
    }
}