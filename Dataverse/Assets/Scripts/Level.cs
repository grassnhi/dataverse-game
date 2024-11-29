using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    bool isFinal;
    int[] levelScores = {250, 250, 300, 350, 350, 350, 350, 350};
    // static bool[] levelsDone = {false, false, false, false, false, false, false, false};
    void Awake() {
        victoryBoard.SetActive(false);
        failBoard.SetActive(false);
        isFinal = false;
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }
    void LateUpdate() {
        PopUp();
    }
    void PopUp() {
        if (health > 0 && SearchAlgorithms.isComplete && !isFinal) {
            victoryBoard.SetActive(true);
            int tmp_score = PlayerPrefs.GetInt("PlayerCurrency");
            int levelScore = levelScores[currentLevel - 1];
            foreach (Transform child in victoryBoard.transform) {
                if (child.tag == "LevelScore") {
                    child.gameObject.GetComponentInChildren<TMP_Text>().text = levelScore.ToString();
                }
            }
            if (currentLevel == unlockedLevel) {
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
                    isFinal = true;
                }
            }
            // Debug.Log(currentLevel.ToString() + " " + unlockedLevel.ToString());
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);       
            // PlayerPrefs.Save();
        }
        else if (health == 0 && !SearchAlgorithms.isComplete) {
            failBoard.SetActive(true);
        }
    }
    public void GoToNextLevel() {
        // if (unlockedLevel >= MAX_LEVEL) {
        //     if (currentLevel >= MAX_LEVEL) return;
        // } else {
        //     if (currentLevel == unlockedLevel) {
        //         unlockedLevel += 1;
        //     }
        //     else if (currentLevel > unlockedLevel) return;
        // }
        // PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
        // PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Retry() {
        health = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
