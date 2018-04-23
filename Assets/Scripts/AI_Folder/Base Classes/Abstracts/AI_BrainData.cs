using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_BrainData : ScriptableObject {


    [System.Serializable]
    [HideInInspector]
    public struct integerListElement { public string key; public int value; }
    public integerListElement[] integers;

    [System.Serializable]
    [HideInInspector]
    public struct stringListElement { public string key; public string value; }
    public stringListElement[] strings;

    [System.Serializable]
    [HideInInspector]
    public struct gameobjectListElement { public string key; public GameObject value; }
    public gameobjectListElement[] gameObjects;

}
