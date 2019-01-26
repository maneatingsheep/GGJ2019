using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;

public class ViewMainGame : MonoBehaviour {

    public Vector2 MinBuildingAnchoredPosition;
    public Vector2 MaxBuildingAnchoredPosition;

    public RectTransform MainBuilding;
    public ViewTarget TargetPrefab;
    private List<ViewTarget> _targets;
    public Sprite[] Characters;
    public Sprite[] Curtains;

    public float RotateFiltersAnimationDuration;
    public AnimationCurve RotateFiltersAnimationCurve;
    public RectTransform FiltersContainer;
    public WarpTextExample[] FilterTexts;
    public Color[] FilterColors;
    public Color GrayTextColor;
    public Image FilterColor;

    public RectTransform ClueContainer;
    public TextMeshProUGUI CluePrefab;
    private List<TextMeshProUGUI> _clues;
    private int _currentClue;

    private int _currentPropIndex;

    public Timer TimerRef;
    public float ClueEveryXSecs;

    public event Action<bool> ELevelOver;

    public TextMeshProUGUI ScoreText;
    //public Text TimeLeftText;
    public Image FullTimer;

    float TimePortion;

    public int SelectedWindow = -1;

    public GameObject NormalCrosshairs;
    public GameObject RedCrosshairs;

    private bool _textsInited;

    public Button ShootButton;

    internal void Init() {
        //ViewClueButton.EButtonPressed += ShowPropFromButton;
    }

    public void InitLevel(ModelLevelData levelData) {
        ShootButton.interactable = false;
        NormalCrosshairs.SetActive(true);
        RedCrosshairs.SetActive(false);

        if(_targets != null) {
            for (int i = 0; i < _targets.Count; i++) {
                Destroy(_targets[i].gameObject);
            }
            
            for (int i = 0; i < _clues.Count; i++) {
                Destroy(_clues[i].gameObject);
            }
        }

        _levelData = levelData;
        MainBuilding.anchoredPosition = Vector2.one;
        TimerRef.StopTimer();
        _targets = new List<ViewTarget>();
        for (int i = 0; i < levelData.Targets.Length; i++) {
            ViewTarget newTarget = Instantiate(TargetPrefab, MainBuilding);
            Sprite randomCharacter = Characters[UnityEngine.Random.Range(0, Characters.Length - 1)];
            Sprite randomCurtain = Curtains[UnityEngine.Random.Range(0, Curtains.Length - 1)];
            newTarget.Fill(levelData.Targets[i], randomCharacter, randomCurtain);
            _targets.Add(newTarget);
        }

        for (var i = 0; i < levelData.Targets[0].Props.Length; i++) {
            FilterTexts[i].text = GetFilterText(levelData.Targets[0].Props[i]);
            if(!_textsInited) {
                StartCoroutine(FilterTexts[i].Fix());
            }
        }
        _textsInited = true;

        _clues = new List<TextMeshProUGUI>();
        for (int i = 0; i < levelData.Clues.Length; i++) {
            TextMeshProUGUI newClue = Instantiate(CluePrefab, ClueContainer);
            newClue.text = levelData.Clues[i];
            _clues.Add(newClue);
        }
        _currentPropIndex = 0;
        _currentClue = 0;
        TimePortion = 1;

        ShowCurrentProp();

        ShowNextClue();

        _pendingFilterSpins = 0;
        FiltersContainer.rotation = Quaternion.Euler(0, 0, 0);



    }

    public void StartCountDown() {
        TimerRef.StartTimer(ClueEveryXSecs, ShowNextClue);
    }

    private const int PREFERED_STRING_LENGTH = 14;
    private string GetFilterText(Prop prop) {
        string addedSpaces = "";
        for (int i = 0; i < (PREFERED_STRING_LENGTH - prop.Key.Length) / 2; i++) {
            addedSpaces += " ";
        }
        string retVal = string.Format("<color=#0000>.</color>{0}{1}{2}<color=#0000>.</color>", addedSpaces, prop.Key, addedSpaces);


        return retVal;
    }

    private void ShowNextClue() {

        SoundMaster.Instance.PlaySingleSound(SoundMaster.SoundTypes.Clue);

        for (int i = 0; i < _clues.Count; i++) {
            _clues[i].gameObject.SetActive(i <= _currentClue);
        }
        _currentClue++;
        if(_currentClue < _clues.Count) {
            TimerRef.StartTimer(ClueEveryXSecs, ShowNextClue);
        } else {
            GameOver(false);
        }
    }
    
