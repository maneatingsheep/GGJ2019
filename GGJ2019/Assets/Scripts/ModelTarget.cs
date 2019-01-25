using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModelTarget {

    public bool IsCorrect = false;
    public Prop[] Props;

	
}

[Serializable]
public class Prop {

    public string Key;
    public string Val;


}


