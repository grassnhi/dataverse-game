using System;
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
    Heap<Node> frontier_AStar;
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    public int mode = 1;

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
            frontier_AStar = new Heap<Node>(gridManager.MaxSize);
        }
    }
    public List<Node> GetNewPath() {
        return GetNewPath(startCoords);
    }
    public List<Node> GetNewPath(Vector2Int startCoords) {
        gridManager.ResetNodes();
        if (mode <= 1) BreadthFirstSearch(startCoords);     // mode = 1: bfs
        else if (mode == 2) DepthFirstSearch(startCoords);  // mode = 2: dfs
        else AStarSearch(startCoords);  // mode = 3: A-Star
        return BuildPath();
    }
    void BreadthFirstSearch(Vector2Int coords) {
        startNode.walkable = true;
        targetNode.walkable = true;

        frontier_BFS.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier_BFS.Enqueue(grid[coords]);
        reached.Add(coords, grid[coords]);

        while (frontier_BFS.Count > 0 && isRunning == true) {
            currentNode = frontier_BFS.Dequeue();
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
    void AStarSearch(Vector2Int coords) {
        startNode.walkable = true;
        targetNode.walkable = true;
        
        frontier_AStar.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier_AStar.Add(grid[coords]);
        reached.Add(coords, grid[coords]);

        while (frontier_AStar.Count > 0 && isRunning == true) {
            currentNode = frontier_AStar.RemoveFirst();
            currentNode.explored = true;
            ExploreNeighbors_AStar();
            if (currentNode.coords == targetCoords) {
                isRunning = false;
                currentNode.walkable = false;
            }
        }
    }
    void ExploreNeighbors_AStar() {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in searchOrder) {
            Vector2Int neighborCoords = currentNode.coords + direction;
            if (grid.ContainsKey(neighborCoords)) {
                neighbors.Add(grid[neighborCoords]);
            }
        }
        foreach (Node neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.walkable) {
                int newCost = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newCost < neighbor.gCost || !frontier_AStar.Contains(neighbor)) {
                    neighbor.gCost = newCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.connectTo = currentNode;
                    reached.Add(neighbor.coords, neighbor);
                    frontier_AStar.Add(neighbor);
                }
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
    int GetDistance(Node node1, Node node2) {
        int distX = (int)Mathf.Abs(node1.coords.x-node2.coords.x);
        int distY = (int)Mathf.Abs(node1.coords.y-node2.coords.y);
        return (int)Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));
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
