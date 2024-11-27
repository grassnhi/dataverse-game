using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    const int MAX_LEVEL = 3;
    public static int health = 3;
    public GameObject victoryBoard;
    public GameObject failBoard;
    public int currentLevel;
    int unlockedLevel;
    int[] levelScores = {250, 250, 300, 350, 350, 350, 350, 350};
    static bool[] levelsDone = {false, false, false, false, false, false, false, false};
    void Awake() {
        victoryBoard.SetActive(false);
        failBoard.SetActive(false);
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }
    void LateUpdate() {
        PopUp();
    }
    public void PopUp() {
        if (health > 0 && SearchAlgorithms.isComplete) {
            victoryBoard.SetActive(true);
            int tmp_score = PlayerPrefs.GetInt("PlayerCurrency");
            int levelScore = levelScores[currentLevel - 1];
            foreach (Transform child in victoryBoard.transform) {
                if (child.tag == "LevelScore") {
                    child.gameObject.GetComponentInChildren<TMP_Text>().text = levelScore.ToString();
                }
            }
            if (levelsDone[currentLevel-1] == false) {
                tmp_score += levelScore;
                levelsDone[currentLevel-1] = true;
            }
            PlayerPrefs.SetInt("PlayerCurrency", tmp_score);
            // PlayerPrefs.Save();
        }
        else if (health == 0 && !SearchAlgorithms.isComplete) {
            failBoard.SetActive(true);
        }
    }
    public void GoToNextLevel() {
        if (unlockedLevel >= MAX_LEVEL) {
            if (currentLevel >= MAX_LEVEL) return;
        } else {
            if (currentLevel == unlockedLevel) {
                unlockedLevel += 1;
            }
            else if (currentLevel > unlockedLevel) return;
        }
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
        // PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Retry() {
        health = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
