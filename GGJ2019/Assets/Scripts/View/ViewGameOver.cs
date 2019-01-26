using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewGameOver : MonoBehaviour {

    public TextMeshProUGUI WinLoseText;
    public TextMeshProUGUI ScoreText;

    public void Fill(int score, bool isWin) {
        WinLoseText.text = isWin ? "win!" : "lose!";
        ScoreText.text = score.ToString("#,##0");
    }
}
