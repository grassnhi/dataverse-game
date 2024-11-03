using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInfoManager : MonoBehaviour
{
    TMP_Text playerName;
    TMP_Text playerLevel;
    private void Awake() {
        playerName = GameObject.FindGameObjectWithTag("PlayerName").GetComponentInChildren<TMP_Text>();
        playerLevel = GameObject.FindGameObjectWithTag("PlayerLevel").GetComponentInChildren<TMP_Text>();
    }
    public void SetPlayerInfo(string name, int level) {
        playerName.text = name;
        playerLevel.text = level.ToString();
    }
}
