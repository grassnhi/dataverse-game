using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool blocked;
    public Vector2Int coords;
    GridManager gridManager;
    void Start()
    {
        SetCoords();
        if (blocked) {
            gridManager.BlockNode(coords);
        }
    }
    private void SetCoords() {
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        coords = new Vector2Int(x/gridManager.UnitGridSize, y/gridManager.UnitGridSize);
    }
}
