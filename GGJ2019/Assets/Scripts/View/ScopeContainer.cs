using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeContainer : MonoBehaviour {

    internal bool IsOverScope;

    private void OnMouseEnter() {
        IsOverScope = true;
    }

    private void OnMouseExit() {
        IsOverScope = false;

    }
}
