using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFlowMaster : MonoBehaviour {
    public enum GameStates { StartMenu, Game, GameOver};

    public ViewMainGame MainView;
    public ModelLevelMaster LevelMaster;

    private GameStates _gameState;

    public GameStates GameState {
        get {
            return _gameState;
        }

        set {
            _gameState = value;
            switch (_gameState) {
                case GameStates.StartMenu:
                    Invoke("StartGame", 3);
                    break;
                case GameStates.Game:
                    MainView.InitLevel(LevelMaster.GetNextLevel());
                    break;
                case GameStates.GameOver:
                    break;
            }

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

    
}
