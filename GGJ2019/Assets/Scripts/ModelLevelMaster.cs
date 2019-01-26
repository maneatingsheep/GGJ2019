
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

        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        _levelsList = JsonUtility.FromJson<LevelsList>(LevelsJSON.text);


        for (int i = 0; i < _levelsList.Levels.Length; i++) {
            _levelsList.Levels[i].Targets[0].IsCorrect = true;

            List<ModelTarget> targetList = new List<ModelTarget>();
            for (int j = 0; j < _levelsList.Levels[i].Targets.Length; j++) {
                int pos = UnityEngine.Random.Range(0, targetList.Count + 1);
                
                targetList.Insert(pos, _levelsList.Levels[i].Targets[j]);
            }

            _levelsList.Levels[i].Targets = targetList.ToArray();

        }

        _currentLevel = 0;
    }
	
	

    internal ModelLevelData GetNextLevel() {
        return _levelsList.Levels[_currentLevel++];
    }

    public void ResetLevels() {
        _currentLevel = 0;
    }
}

[Serializable]
public class LevelsList {
    public ModelLevelData[] Levels;
}