    public void ShowNextProp(bool isUp) {
        SoundMaster.Instance.PlaySingleSound(SoundMaster.SoundTypes.Scope);

        if (isUp) {
            _currentPropIndex++; 
        } else {
            _currentPropIndex--;
            _currentPropIndex += _levelData.Targets[0].Props.Length;

        }
        _currentPropIndex %=  _levelData.Targets[0].Props.Length;
        _pendingFilterSpins += isUp ? 1 : -1;
        StartCoroutine(RotateFilters(isUp));

        ShowCurrentProp();
    }

    public void ShowCurrentProp() {
        for (int i = 0; i < _targets.Count; i++) {
            _targets[i].ShowProp(_levelData.Targets[i].Props[_currentPropIndex].Key);
        }
        FilterColor.color = FilterColors[_currentPropIndex];
        for (int i = 0; i < FilterTexts.Length; i++) {
            FilterTexts[i].m_TextComponent.color = i == _currentPropIndex ? FilterColors[_currentPropIndex] : GrayTextColor;
        }
    }

    public void Shoot() {

        SoundMaster.Instance.PlaySingleSound(SoundMaster.SoundTypes.Shot);

        GameOver(SelectedWindow > -1 && _levelData.Targets[SelectedWindow].IsCorrect);
        
    }

    private Vector3 _previousFiltersRotation;
    private int _pendingFilterSpins;
    private bool _spinning;
    private IEnumerator RotateFilters(bool clockwise) {
        if (_spinning) yield break;
        _spinning = true;
        _previousFiltersRotation = FiltersContainer.rotation.eulerAngles;
        float targetRotationZ = _previousFiltersRotation.z + (clockwise ? -60 : 60);
        float startTime = Time.time;
        while(Time.time - startTime < RotateFiltersAnimationDuration) {
            float z = (Time.time - startTime) / RotateFiltersAnimationDuration;
            Quaternion rotation = Quaternion.Euler(0, 0, _previousFiltersRotation.z + (clockwise ? -60 : 60) * z);
            FiltersContainer.rotation = rotation;
            yield return null;
        }
        FiltersContainer.rotation = Quaternion.Euler(0, 0, targetRotationZ);
        _previousFiltersRotation = FiltersContainer.rotation.eulerAngles;
        _pendingFilterSpins -= _pendingFilterSpins > 0 ? 1 : -1;
        _spinning = false;
        if(_pendingFilterSpins != 0) {
            StartCoroutine(RotateFilters(_pendingFilterSpins > 0));
        }
    }

    private Vector3 _lastMousePos;
    private bool _mouseDown;
    internal Vector2 MOVE_SCALE_VECTOR = new Vector2(2f, 3f);
    private ModelLevelData _levelData;
    public ScopeContainer ScopeContainerRef;

    private void GameOver(bool success) {
        ELevelOver(success);
    }

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

                int horPos = -1;
                int verPos = -1;

                if (MainBuilding.anchoredPosition.x > 100) {
                    horPos = 0;
                } else if (MainBuilding.anchoredPosition.x < -100) {
                    horPos = 1;
                }


                float minYVal = -650;
                float fullWindow = 600;
                float activeWindow = 350;

                float fromBottom = MainBuilding.anchoredPosition.y - minYVal;
                verPos = Mathf.FloorToInt(fromBottom / fullWindow);
                if (fromBottom - verPos * fullWindow > activeWindow) {
                    verPos = -1;
                }

                SelectedWindow = (horPos > -1 && verPos > -1) ?(verPos * 2 + horPos) :-1;

                NormalCrosshairs.SetActive(SelectedWindow == -1);
                RedCrosshairs.SetActive(SelectedWindow > -1);
                ShootButton.interactable = SelectedWindow > -1;
            }
        } else {
            _mouseDown = false;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ShowNextProp(true);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ShowNextProp(false);
        }

        TimePortion = TimerRef.TimeLeft / ClueEveryXSecs;

        if (_clues != null) {
            FullTimer.fillAmount = TimePortion;
            //TimeLeftText.text = TimePortion.ToString();
            ScoreText.text = CurrentScore.ToString();
        }
    }

    public int CurrentScore {
        get {
            int fullClues = _clues.Count - _currentClue;
            return Mathf.FloorToInt( fullClues * 100 + TimePortion * 10);
        }
    }
}
