using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFlowMaster : MonoBehaviour {
    public enum GameStates { StartMenu, Game, GameOver, LevelOver};

    public ViewMainGame MainView;
    public ModelLevelMaster LevelMaster;

    public GameObject OpenScreen;

    public GameObject LevelOverScreen;
    public GameObject GameOverScreen;

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
                    MainView.InitLevel(LevelMaster.GetNextLevel());
                    break;
                case GameStates.GameOver:
                    // fill game over
                    break;
                case GameStates.LevelOver:
                    // fill level over
                    break;
            }
            LevelOverScreen.SetActive(_gameState == GameStates.LevelOver);
            GameOverScreen.SetActive(_gameState == GameStates.GameOver);
        }
    }


    private void Awake() {
        InitAll();
    }

    private void InitAll() {
        MainView.Init();
        LevelMaster.Init();
        MainView.ELevelOver += LevelOver;
        GameState = GameStates.StartMenu;


    }

    public void StartGame() {
        GameState = GameStates.Game;
    }

    private void LevelOver(bool isSuccess) {
        ModelLevelData levelData = LevelMaster.GetNextLevel();
        
        if (levelData != null ) {
            MainView.InitLevel(levelData);
        } else {
            GameState = GameStates.GameOver;
        }
    }

    public void EndLevelTransition() {
        
    }
}
