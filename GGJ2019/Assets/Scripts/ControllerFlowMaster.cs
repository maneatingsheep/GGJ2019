using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFlowMaster : MonoBehaviour {
    public ViewMainGame MainView;

    private void Awake() {
        InitAll();
    }

    private void InitAll() {
        MainView.Init();

    }

    private void InitLevel() {
        ModelLevelData levelData = new ModelLevelData();
        MainView.InitLevel(levelData);
    }

}
