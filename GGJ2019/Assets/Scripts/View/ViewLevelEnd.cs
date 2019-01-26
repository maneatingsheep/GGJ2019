using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewLevelEnd : MonoBehaviour {
    
    public TextMeshProUGUI ScoreText;

    public void Fill(int score) {
        ScoreText.text = "Your score: " + score.ToString("#,##0");
    }
}
