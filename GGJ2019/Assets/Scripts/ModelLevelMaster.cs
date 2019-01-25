using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLevelMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	

    internal ModelLevelData GetNextLevel() {
        ModelLevelData retVal = new ModelLevelData();

        retVal.Clues = new List<string>() { "clue 1 is a long one", "clue 2 is even longer " };


        ModelTarget mt1 = new ModelTarget();
        mt1.InfoValues = new Dictionary<string, string>();
        mt1.InfoValues["Country"] = "USA";
        mt1.InfoValues["BodyType"] = "Thin";
        mt1.InfoValues["SkinColor"] = "White";

        ModelTarget mt2 = new ModelTarget();
        mt2.InfoValues = new Dictionary<string, string>();
        mt2.InfoValues["Country"] = "USA2";
        mt2.InfoValues["BodyType"] = "Thin2";
        mt2.InfoValues["SkinColor"] = "White2";

        ModelTarget mt3 = new ModelTarget();
        mt3.InfoValues = new Dictionary<string, string>();
        mt3.InfoValues["Country"] = "USA3";
        mt3.InfoValues["BodyType"] = "Thin3";
        mt3.InfoValues["SkinColor"] = "White3";

        retVal.Targets.Add(mt1);
        retVal.Targets.Add(mt2);
        retVal.Targets.Add(mt3);


        return retVal;
    }
}
