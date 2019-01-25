
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLevelMaster : MonoBehaviour {

    public TextAsset LevelsJSON;

    private LevelsList _levelsList;



    // Use this for initialization
    public void Init () {
        _levelsList = JsonUtility.FromJson<LevelsList>(LevelsJSON.text);
        print(LevelsJSON.text);
    }
	
	

    internal ModelLevelData GetNextLevel() {
        
        return _levelsList.Levels[0];
    }
}

[Serializable]
public class LevelsList {
    public ModelLevelData[] Levels;
}
