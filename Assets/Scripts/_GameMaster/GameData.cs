using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public string sceneName;
    public int toiletNum;
    public bool isScrewUnlocked;
    public bool isLaxaUnlocked;

    public GameData()
    {
        this.sceneName = "DefaultSceneName";
        this.toiletNum = 0;
        this.isScrewUnlocked = false;
        this.isLaxaUnlocked = false;
    }
    

    public GameData(string sceneName, int toiletNum, bool isScrewUnlocked, bool isLaxaUnlocked)
    {
        this.sceneName = sceneName;
        this.toiletNum = toiletNum;
        this.isScrewUnlocked = isScrewUnlocked;
        this.isLaxaUnlocked = isLaxaUnlocked;
    }
}
