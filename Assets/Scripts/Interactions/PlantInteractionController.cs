using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractionController : ObjectInteractionController
{
    public Transform hidePosition;
    public SplineLine plantSpline;
    private float plantProgress;

    private void Awake()
    {
        this.isModifyingMove = true;
        SpawnButton();
        //hidePosition.position = plantSpline.GetNearestPointOnSpline(this.transform.position) - this.transform.position;
        if (plantSpline != null)
            plantProgress = plantSpline.GetNearestProgressOnSpline(this.transform.position);
        else Debug.Log("ERROR: NO Spline for plant :" + transform.position.ToString());

    }

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
            bool isBehindPot = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.BEHINDPOT];
            if (!isBehindPot)
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.BEHINDPOT, true);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, true);
                otherPcc.transform.SetPositionAndRotation(hidePosition.position, otherPcc.transform.rotation);
            }
            else
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, false);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.BEHINDPOT, false);
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
            bool isBehindPot = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.BEHINDPOT];
            if (isBehindPot)
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, true);
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
        SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
        float dotMagnitude = Vector3.Dot(plantSpline.GetDirection(plantProgress).normalized, direction);
        if (!otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED])
        {
            if (Math.Abs(dotMagnitude) > 0.05f)
            {

                // Lerp-Rotate the rigidbody toward the direction

                //Debug.Log(Time.timeSinceLevelLoad+" - SplineDir " + plantSpline.GetDirection(plantProgress).normalized
                //          +"playerDir"+ direction);

                //plantProgress = plantSpline.GetLengthAtDistFromParametric(Math.Sign(dotMagnitude) * otherPcc.charSpeed * Time.deltaTime, plantProgress);
                //Vector3 newPosition = plantSpline.GetPoint(plantProgress);

                //Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * plantSpline.GetDirection(plantProgress).normalized, Vector3.up);
                //Quaternion newRotation = Quaternion.Lerp(otherPcc.pcc_rigidbody.rotation, targetRotation, otherPcc.turnSmooth);

                //transform.SetPositionAndRotation(newPosition, newRotation);


                //otherPcc.pcc_animator.SetFloat("Speed", 4.5f, otherPcc.speedDamptime, Time.deltaTime);

                plantProgress = plantSpline.GetLengthAtDistFromParametric(Math.Sign(dotMagnitude) * otherPcc.charSpeed * Time.deltaTime, plantProgress);
                Vector3 newPosition = plantSpline.GetPoint(plantProgress);

                Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * plantSpline.GetDirection(plantProgress).normalized, Vector3.up);
                Quaternion newRotation = Quaternion.Lerp(otherPcc.pcc_rigidbody.rotation, targetRotation, otherPcc.turnSmooth);

                otherPcc.transform.SetPositionAndRotation(newPosition, newRotation);
                

                transform.position = otherPcc.transform.position + (this.transform.position - hidePosition.position );


                //otherPcc.pcc_animator.SetFloat("Speed", 5.7f, otherPcc.speedDamptime, Time.deltaTime);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, false);
            }
            else
            {
                otherPcc.pcc_animator.SetFloat("Speed", 0f, otherPcc.speedDamptime, Time.deltaTime);
            }
        }
        else
        {
            if (Math.Abs(dotMagnitude) > 0.05f)
            {
                otherPcc.transform.rotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * plantSpline.GetDirection(plantProgress).normalized, Vector3.up);
                otherPcc.pcc_animator.SetFloat("Speed", 0f, otherPcc.speedDamptime, Time.deltaTime);
            }
        }
    }
}
