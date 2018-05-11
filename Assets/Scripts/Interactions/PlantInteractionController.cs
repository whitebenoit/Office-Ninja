using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractionController : ObjectInteractionController
{
    public Transform hidePosition;
    public SplineLine plantSpline;
    public float plantProgress;

    private void Awake()
    {
        this.isModifyingMove = true;
        SpawnButton();

    }

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
            bool isHidden = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN];
            if (!isHidden)
            {
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN] = true;
                otherPcc.transform.SetPositionAndRotation(hidePosition.position, otherPcc.transform.rotation);
            }
            else
            {
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN] = false;
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
        SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();

        float dotMagnitude = Vector3.Dot(plantSpline.GetDirection(plantProgress).normalized, direction);
        if (Math.Abs(dotMagnitude) > 0.05f)
        {
            // Lerp-Rotate the rigidbody toward the direction
            //Math.Sign(dotMagnitude) * charSpeed * Time.deltaTime
            plantProgress = plantSpline.GetLengthAtDistFromParametric(Math.Sign(dotMagnitude) * otherPcc.charSpeed * Time.deltaTime, plantProgress);
            Vector3 newPosition = plantSpline.GetPoint(plantProgress);

            Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * plantSpline.GetDirection(plantProgress).normalized, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(otherPcc.pcc_rigidbody.rotation, targetRotation, otherPcc.turnSmooth);

            otherPcc.transform.SetPositionAndRotation(newPosition, newRotation);
            transform.position = otherPcc.transform.position - hidePosition.localPosition;


            otherPcc.pcc_animator.SetFloat("Speed", 5.7f, otherPcc.speedDamptime, Time.deltaTime);
        }
        else
        {
            otherPcc.pcc_animator.SetFloat("Speed", 0f, otherPcc.speedDamptime, Time.deltaTime);
        }
    }
}
