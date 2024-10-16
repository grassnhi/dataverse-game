using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int number;
    public Vector2 worldPosition;
    public bool walkable;
    public Node(int num, Vector2 worldPos, bool _walkable) {
        number = num;
        worldPosition = worldPos;
        walkable = _walkable;
    }
}
