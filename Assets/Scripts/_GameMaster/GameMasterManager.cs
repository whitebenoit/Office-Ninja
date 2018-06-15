using EazyTools.SoundManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager : MonoBehaviour  {


    [HideInInspector]
    public GameData gd_currentLevel;
    [HideInInspector]
    public GameData gd_nextLevel = new GameData();
    public ChangeFloorData cfd_nextLevel = null; 
    public string saveName = "Save_001";

    private static GameMasterManager _instance = null;
    private static bool initialized = false;
    private GameMasterManager() {}

    

    public static GameMasterManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameMasterManager)FindObjectOfType(typeof(GameMasterManager));
                if (_instance == null)
                {
                    // Create gameObject and add component
                    _instance = (new GameObject("GameMasterManagerObj")).AddComponent<GameMasterManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        instance.Init();
        gd_currentLevel = SaveManager.LoadFile(saveName);
       
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
        if (player != null)
            SetUpSPCC(player.GetComponent<SplinePlayerCharacterController>(), ref gd_currentLevel);
    }
    void Init()
    {
        if (!initialized)
        {
            initialized = true;
            GameMasterManager.DontDestroyOnLoad(this);
        }
    }
    
    
    public void LevelSetUp()
    {
        SoundManager.StopAll();
        PauseController.instance.Init();
        PauseController.instance.ClosePause();
        if(cfd_nextLevel != null)
        {
            LevelSetupChangeFLoor(ref cfd_nextLevel);
        }
        else if (gd_nextLevel != null)
        {
            LevelSetupDoor(ref gd_nextLevel);
        }

    }

    private void LevelSetupDoor(ref GameData gdd)
    {

        GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
        SplinePlayerCharacterController spcc = player.GetComponent<SplinePlayerCharacterController>();

        SetUpSPCC(spcc,ref gdd);

        if (spcc != null)
        {
            SplineLine spline = spcc.lSpline;
            GameObject[] resGOList = GameObject.FindGameObjectsWithTag(Tags.respawn);
            foreach (GameObject go in resGOList)
            {
                ToiletInteractionController tIntCont = go.GetComponent<ToiletInteractionController>();
                if (tIntCont != null)
                {
                    if (tIntCont.toiletNum == gdd.toiletNum)
                    {

                        PositionPlayer(tIntCont.transform, spcc);
                        SplineCameraController mCC = Camera.main.GetComponent<SplineCameraController>();
                        if (mCC != null)
                        {
                            mCC.currCameraPos = tIntCont.nextCameraPosition;
                        }
                    }
                }
            }
        }
        gdd = null;
    }

    private void LevelSetupChangeFLoor(ref ChangeFloorData gfc)
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
        SplinePlayerCharacterController spcc = player.GetComponent<SplinePlayerCharacterController>();
        if(gd_currentLevel ==  null)
            gd_currentLevel = SaveManager.LoadFile(saveName);
        if (spcc.currPlayerObjectStatus[Dictionaries.ItemName.SCREWDRIVER] || gd_currentLevel.isScrewUnlocked || (gd_nextLevel != null && gd_nextLevel.isScrewUnlocked))
        {
            spcc.currPlayerObjectStatus[Dictionaries.ItemName.SCREWDRIVER] = true;
            if (gd_currentLevel != null)
            {
                gd_currentLevel.isScrewUnlocked = true;
                SaveManager.Save(gd_currentLevel, saveName);
            }
            if (gd_nextLevel != null)
                gd_nextLevel.isScrewUnlocked = true;
        }
        SetUpSPCC(spcc, ref gd_currentLevel);

        if (gfc != null)
        {
            ChangeFloorInteractionController[] cficList = GameObject.FindObjectsOfType<ChangeFloorInteractionController>();
            if(cficList.Length > 0)
            {
                ChangeFloorInteractionController correctCFIC = null;
                foreach (ChangeFloorInteractionController curCFIC in cficList)
                {
                    if (curCFIC.floorDoorNum == gfc.floorDoorNum)
                        correctCFIC = curCFIC;
                }

                if(correctCFIC != null)
                {
                    PositionPlayer(correctCFIC.transform, spcc);
                    SplineCameraController mCC = Camera.main.GetComponent<SplineCameraController>();
                    if (mCC != null)
                    {
                        mCC.currCameraPos = correctCFIC.nextCameraPosition;
                    }
                    spcc.ChangeStatus(PlayerCharacterController.StatusListElement.NINJA, gfc.isNinja);
                }
                else
                {
                    Debug.LogError("Correct CFIC not Found");
                }


            }
        }

        gfc = null;

    }

    private void PositionPlayer(Transform transf,SplinePlayerCharacterController spcc)
    {
        spcc.transform.position = spcc.lSpline.GetNearestPointOnSpline(transf.position);
        spcc.progress = spcc.lSpline.GetNearestProgressOnSpline(transf.position);
        spcc.transform.rotation = Quaternion.LookRotation(spcc.lSpline.GetDirection(spcc.progress));

        GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
        SplineCameraController sMCC = new SplineCameraController
        {
            gO = player
        };

        Vector3 newPos = new Vector3();
        Quaternion newRot = new Quaternion();
        sMCC.MoveCamera(ref newPos, ref newRot);

        Camera.main.transform.position = newPos;
        Camera.main.transform.rotation = newRot;
    }

    public void SetUpSPCC(SplinePlayerCharacterController spcc,ref GameData gdd)
    {
        spcc.currPlayerObjectStatus[Dictionaries.ItemName.SCREWDRIVER] = gdd.isScrewUnlocked;
        spcc.currPlayerObjectStatus[Dictionaries.ItemName.LAXATIVE] = gdd.isLaxaUnlocked;
        spcc.ChangeStatus(PlayerCharacterController.StatusListElement.NINJA, gdd.isNinja);
    }
    

    public void Restart(MonoBehaviour caller)
    {
        gd_nextLevel = SaveManager.LoadFile(saveName);
        SaveManager.Load(saveName, caller);
    }
}
