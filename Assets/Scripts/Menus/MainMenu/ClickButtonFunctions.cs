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

        GameMasterManager gMM = GameMasterManager.instance;
        SaveManager.Save(gd,gMM.saveName);
        gMM.gd_nextLevel = gd;

        SceneManager.LoadScene(gd.sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void DebugWrite(string text)
    {
        Debug.Log(Time.timeSinceLevelLoad + "|| " + text);
    }

    public void PC_Continue()
    {
        PauseController.instance.TogglePause();
    }
}
