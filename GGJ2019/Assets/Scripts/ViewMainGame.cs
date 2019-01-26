using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewMainGame : MonoBehaviour {

    public Vector2 MinBuildingAnchoredPosition;
    public Vector2 MaxBuildingAnchoredPosition;

    public RectTransform MainBuilding;
    public ViewTarget TargetPrefab;
    private List<ViewTarget> _targets;

    public TextMeshProUGUI[] FilterTexts;

    public RectTransform ClueContainer;
    public TextMeshProUGUI CluePrefab;
    private List<TextMeshProUGUI> _clues;
    private int _currentClue;

    private int _currentPropIndex;

    public Timer TimerRef;
    public float ClueEveryXSecs;

    public event Action<bool> ELevelOver;

    internal void Init() {
        //ViewClueButton.EButtonPressed += ShowPropFromButton;
    }

    public void InitLevel(ModelLevelData levelData) {
        _levelData = levelData;
        MainBuilding.anchoredPosition = Vector2.one;
        TimerRef.StopTimer();
        _targets = new List<ViewTarget>();
        for (int i = 0; i < levelData.Targets.Length; i++) {
            ViewTarget newTarget = Instantiate(TargetPrefab, MainBuilding);
            newTarget.Fill(levelData.Targets[i]);
            _targets.Add(newTarget);
        }

        Debug.Log(levelData.Targets[0].Props.Length);
        Debug.Log(levelData.Targets[0].Props[0].Val);

        for (var i = 0; i < levelData.Targets[0].Props.Length; i++) {
            FilterTexts[i].text = GetFilterText(levelData.Targets[0].Props[i]);
        }

        _clues = new List<TextMeshProUGUI>();
        for (int i = 0; i < levelData.Clues.Length; i++) {
            TextMeshProUGUI newClue = Instantiate(CluePrefab, ClueContainer);
            newClue.text = levelData.Clues[i];
            _clues.Add(newClue);
        }
        _currentPropIndex = 0;
        ShowCurrentProp();

        _currentClue = 0;
        ShowNextClue();
        TimerRef.StartTimer(ClueEveryXSecs, ShowNextClue);

    }

    private const int PREFERED_STRING_LENGTH = 14;
    private string GetFilterText(Prop prop) {
        string addedSpaces = "";
        for (int i = 0; i < (PREFERED_STRING_LENGTH - prop.Key.Length) / 2; i++) {
            addedSpaces += " ";
        }
        string retVal = string.Format("<color=#0000>.</color>{0}{1}{2}<color=#000>.</color>", addedSpaces, prop.Key, addedSpaces);


        return retVal;
    }

    private void ShowNextClue() {
        
        for (int i = 0; i < _clues.Count; i++) {
            _clues[i].gameObject.SetActive(i <= _currentClue);
        }
        _currentClue++;
        if(_currentClue < _clues.Count) {
            TimerRef.StartTimer(ClueEveryXSecs, ShowNextClue);
        }
    }



   public void ShowNextProp(bool isUp) {
        if (isUp) {
            _currentPropIndex++; 
        } else {
            _currentPropIndex--;
            _currentPropIndex += _levelData.Targets[0].Props.Length;

        }
        _currentPropIndex %=  _levelData.Targets[0].Props.Length;

        ShowCurrentProp();
    }

    public void ShowCurrentProp() {
        for (int i = 0; i < _targets.Count; i++) {
            _targets[i].ShowProp(_levelData.Targets[i].Props[_currentPropIndex].Key);
        }
    }

    public void Shoot() {
        ELevelOver(true);
    }

    private Vector3 _lastMousePos;
    private bool _mouseDown;
    internal Vector2 MOVE_SCALE_VECTOR = new Vector2(2f, 3f);
    private ModelLevelData _levelData;
    public ScopeContainer ScopeContainerRef;


    private void Update() {
        if (Input.GetMouseButton(0)) {
            if (ScopeContainerRef.IsOverScope){
                if (_mouseDown) {
                    Vector2 newPos = MainBuilding.anchoredPosition - (_lastMousePos - Input.mousePosition) * MOVE_SCALE_VECTOR;
                    newPos = Vector2.Min(newPos, MaxBuildingAnchoredPosition);
                    newPos = Vector2.Max(newPos, MinBuildingAnchoredPosition);
                    MainBuilding.anchoredPosition = newPos;
                }
                _mouseDown = true;
                
                _lastMousePos = Input.mousePosition;
            }
        } else {
            _mouseDown = false;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ShowNextProp(true);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ShowNextProp(false);
        }
    }
}
