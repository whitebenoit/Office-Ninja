using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFloorInteractionController : ObjectInteractionController {

    public int floorDoorNum = 0;
    [Header("Next Floor properties")]
    public int nextfloorDoorNum = 0;
    public string nextSceneName;
    public SplineCameraController.cameraPosition nextCameraPosition = SplineCameraController.cameraPosition.front;
    


    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        SplinePlayerCharacterController spcc = other.GetComponent<SplinePlayerCharacterController>();
        GameMasterManager.instance.cfd_nextLevel = new ChangeFloorData(nextfloorDoorNum, spcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA], nextCameraPosition);
        if (nextSceneName != null)
        {
            FadeInOutController.instance.FadeOut(1.0f, "chargement");
            SceneManager.LoadScene(nextSceneName);
        }
        else
            Debug.LogError("Missing Scene Name");

        
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
    
}
