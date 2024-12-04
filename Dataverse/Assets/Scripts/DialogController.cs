using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    TMP_Text currentChar;
    TMP_Text currentLine;
    int currentLineIndex = 0;
    List<(string, string)> dialogLines = new List<(string, string)>();
    GameObject charSprite1;
    GameObject charSprite2;
    // Start is called before the first frame update
    void Awake()
    {
        currentChar = GameObject.FindGameObjectWithTag("CurrentSpeaker").GetComponent<TMP_Text>();
        currentLine = GameObject.FindGameObjectWithTag("CurrentLine").GetComponent<TMP_Text>();
        charSprite1 = GameObject.FindGameObjectWithTag("Algo");
        charSprite2 = GameObject.FindGameObjectWithTag("Chaos");
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName == "Level 1") {
            dialogLines.Add(("Narrator", "In the Array Fields, Algo's journey begins. Before him lies a vast plain where data once flowed in perfect harmony, now shattered and thrown into utter disarray by Chaos."));
            dialogLines.Add(("Narrator", "Somewhere within this chaos lies a treasure—a vital fragment that will restore balance to the Array Fields."));
            dialogLines.Add(("Narrator", "To retrieve it, Algo must master the basics of searching, starting with the simplest, yet essential method: sequential search."));
            dialogLines.Add(("Chaos", "Good luck finding anything here! You'll search forever and find nothing! The fields will drown you in confusion, little hero!"));
            dialogLines.Add(("Algo", "I'll search every fragment if I have to. Order starts with understanding! Your chaos won't last forever."));
        }
        else if (levelName == "Level 2") {
            dialogLines.Add(("Narrator", "Algo’s resolve grows stronger, but the journey ahead demands greater efficiency. The Array Fields stretch far and wide, concealing the next treasure in the distance."));
            dialogLines.Add(("Narrator", "To uncover it, Algo must leap through the data with precision, finding the balance between speed and accuracy."));
            dialogLines.Add(("Chaos", "Faster? Smarter? Ha! You'll only leap into failure. The Key of Sequence will remain hidden forever in my disarray!"));
            dialogLines.Add(("Algo", "I'll leap beyond your reach. Chaos has no defense against logic and order!"));
        }
        else if (levelName == "Level 3") {
            dialogLines.Add(("Narrator", "Before Algo lies an array, a vast and orderly expanse. With each element neatly placed in ascending order,"));
            dialogLines.Add(("Narrator", "Algo knows that the power of Binary Search will guide him swiftly through the array and uncover the key fragment that holds the secret to revealing the true path through the twisted forest."));
            dialogLines.Add(("Chaos", "You think you can simply slice through this orderly array and find what you seek? The treasure is hidden deep within, and you will never uncover it so easily!"));
            dialogLines.Add(("Algo", "I've learned the power of logic, Chaos. With Binary Search, I'll cut through the vastness of this array and find what you've hidden. This will be the key to my journey forward!"));
        }
        // else if (levelName == "Level 4") {
        //     dialogLines.Add(("Chaos", "Faster? Smarter? Ha! You'll only leap into failure. The Key of Sequence will remain hidden forever in my disarray!"));
        //     dialogLines.Add(("Algo", "I'll leap beyond your reach. Chaos has no defense against logic and order!"));
        // }
        // else if (levelName == "Level 5") {
        //     dialogLines.Add(("Chaos", "Faster? Smarter? Ha! You'll only leap into failure. The Key of Sequence will remain hidden forever in my disarray!"));
        //     dialogLines.Add(("Algo", "I'll leap beyond your reach. Chaos has no defense against logic and order!"));
        // }
        // else if (levelName == "Level 6") {
        //     dialogLines.Add(("Chaos", "Faster? Smarter? Ha! You'll only leap into failure. The Key of Sequence will remain hidden forever in my disarray!"));
        //     dialogLines.Add(("Algo", "I'll leap beyond your reach. Chaos has no defense against logic and order!"));
        // }
        // else if (levelName == "Level 7") {
        //     dialogLines.Add(("Chaos", "Faster? Smarter? Ha! You'll only leap into failure. The Key of Sequence will remain hidden forever in my disarray!"));
        //     dialogLines.Add(("Algo", "I'll leap beyond your reach. Chaos has no defense against logic and order!"));
        // }
        // else if (levelName == "Level 8") {
        //     dialogLines.Add(("Chaos", "Faster? Smarter? Ha! You'll only leap into failure. The Key of Sequence will remain hidden forever in my disarray!"));
        //     dialogLines.Add(("Algo", "I'll leap beyond your reach. Chaos has no defense against logic and order!"));
        // }
    }

    // Update is called once per frame
    void Update()
    {
        currentChar.text = dialogLines[currentLineIndex].Item1;
        currentLine.text = dialogLines[currentLineIndex].Item2;
        var img1 = charSprite1.GetComponent<UnityEngine.UI.Image>();
        var img2 = charSprite2.GetComponent<UnityEngine.UI.Image>();
        var tmpColor1 = img1.color;
        var tmpColor2 = img2.color;
        if (currentChar.text == "Algo") {
            tmpColor1.a = 1f;
            tmpColor2.a = 0.5f;
        }
        else if (currentChar.text == "Chaos") {
            tmpColor1.a = 0.5f;
            tmpColor2.a = 1f;
        }
        else {
            tmpColor1.a = 0.5f;
            tmpColor2.a = 0.5f;
        }
        img1.color = tmpColor1;
        img2.color = tmpColor2;
    }
    public void ToNextLine() {
        if (currentLineIndex + 1 == dialogLines.Count) {
            Destroy(gameObject);
        } else {
            currentLineIndex += 1;
        }
    }
}