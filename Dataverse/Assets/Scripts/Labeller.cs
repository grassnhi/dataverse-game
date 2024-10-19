using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Labeller : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coords = new Vector2Int();
    public Vector2Int Coords {
        get {
            return coords;
        }
    }
    GridManager gridManager;
    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        DisplayCoords();
    }
    private void Update() {
        DisplayCoords();
        transform.name = coords.ToString();
    }
    private void DisplayCoords() {
        if (!gridManager) return;

        coords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnitGridSize);
        coords.y = Mathf.RoundToInt(transform.position.y / gridManager.UnitGridSize);

        label.text = $"{coords.x},{coords.y}";
    }
}
