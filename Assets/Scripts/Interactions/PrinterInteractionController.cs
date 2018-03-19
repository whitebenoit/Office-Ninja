using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterInteractionController : ObjectInteractionController
{
    public Transform hidePosition;
    public Transform outPosition;
    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            PlayerCharacterController otherPcc = other.transform.GetComponent<PlayerCharacterController>();
            bool isHidden = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN];
            bool isRooted = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN];
            if (!isHidden)
            {
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = true;
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN] = true;
                otherPcc.transform.SetPositionAndRotation(hidePosition.position, otherPcc.transform.rotation);
            }
            if (isRooted)
            {
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN] = false;
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = false;
                otherPcc.Move(otherPcc.transform.forward);
            }

        }
        
        
    }
}
