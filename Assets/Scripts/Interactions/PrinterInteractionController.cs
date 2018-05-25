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
        if (other.tag == Tags.player)
        {
            SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
            bool isHidden = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN];
            float dotMagnitude = Vector3.Dot((transform.position - hidePosition.position), direction);
            if (isHidden)
            {
                PrivModifiedMove(direction, oicCaller, other);
            }
            else
            {
                otherPcc.ImplementedMove(direction);
            }
        }
    }

    private void PrivModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {

        float dotMagnitude = Vector3.Dot((transform.position - hidePosition.position), direction);
        SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
        otherPcc.transform.rotation = Quaternion.LookRotation(Math.Sign(dotMagnitude)*Vector3.Scale(transform.position - hidePosition.position, Vector3.up).normalized, Vector3.up);
        otherPcc.pcc_animator.SetFloat("Speed", 0f, otherPcc.speedDamptime, Time.deltaTime);
    }
}
