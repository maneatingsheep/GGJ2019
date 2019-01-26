using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewLevelEnd : MonoBehaviour {

    public TextMeshProUGUI WinLoseText;
    public TextMeshProUGUI ScoreText;

    public void Fill(int score) {
        WinLoseText.text = "good job!";
        ScoreText.text = score.ToString("#,##0");
    }
}
