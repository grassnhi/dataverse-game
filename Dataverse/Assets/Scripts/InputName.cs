using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour
{
    [SerializeField] TMP_InputField name_input;
    public GameObject nameWarning;
    string player_name;
    // public static string player_name;
    public void AddName() {
        player_name = name_input.text;
        if (player_name.Length == 0) {
            return;
        }
        nameWarning.SetActive(true);
        nameWarning.GetComponentsInChildren<TMP_Text>()[1].text = player_name;
    }
    public void AcceptName() {
        PlayerPrefs.SetString("PlayerName", player_name);
        PlayerPrefs.SetInt("PlayerCurrency", 0);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("IsFinal", 0);
        // PlayerPrefs.Save();
        Level.retryFlag = false;
        SceneManager.LoadSceneAsync("Play Scene");
    }
    public void RefuseName() {
        nameWarning.SetActive(false);
        name_input.text = "";
    }
}
