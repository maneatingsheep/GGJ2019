using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewMainGame : MonoBehaviour {

    public RectTransform MainBuilding;
    public ViewTarget TargetPrefab;
    private List<ViewTarget> _targets;

    public RectTransform ButtonsContainer;
    public ViewClueButton ButtonPrefab;
    private List<ViewClueButton> _clueButtons;

    public RectTransform ClueContainer;
    public TextMeshProUGUI CluePrefab;
    private List<TextMeshProUGUI> _clues;
    private int _currentClue;

    public Timer TimerRef;
    public float ClueEveryXSecs;

    public void InitLevel(ModelLevelData levelData) {
        TimerRef.StopTimer();
        _targets = new List<ViewTarget>();
        for (int i = 0; i < levelData.Targets.Length; i++) {
            ViewTarget newTarget = Instantiate(TargetPrefab, MainBuilding);
            newTarget.Fill(levelData.Targets[i]);
            _targets.Add(newTarget);
        }

        _clueButtons = new List<ViewClueButton>();
        for (var i = 0; i < levelData.Targets[0].Props.Length; i++) {
            ViewClueButton newButton = Instantiate(ButtonPrefab, ButtonsContainer);
            newButton.FillButtonText(levelData.Targets[0].Props[i].Key);
            _clueButtons.Add(newButton);
        }

        _clues = new List<TextMeshProUGUI>();
        for (int i = 0; i < levelData.Clues.Length; i++) {
            TextMeshProUGUI newClue = Instantiate(CluePrefab, ClueContainer);
            newClue.text = levelData.Clues[i];
            _clues.Add(newClue);
        }
        _currentClue = 0;
        ShowNextClue();
        TimerRef.StartTimer(ClueEveryXSecs, ShowNextClue);

    }

    private void ShowNextClue() {
        Debug.Log("ShowNextClue: " + _currentClue);
        for (int i = 0; i < _clues.Count; i++) {
            _clues[i].gameObject.SetActive(i <= _currentClue);
        }
        _currentClue++;
        if(_currentClue < _clues.Count) {
            TimerRef.StartTimer(ClueEveryXSecs, ShowNextClue);
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
