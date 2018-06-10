using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFloorInteractionController : ObjectInteractionController {

    public int floorDoorNum = 0;
    [Header("Next Food properties")]
    public int nextfloorDoorNum = 0;
    public string nextSceneName;


    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        SplinePlayerCharacterController spcc = other.GetComponent<SplinePlayerCharacterController>();
        GameMasterManager.instance.cfd_nextLevel = new ChangeFloorData(nextfloorDoorNum, spcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA]);
        if (nextSceneName != null)
            SceneManager.LoadScene(nextSceneName);
        else
            Debug.LogError("Missing Scene Name");
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
    
}
