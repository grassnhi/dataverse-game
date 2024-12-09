using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (Level.retryFlag == true) {
            gameObject.SetActive(false);
            return;
        };
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.retryFlag == true) return;
        currentChar.text = dialogLines[currentLineIndex].Item1;
        currentLine.text = dialogLines[currentLineIndex].Item2;
        var img1 = charSprite1.GetComponent<UnityEngine.UI.Image>();
        var img2 = charSprite2.GetComponent<UnityEngine.UI.Image>();
        var tmpColor1 = img1.color;
        var tmpColor2 = img2.color;
        if (currentChar.text == "Algo") {
            tmpColor1.a = 1f;
            tmpColor2.a = 0f;
        }
        else if (currentChar.text == "Chaos") {
            tmpColor1.a = 0f;
            tmpColor2.a = 1f;
        }
        else {
            tmpColor1.a = 0f;
            tmpColor2.a = 0f;
        }
        img1.color = tmpColor1;
        img2.color = tmpColor2;
    }
    public void ToNextLine() {
        if (currentLineIndex + 1 == dialogLines.Count) {
            Level.retryFlag = true;
            gameObject.SetActive(false);
            dialogLines.Clear();
        } else {
            currentLineIndex += 1;
        }
    }
    public void ToNextLineEpilogue() {
        if (currentLineIndex + 1 == dialogLines.Count) {
            Level.retryFlag = true;
            dialogLines.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            currentLineIndex += 1;
        }
    }
    public void Rewatch() {
        currentLineIndex = 0;
        Level.retryFlag = false;
        gameObject.SetActive(true);
        Setup();
    }
    void Setup() {
        currentChar = GameObject.FindGameObjectWithTag("CurrentSpeaker").GetComponent<TMP_Text>();
        currentLine = GameObject.FindGameObjectWithTag("CurrentLine").GetComponent<TMP_Text>();
        charSprite1 = GameObject.FindGameObjectWithTag("Algo");
        charSprite2 = GameObject.FindGameObjectWithTag("Chaos");
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName == "Main Story") {
            dialogLines.Add(("", "The Dataverse was once a realm of order, where data flowed efficiently. Young and curious, Algo thrived in this structured environment."));
            dialogLines.Add(("", "However, a flawed system update birthed Chaos—a corrupt entity that distorts data, hides critical fragments, and thrives on disorder."));
            dialogLines.Add(("Narrator", "The Dataverse—a sanctuary of information, now overshadowed by Chaos. This corrupted force scatters data into the void, challenging any who seek to restore balance."));
            dialogLines.Add(("Chaos", "Order is a fragile illusion! Let the Dataverse crumble into entropy, its treasures scattered beyond reach."));
            dialogLines.Add(("Chaos", "You, a mere speck, think you can defy me? Your efforts will only add to the chaos, little hero!"));
            dialogLines.Add(("Algo", "I'll find every fragment you've hidden. I will restore balance, no matter the challenge!"));
        }
        else if (levelName == "Level 1") {
            dialogLines.Add(("Narrator", "In the Array Fields, Algo's journey begins. Before him lies a vast plain where data once flowed in perfect harmony, now shattered and thrown into utter disarray by Chaos."));
            dialogLines.Add(("Narrator", "Somewhere within this chaos lies a treasure—a vital fragment that will restore balance to the Array Fields."));
            dialogLines.Add(("Narrator", "To retrieve it, Algo must master the basics of searching, starting with the simplest, yet essential method: sequential search."));
            dialogLines.Add(("Chaos", "Good luck finding anything here! You'll search forever and find nothing! The fields will drown you in confusion, little hero!"));
            dialogLines.Add(("Algo", "I'll search every fragment if I have to. Order starts with understanding! Your chaos won't last forever."));
        }
        else if (levelName == "Level 2") {
            dialogLines.Add(("Narrator", "Algo's resolve grows stronger, but the journey ahead demands greater efficiency. The Array Fields stretch far and wide, concealing the next treasure in the distance."));
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
        else if (levelName == "Epilogue") {
            dialogLines.Add(("Narrator", "The journey has been long and perilous. From the disordered fields of arrays to the twisted trees, the tangled networks of graphs, and the broken vaults of hashes,"));
            dialogLines.Add(("Narrator", "Algo has reclaimed the essence of order from the clutches of Chaos. Now, with all eight fragments in hand, the fate of the Dataverse lies in his grasp."));
            dialogLines.Add(("Chaos", "You think you've won? You may have the fragments, but you lack the strength to wield them. The Dataverse is mine!"));
            dialogLines.Add(("Algo", "No, Chaos. The Dataverse belongs to balance. It's not about control or destruction - it's about harmony."));
            dialogLines.Add(("Narrator", "With the fragments reunited, the Core emits a powerful wave of order, purging Chaos from the Dataverse."));
            dialogLines.Add(("Narrator", "The twisted landscapes unwind, data flows smoothly, and the once-broken systems realign to their proper forms"));
            dialogLines.Add(("Narrator", "In restoring the fragments, Algo has done more than repair the Dataverse. He has proven that understanding, perseverance, and logic can overcome even the most insurmountable challenges"));
            dialogLines.Add(("Algo", "The journey was never about the fragments—it was about learning to think clearly, to adapt, and to believe in the power of logic."));
            dialogLines.Add(("Algo", "Chaos is a part of every system, but it's how we respond to it that defines us."));
            dialogLines.Add(("Narrator", "And so, the Dataverse stands as a beacon of order amidst the tides of chaos, forever guarded by the logic and courage of its hero, Algo."));
            dialogLines.Add(("Narrator", "His story is not just one of victory but of understanding - a lesson for all who seek clarity in a chaotic world."));
        }
    }
}
