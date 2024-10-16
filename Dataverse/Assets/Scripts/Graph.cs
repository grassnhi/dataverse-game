using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 graphSize;
    public float nodeRadius;
    Node[,] graph;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(graphSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(graphSize.y / nodeDiameter);
        CreateGraph();
    }

    void CreateGraph()
    {
        graph = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * graphSize.x/2 - Vector3.up * graphSize.y/2;
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 worldPoint = worldBottomLeft + Vector3.right*(x*nodeDiameter+nodeRadius) + Vector3.up*(y*nodeDiameter+nodeRadius);
                // bool walkable = !Physics2D.Col
            }
        }
    }
}
