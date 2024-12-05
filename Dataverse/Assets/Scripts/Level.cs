using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    const int MAX_LEVEL = 3;
    public static int health = 3;
    public static bool retryFlag = false;
    public GameObject victoryBoard;
    public GameObject failBoard;
    public int currentLevel;
    int unlockedLevel;
    int isFinal;
    int[] levelScores = {250, 250, 300, 350, 350, 350, 350, 350};
    void Awake() {
        victoryBoard.SetActive(false);
        failBoard.SetActive(false);
        isFinal = PlayerPrefs.GetInt("IsFinal");
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");
    }
    void LateUpdate() {
        StartCoroutine(PopUp());
    }
    IEnumerator PopUp() {
        // yield return new WaitForSeconds(1.5f);
        if (health > 0 && SearchAlgorithms.isComplete) {
            yield return new WaitForSeconds(0.5f);
            victoryBoard.SetActive(true);
            int tmp_score = PlayerPrefs.GetInt("PlayerCurrency");
            int levelScore = levelScores[currentLevel - 1];
            foreach (Transform child in victoryBoard.transform) {
                if (child.tag == "LevelScore") {
                    child.gameObject.GetComponentInChildren<TMP_Text>().text = levelScore.ToString();
                }
            }
            if (currentLevel == unlockedLevel && isFinal == 0) {
                tmp_score += levelScore;
                Debug.Log("Add score");
            }
            PlayerPrefs.SetInt("PlayerCurrency", tmp_score);
            if (unlockedLevel < MAX_LEVEL) {
                if (currentLevel == unlockedLevel) {
                    unlockedLevel += 1;
                }
            } else {
                if (currentLevel == unlockedLevel) {
                    isFinal = 1;
                }
            }
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.SetInt("IsFinal", isFinal);    
            // PlayerPrefs.Save();
        }
        else if (health == 0 && !SearchAlgorithms.isComplete) {
            yield return new WaitForSeconds(0.5f);
            failBoard.SetActive(true);
        }
    }
    public void GoToNextLevel() {
        health = 3;
        retryFlag = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Retry() {
        health = 3;
        retryFlag = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
