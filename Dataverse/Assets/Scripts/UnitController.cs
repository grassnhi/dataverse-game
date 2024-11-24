using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static int flag;
    [SerializeField] float movementSpeed = 1f;
    Transform selectedUnit;
    bool unitSelected = false;
    List<Node> path = new List<Node>();
    GridManager gridManager;
    PathFinding pathFinder;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                flag = 1;
                Debug.Log("Hit detected! " + hit.transform.tag);
                if (hit.transform.CompareTag("Tile")) {
                    flag = 2;
                    if (unitSelected) {
                        Vector2Int targetCoords = hit.transform.GetComponent<Labeller>().Coords;
                        if (gridManager.GetNode(targetCoords).walkable) {
                            Vector2Int startCoords = new Vector2Int((int) selectedUnit.position.x, (int) selectedUnit.position.y) / gridManager.UnitGridSize;
                            pathFinder.SetNewDestination(startCoords, targetCoords);
                            RecalculatePath(true);
                        }
                    }
                }
                if (hit.transform.CompareTag("Unit")) {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            } else {
                Debug.Log("Hit not detected!");
            }
        }
    }
    void RecalculatePath(bool resetPath) {
        Vector2Int coords = new Vector2Int();
        if (resetPath) {
            coords = pathFinder.StartCoords;
        }
        else {
            coords = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coords);
        StartCoroutine(FollowPath());
    }
    IEnumerator FollowPath() {
        for (int i = 1; i < path.Count; i++) {
            Vector3 startPos = selectedUnit.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coords);
            float travelPercent = 0f;

            // selectedUnit.LookAt(endPos);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * movementSpeed;
                selectedUnit.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
