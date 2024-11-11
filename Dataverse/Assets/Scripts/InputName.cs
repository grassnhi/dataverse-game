using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour
{
    [SerializeField] TMP_InputField name_input;
    public GameObject nameWarning;
    public void AddName() {
        string player_name = name_input.text;
        if (player_name.Length == 0) {
            return;
        }
        nameWarning.SetActive(true);
        nameWarning.GetComponentsInChildren<TMP_Text>()[1].text = player_name;
    }
    public void AcceptName() {
        Debug.Log("My name is " + name_input.text);
        SceneManager.LoadSceneAsync("Play Scene");
    }
    public void RefuseName() {
        nameWarning.SetActive(false);
        name_input.text = "";
    }
}
