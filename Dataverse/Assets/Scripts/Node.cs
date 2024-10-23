using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Vector2Int coords;
    public bool walkable;
    public bool explored;
    public bool path;
    public Node connectTo;
    public int gCost;
    public int hCost;
    public int fCost {
        get {
            return gCost + hCost;
        }
    }
    int heapIndex;
    public Node(Vector2Int coords, bool walkable)
    {
        this.coords = coords;
        this.walkable = walkable;
    }
    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }
    public int CompareTo(Node other) {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
