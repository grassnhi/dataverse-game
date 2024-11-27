using UnityEngine;
using TMPro;

public class BroadcastName : MonoBehaviour
{
    TMP_Text info_name_tmp;
    TMP_Text currency_tmp;
    TMP_Text level_tmp;
    void Awake() {
        info_name_tmp = GameObject.FindGameObjectWithTag("PlayerName").GetComponent<TMP_Text>();
        currency_tmp = GameObject.FindGameObjectWithTag("PlayerCurrency").GetComponent<TMP_Text>();
        level_tmp = GameObject.FindGameObjectWithTag("PlayerLevel").GetComponent<TMP_Text>();
        info_name_tmp.text = PlayerPrefs.GetString("PlayerName", "");
        currency_tmp.text = PlayerPrefs.GetInt("PlayerCurrency", 0).ToString();
        level_tmp.text = "Level " + PlayerPrefs.GetInt("PlayerLevel", 1).ToString();
    }

}
