using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFlowMaster : MonoBehaviour {
    public enum GameStates { StartMenu, Game, GameOver, LevelOver};

    public ViewMainGame MainView;
    public ModelLevelMaster LevelMaster;

    public GameObject OpenScreen;

    public ViewLevelEnd LevelOverScreen;
    public ViewGameOver GameOverScreen;

    public SoundMaster SoundMasterRef;

    private ModelLevelData _levelData;

    private GameStates _gameState;

    public GameStates GameState {
        get {
            return _gameState;
        }

        set {
            _gameState = value;
            switch (_gameState) {
                case GameStates.StartMenu:
                    OpenScreen.SetActive(true);
                    //Invoke("StartGame", 3);
                    break;
                case GameStates.Game:
                    OpenScreen.SetActive(false);
                    break;
                case GameStates.GameOver:
                    LevelMaster.Init();
                    LevelMaster.ResetLevels();
                    break;
                case GameStates.LevelOver:
                    // fill level over
                    break;
            }
            LevelOverScreen.gameObject.SetActive(_gameState == GameStates.LevelOver);
            GameOverScreen.gameObject.SetActive(_gameState == GameStates.GameOver);
        }
    }


    private void Awake() {
        InitAll();
    }

    private void InitAll() {
        MainView.Init();
        LevelMaster.Init();

        _levelData = LevelMaster.GetNextLevel();

        MainView.ELevelOver += LevelOver;
        GameState = GameStates.StartMenu;


    }

    public void StartGame() {
        MainView.InitLevel(_levelData);
        EndLevelTransition();
    }

    private void LevelOver(bool isSuccess) {
        _levelData = LevelMaster.GetNextLevel();
        
        if (isSuccess && _levelData != null ) {
            GameState = GameStates.LevelOver;
            
            LevelOverScreen.Fill(MainView.CurrentScore);
        } else {
            GameState = GameStates.GameOver;
            GameOverScreen.Fill(MainView.CurrentScore, isSuccess);
        }
    }

    public void EndLevelTransition() {
        MainView.StartCountDown();
        GameState = GameStates.Game;
    }
}
