using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionSceneController : MonoBehaviour {

    public Image mainImage;

    public float timeBetweenImages = 5;
    private int curIndex = 0;
    private float timeOfNewImage;

    public List<Sprite> spriteList;


    string sceneName = "startingFloor";

    private void Start()
    {
        Init();
    }


    private void Init()
    {
        timeOfNewImage = Time.timeSinceLevelLoad;
        PauseController.instance.ClosePause();
        SetImage(curIndex);
    }



    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseController.instance.TogglePause();
        }

        if (Input.GetButtonDown("Hide") ||
            Input.GetButtonDown("Interact") ||
            Input.GetButtonDown("Dash") ||
            Input.GetButtonDown("Use"))
        {
            ForceImage();
        }
        //Debug.Log("Time Since new Image = " + Math.Round(Time.timeSinceLevelLoad - timeOfNewImage, 1)+"("+ curIndex+")");
        if (Time.timeSinceLevelLoad - timeOfNewImage > timeBetweenImages)
        {
            ForceImage();
        }

    }

    private void ForceImage()
    {
        if(curIndex+1< spriteList.Count)
        {
            curIndex++;
            SetImage(curIndex);
            timeOfNewImage = Time.timeSinceLevelLoad;
        }
        else
        {
            StartNewGame(sceneName);
        }
    }

    private void SetImage(int index)
    {
        SetImage(spriteList[index]);
    }

    private void SetImage(Sprite sprite)
    {
        mainImage.sprite = sprite;
    }


    public void StartNewGame(string sceneName)
    {
        GameData gd = new GameData
        {
            sceneName = sceneName,
            toiletNum = 100
        };
        GameMasterManager gMM = GameMasterManager.instance;
        SaveManager.Save(gd, gMM.saveName);
        gMM.gd_nextLevel = gd;
        SaveManager.Load(gMM.saveName, this);
        
    }
}
