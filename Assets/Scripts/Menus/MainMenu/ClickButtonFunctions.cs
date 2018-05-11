using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButtonFunctions : MonoBehaviour {

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void LoadScene(string sceneName)
    {
        GameData gd = new GameData();
        gd.sceneName = sceneName;
        gd.toiletNum = 100;

        GameMasterManager gMM = GameMasterManager.Instance;
        SaveManager.Save(gd,gMM.saveName);
        gMM.gd_nextLevel = gd;

        SceneManager.LoadScene(gd.sceneName);
    }
}
