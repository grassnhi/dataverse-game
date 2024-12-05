using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
    public Button[] buttons;
    public GameObject tooltip;
    int unlockedLevel;
    void Awake() {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");
    }
    public void OpenLevel(int levelID) {
        if (levelID <= unlockedLevel) {
            Level.health = 3;
            Level.retryFlag = !(levelID == unlockedLevel);
            string levelName = "Level " + levelID.ToString();
            SceneManager.LoadScene(levelName);
        }
        else {
            Button curr_button = buttons[levelID - 1];
            Rect btn_rect = curr_button.GetComponent<RectTransform>().rect;
            Vector3 tooltip_pos = curr_button.transform.position - new Vector3(0, btn_rect.width*4/5, 0);
            StartCoroutine(waiter(tooltip_pos));
        }
    }
    IEnumerator waiter(Vector3 position) {
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        yield return new WaitForSeconds(1);
        tooltip.SetActive(false);
    }
}
