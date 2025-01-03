using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    static Vector2Int gridSize;
    Dictionary<Vector2Int, Item> grid = new Dictionary<Vector2Int, Item>();
    [SerializeField] private GameObject hint_Object;
    [SerializeField] private Button hintButton;
    [SerializeField] private GameObject hintDialogue;
    [SerializeField] private Button exitDialogueButton;
    GameObject[] item_objs;
    public int MaxSize {
        get {
            return grid.Count;
        }
    }
    public static Vector2Int GridSize {
        get {
            return gridSize;
        }
    }
    private void Start()
    {
        hintDialogue.SetActive(false);
    }
    void Awake() {
        CreateGrid();
        hintButton.onClick.AddListener(() => {
            hintDialogue.SetActive(true);
        });
        exitDialogueButton.onClick.AddListener(() => {
            hintDialogue.SetActive(false);
        });
    }
    void CreateGrid() {
        int numRows = 1;
        int numCols = transform.childCount;
        item_objs = new GameObject[numCols];
        for (int i = 0; i < transform.childCount; i++) {
            item_objs[i] = transform.GetChild(i).gameObject;
        }
        gridSize = new Vector2Int(numRows, numCols);
        for (int x = 0; x < numRows; x++) {
            for (int y = 0; y < numCols; y++) {
                int item_idx = x * numRows + y;
                if (item_idx >= item_objs.Length) {
                    break;
                }
                int itemVal = int.Parse(item_objs[item_idx].GetComponentInChildren<TMP_Text>().text);
                item_objs[item_idx].GetComponentInChildren<TMP_Text>().enabled = false;
                Vector2Int coord = new Vector2Int(x, y);
                grid.Add(coord, new Item(coord, itemVal));
            }
        }
    }
    private void Update()
    {
        if (Level.health <= 2)
        {
            hint_Object.SetActive(true);
        }
        else
        {
            hint_Object.SetActive(false);
        }
    }
}
