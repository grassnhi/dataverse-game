using UnityEngine;
using TMPro;

public class BroadcastName : MonoBehaviour
{
    TMP_Text info_name_tmp;
    void Awake() {
        info_name_tmp = GameObject.FindGameObjectWithTag("PlayerName").GetComponent<TMP_Text>();
        info_name_tmp.text = PlayerPrefs.GetString("PlayerName");
    }

}
