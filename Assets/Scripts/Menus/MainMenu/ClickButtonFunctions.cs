using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButtonFunctions : MonoBehaviour {

    string newGameSaveName;

    private void Awake()
    {
        newGameSaveName = GameMasterManager.instance.saveName;
    }

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

    public void StartNewGame()
    {
        GameData gd = new GameData();
        gd.sceneName = SceneManager.GetSceneByBuildIndex(0).name;
        gd.toiletNum = 100;

        GameMasterManager gMM = GameMasterManager.instance;
        SaveManager.Save(gd, gMM.saveName);
        gMM.gd_nextLevel = gd;
        SaveManager.Load(gMM.saveName, this);
        //SceneManager.LoadScene(gd.sceneName);
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
