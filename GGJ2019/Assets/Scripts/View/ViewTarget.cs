using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewTarget : MonoBehaviour {

    internal ModelTarget Target;
    public TextMeshProUGUI ClueText;

    public void Fill(ModelTarget modelTarget) {
        Target = modelTarget;

    }

    public void ShowClue(string clue) {
        string value = "NAN";
        if(Target.InfoValues.TryGetValue(clue, out value)) {
            ClueText.text = value;
        } else {
            Debug.Log("Target " + gameObject.name + " doesn't have a value for " + clue + "!");
        }
        
    }
}
