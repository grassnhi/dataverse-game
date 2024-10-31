using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToScene(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void QuitGame() {
        Application.Quit(0);
    }
}
