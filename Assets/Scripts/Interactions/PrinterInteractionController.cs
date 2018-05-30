using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterInteractionController : ObjectInteractionController
{
    public Transform hidePosition;
    //public Transform outPosition;
    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
            bool isHidden = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN];
            if (!isHidden)
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.ROOTED, true);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, true);
                otherPcc.transform.SetPositionAndRotation(hidePosition.position, otherPcc.transform.rotation);
            }
            else
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, false);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.ROOTED, false);
                otherPcc.progress = otherPcc.lSpline.GetNearestProgressOnSpline(this.transform.position);

                otherPcc.Move(otherPcc.transform.forward);
            }

        }
        
        
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {

    }
    
}
