using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager  {

    public GameData gd_nextLevel = new GameData();
    public string saveName = "Save_001";

    private static GameMasterManager instance;
    private GameMasterManager() {}

    public static GameMasterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameMasterManager();
            }
            return instance;
        }
    }

    public void LevelSetUp()
    {
        if(gd_nextLevel != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
            SplinePlayerCharacterController spcc = player.GetComponent<SplinePlayerCharacterController>();

            if (spcc != null)
            {
                SplineLine spline = spcc.lSpline;
                GameObject[] resGOList = GameObject.FindGameObjectsWithTag(Tags.respawn);
                foreach (GameObject go in resGOList)
                {
                    ToiletInteractionController tIntCont = go.GetComponent<ToiletInteractionController>();
                    if (tIntCont != null)
                    {
                        if (tIntCont.toiletNum == gd_nextLevel.toiletNum)
                        {
                            spcc.transform.position = spline.GetNearestPointOnSpline(tIntCont.transform.position);
                            spcc.progress = spline.GetNearestProgressOnSpline(tIntCont.transform.position);
                            spcc.transform.rotation = Quaternion.LookRotation(spline.GetDirection(spcc.progress));
                            SplineCameraController sMCC = new SplineCameraController();
                            sMCC.gO = player;
                            Vector3 newPos = new Vector3();
                            Quaternion newRot = new Quaternion();
                            sMCC.MoveCamera(ref newPos, ref newRot);
                            Camera.main.transform.position = newPos;
                            Camera.main.transform.rotation = newRot;
                        }
                    }
                }
            }
            gd_nextLevel = null;
        }

    }
	
    public void Restart(MonoBehaviour caller)
    {
        gd_nextLevel = SaveManager.LoadFile(saveName);
        SaveManager.Load(saveName, caller);
    }
}
