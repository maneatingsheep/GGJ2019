using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ViewClueButton : MonoBehaviour {
    public TextMeshProUGUI ButtonText;
    public static Action<ViewClueButton> EButtonPressed;

    public void FillButtonText(string text) {
        ButtonText.text = text;
    }

    public void ClueButtonClicked() {
        EButtonPressed(this);
    }
	
}
