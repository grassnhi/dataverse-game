using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _TutorialText;
    [SerializeField] private GameObject _Clickondot;
    [SerializeField] private GameObject _Clicktomove;
    [SerializeField] private GameObject _Blur;
    // Start is called before the first frame update
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        _Clicktomove.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(UnitController.flag == 1)
        {
            _Clickondot.SetActive(false);
            _Clicktomove.SetActive(true);
        }
        if (UnitController.flag == 2) {
            _Blur.SetActive(false);
        }

    }
}
