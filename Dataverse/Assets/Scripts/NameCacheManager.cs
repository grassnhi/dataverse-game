using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameCacheManager : MonoBehaviour
{
    GameObject[] main_menuButtons;
    public GameObject warning;
    void Awake() {
        main_menuButtons = GameObject.FindGameObjectsWithTag("MainMenu");
    }
    public void GoToPlayScene() {
        Debug.Log(PlayerPrefs.HasKey("PlayerName"));
        if (PlayerPrefs.HasKey("PlayerName")) {
            string name = PlayerPrefs.GetString("PlayerName");
            if (name == null || name == "") {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadSceneAsync("New User");
                return;
            }
            for (int i = 0; i < main_menuButtons.Length; i++) {
                main_menuButtons[i].GetComponent<Button>().interactable = false;
            }
            warning.SetActive(true);
        }
        else {
            SceneManager.LoadScene("New User");
        }
    }
    public void Accept() {
        SceneManager.LoadScene("Play Scene");
    }
    public void Reject() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("New User");
    }
    public void Exit() {
        warning.SetActive(false);
        for (int i = 0; i < main_menuButtons.Length; i++) {
            main_menuButtons[i].GetComponent<Button>().interactable = true;
        }
    }
}
