﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMainGame : MonoBehaviour {

    public RectTransform MainBuilding;
    public ViewTarget TargetPrefab;
    private List<ViewTarget> _targets;

    public RectTransform ButtonsContainer;
    public ViewClueButton ButtonPrefab;
    private List<ViewClueButton> _clueButtons;

    public void InitLevel(ModelLevelData levelData) {
        _targets = new List<ViewTarget>();
        for (int i = 0; i < levelData.Targets.Count; i++) {
            ViewTarget newTarget = Instantiate(TargetPrefab, MainBuilding);
            newTarget.Fill(levelData.Targets[i]);
            _targets.Add(newTarget);
        }

        _clueButtons = new List<ViewClueButton>();
        foreach (var key in levelData.Targets[0].InfoValues.Keys) {
            ViewClueButton newButton = Instantiate(ButtonPrefab, ButtonsContainer);
            newButton.FillButtonText(key);
            _clueButtons.Add(newButton);
        }
    }

    internal void Init() {
        ViewClueButton.EButtonPressed += ShowClueData;
    }

    public void ShowClueData(string clue) {
        for (int i = 0; i < _targets.Count; i++) {
            _targets[i].ShowClue(clue);
        }
    }
}
