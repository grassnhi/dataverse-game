using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    public GameObject finalScore;
    // Start is called before the first frame update
    void Start()
    {
        finalScore.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("PlayerCurrency").ToString();
    }
}
