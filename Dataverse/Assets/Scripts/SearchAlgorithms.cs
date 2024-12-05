using System.Collections;
using UnityEngine;
using TMPro;

public class SearchAlgorithms : MonoBehaviour
{
    int targetValue;
    public int searchMode = 0;
    int currentIndex = 0;
    int clickedIndex = -1;
    [SerializeField] GameObject itemset;
    GameObject[] item_objs;
    public static bool isComplete = false;
    void Start()
    {
        int numCols = ItemsManager.GridSize.y;
        item_objs = new GameObject[numCols];
        for (int i = 0; i < itemset.transform.childCount; i++) {
            item_objs[i] = itemset.transform.GetChild(i).gameObject;
        }
        targetValue = int.Parse(GameObject.FindGameObjectWithTag("Target").GetComponentInChildren<TMP_Text>().text);
        currentIndex = 0;
        clickedIndex = -1;
        Search();
    }
    void Search() {
        isComplete = false;
        if (searchMode == 0) {
            StartCoroutine(LinearSearch());
        }
        else if (searchMode == 1) {
            StartCoroutine(BinarySearch());
        }
    }
    IEnumerator LinearSearch() {
        for (int i = 0; i < item_objs.Length; i++) {
            currentIndex = i;
            yield return new WaitUntil(CanContinue);
            Debug.Log("OK");
            // Debug.Log("Current: " + currentIndex.ToString() + "; clicked: " + clickedIndex.ToString());
            int curr_num = int.Parse(item_objs[i].GetComponentInChildren<TMP_Text>().text);
            if (curr_num == targetValue) {
                break;
            }
        }
        Debug.Log("COMPLETE");
        isComplete = true;
    }
    IEnumerator BinarySearch() {
        int low = 0, high = item_objs.Length - 1;
        while (low <= high) {
            int mid = (low + high) / 2;
            currentIndex = mid;
            yield return new WaitUntil(CanContinue);
            int curr_num = int.Parse(item_objs[mid].GetComponentInChildren<TMP_Text>().text);
            if (curr_num == targetValue) {
                break;
            }
            else if (curr_num < targetValue) {
                low = mid + 1;
            }
            else {
                high = mid - 1;
            }
        }
        isComplete = true;
    }
    bool CanContinue() {
        return currentIndex == clickedIndex;
    }
    public void SendIndex(int idx) {
        clickedIndex = idx;
        if (CanContinue()) {
            item_objs[idx].GetComponentInChildren<TMP_Text>().enabled = true;
        }
        else {
            Level.health -= 1;
        }
    }
}
