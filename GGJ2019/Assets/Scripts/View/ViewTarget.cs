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

    public void ShowProp(string clue) {
        for (int i = 0; i < Target.Props.Length; i++) {
            if(Target.Props[i].Key == clue) {
                ClueText.text = Target.Props[i].Val;
                return;
            }
        }
        Debug.Log("Target " + gameObject.name + " doesn't have a value for " + clue + "!");
    }
}
