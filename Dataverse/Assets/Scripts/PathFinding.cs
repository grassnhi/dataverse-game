using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords {
        get {
            return startCoords;
        }
    }
    
    [SerializeField] Vector2Int targetCoords;
    public Vector2Int TargetCoords {
        get {
            return targetCoords;
        }
    }
    
    Node startNode;
    Node targetNode;
    Node currentNode;
    
    Queue<Node> frontier_BFS = new Queue<Node>();
    Stack<Node> frontier_DFS = new Stack<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    public bool useBFS = true;

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Vector2Int[] searchOrder = {
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.down
    };

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager) {
            grid = gridManager.Grid;
        }
    }
    public List<Node> GetNewPath() {
        return GetNewPath(startCoords);
    }
    public List<Node> GetNewPath(Vector2Int startCoords) {
        gridManager.ResetNodes();
        if (useBFS) BreadthFirstSearch(startCoords);
        else DepthFirstSearch(startCoords);
        return BuildPath();
    }
    void BreadthFirstSearch(Vector2Int coords) {
        startNode.walkable = true;
        targetNode.walkable = true;

        frontier_BFS.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier_BFS.Enqueue(grid[coords]);
        // frontier.Push(grid[coords]);
        reached.Add(coords, grid[coords]);

        while (frontier_BFS.Count > 0 && isRunning == true) {
            currentNode = frontier_BFS.Dequeue();
            // currentNode = frontier.Pop();
            currentNode.explored = true;
            ExploreNeighbors_BFS();
            if (currentNode.coords == targetCoords) {
                isRunning = false;
                currentNode.walkable = false;
            }
        }
    }
    void DepthFirstSearch(Vector2Int coords) {
        startNode.walkable = true;
        targetNode.walkable = true;

        frontier_DFS.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier_DFS.Push(grid[coords]);
        reached.Add(coords, grid[coords]);

        while (frontier_DFS.Count > 0 && isRunning == true) {
            currentNode = frontier_DFS.Pop();
            currentNode.explored = true;
            ExploreNeighbors_DFS();
            if (currentNode.coords == targetCoords) {
                isRunning = false;
                currentNode.walkable = false;
            }
        }
    }
    void ExploreNeighbors_BFS() {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in searchOrder) {
            Vector2Int neighborCoords = currentNode.coords + direction;
            if (grid.ContainsKey(neighborCoords)) {
                neighbors.Add(grid[neighborCoords]);
            }
        }
        foreach (Node neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.walkable) {
                neighbor.connectTo = currentNode;
                reached.Add(neighbor.coords, neighbor);
                frontier_BFS.Enqueue(neighbor);
            }
        }
    }
    void ExploreNeighbors_DFS() {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in searchOrder) {
            Vector2Int neighborCoords = currentNode.coords + direction;
            if (grid.ContainsKey(neighborCoords)) {
                neighbors.Add(grid[neighborCoords]);
            }
        }
        foreach (Node neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.walkable) {
                neighbor.connectTo = currentNode;
                reached.Add(neighbor.coords, neighbor);
                frontier_DFS.Push(neighbor);
            }
        }
    }
    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        path.Add(currentNode);
        currentNode.path = true;

        while (currentNode.connectTo != null)
        {
            currentNode = currentNode.connectTo;
            path.Add(currentNode);
            currentNode.path = true;
        }
        path.Reverse();
        return path;
    }
    public void NotifyReceivers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
    public void SetNewDestination(Vector2Int startCoords, Vector2Int targetCoords) {
        this.startCoords = startCoords;
        this.targetCoords = targetCoords;
        startNode = grid[this.startCoords];
        targetNode = grid[this.targetCoords];
        GetNewPath();
    }
}
