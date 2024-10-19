using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unitGridSize;
    public int UnitGridSize {
        get {
            return unitGridSize;
        }
    }
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid {
        get {
            return grid;
        }
    }
    private void Awake()
    {
        CreateGrid();
    }
    public Node GetNode(Vector2Int coords) {
        if (grid.ContainsKey(coords)) {
            return grid[coords];
        }
        return null;
    }
    public void BlockNode(Vector2Int coords) {
        if (grid.ContainsKey(coords)) {
            grid[coords].walkable = false;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(unitGridSize, unitGridSize, unitGridSize);
            cube.transform.position = GetPositionFromCoordinates(coords);
        }
    }
    public void ResetNodes() {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid) {
            entry.Value.connectTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
        }
    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coords) {
        Vector3 position = new Vector3();
        position.x = coords.x * unitGridSize;
        position.y = coords.y * unitGridSize;
        return position;
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
        Vector2Int coords = new Vector2Int();
        coords.x = (int)position.x / unitGridSize;
        coords.y = (int)position.y / unitGridSize;
        return coords;
    }
    public void CreateGrid() {
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Vector2Int coords = new Vector2Int(x, y);
                grid.Add(coords, new Node(coords, true));
            }
        }
    }
}
