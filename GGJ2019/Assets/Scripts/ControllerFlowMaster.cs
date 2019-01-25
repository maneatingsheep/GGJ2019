using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFlowMaster : MonoBehaviour {
    public ViewMainGame MainView;
    public ModelLevelMaster LevelMaster;

    private void Awake() {
        InitAll();
    }

    private void InitAll() {
        MainView.Init();
        LevelMaster.Init();
        InitLevel();
    }

    private void InitLevel() {

        MainView.InitLevel(LevelMaster.GetNextLevel());
    }
}
