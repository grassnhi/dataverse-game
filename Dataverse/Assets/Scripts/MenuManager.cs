using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToScene(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void QuitGame() {
        PlayerPrefs.DeleteAll();
        Debug.Log("Quit game!");
        Application.Quit(0);
    }
}
