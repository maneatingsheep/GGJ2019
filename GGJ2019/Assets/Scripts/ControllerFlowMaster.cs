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
        MainView.ELevelOver += LevelOver;

        MainView.InitLevel(LevelMaster.GetNextLevel());
    }

    private void LevelOver(bool obj) {
        ModelLevelData levelData = LevelMaster.GetNextLevel();
        
        if (levelData != null ) {
            MainView.InitLevel(levelData);
        } else {

        }
    }

    
}
