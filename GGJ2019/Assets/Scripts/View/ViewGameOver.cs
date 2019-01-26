using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewGameOver : MonoBehaviour {

    public GameObject WinPopup;
    public GameObject LosePopup;
    public TextMeshProUGUI ScoreText;

    public void Fill(int score, bool isWin) {
        WinPopup.SetActive(isWin);
        LosePopup.SetActive(!isWin);
        ScoreText.text = "Your score: " + score.ToString("#,##0");
    }
}
