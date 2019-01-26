
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLevelMaster : MonoBehaviour {

    public TextAsset LevelsJSON;

    private LevelsList _levelsList;
    private int _currentLevel;


    // Use this for initialization
    public void Init () {
        _levelsList = JsonUtility.FromJson<LevelsList>(LevelsJSON.text);


        for (int i = 0; i < _levelsList.Levels.Length; i++) {
            _levelsList.Levels[i].Targets[0].IsCorrect = true;

            List<ModelTarget> targetList = new List<ModelTarget>();
            for (int j = 0; j < _levelsList.Levels[i].Targets.Length; j++) {
                targetList.Insert(UnityEngine.Random.Range(0, targetList.Count), _levelsList.Levels[i].Targets[j]);
            }

            _levelsList.Levels[i].Targets = targetList.ToArray();

        }

        _currentLevel = 0;
    }
	
	

    internal ModelLevelData GetNextLevel() {
        return _levelsList.Levels[_currentLevel++];
    }
}

[Serializable]
public class LevelsList {
    public ModelLevelData[] Levels;
}
