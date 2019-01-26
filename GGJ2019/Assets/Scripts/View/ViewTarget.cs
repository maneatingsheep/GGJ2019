using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ViewTarget : MonoBehaviour {

    public Sprite[] CharaterPool;
    public Image Character; 

    internal ModelTarget Target;
    public TextMeshProUGUI ClueText;
    private float _timeOfPointDown;
    private Vector3 _pointDownPos;

    public void Fill(ModelTarget modelTarget) {
        Target = modelTarget;
        //Character.sprite = CharaterPool[Random.Range(0, CharaterPool.Length)];

        IsActive = true;
    }

    private bool _isActive;

    public bool IsActive {
        get {
            return _isActive;
        }

        set {
            _isActive = value;
            print(_isActive);
        }
    }

    public void ShowProp(string clue) {
        for (int i = 0; i < Target.Props.Length; i++) {
            if(Target.Props[i].Key == clue) {
                ClueText.text = Target.Props[i].Val;
                return;
            }
        }
        
    }

    public void PointerDown() {
        _timeOfPointDown = Time.time;
        _pointDownPos = Input.mousePosition;
    }

    public void PointerUp() {
        if (Time.time - _timeOfPointDown < 0.3f) {
            float dist = (Input.mousePosition - _pointDownPos).magnitude;

            if (dist / Screen.width < 0.05f) {

                IsActive = !_isActive;
            }
        }
    }
}
