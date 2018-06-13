using EazyTools.SoundManager;
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
        SoundManager.StopAll();
        SceneManager.LoadScene(0);
    }

    public void StartNewGame(string sceneName)
    {
        GameData gd = new GameData
        {
            sceneName = sceneName,
            toiletNum = 100
            
        };

        ////Debug.Log(SceneManager.GetSceneByBuildIndex(1).name);
        //for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        //{
        //    Debug.Log(SceneManager.GetSceneByBuildIndex(i));
        //}


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
