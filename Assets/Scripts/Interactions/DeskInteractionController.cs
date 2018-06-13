using EazyTools.SoundManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteractionController : ObjectInteractionController
{

    public SplineLine deskSpline;
    private float deskProgress;

    private void Awake()
    {
        this.isModifyingMove = true;
        SpawnButton();
        deskProgress = 0;

    }


    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
            bool isHidden = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.HIDDEN];
            if (!isHidden)
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.BEHINDPOT, true);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, true);
                deskProgress = deskSpline.GetNearestProgressOnSpline(otherPcc.transform.position);
                //otherPcc.transform.SetPositionAndRotation(deskSpline.GetNearestPointOnSpline(otherPcc.transform.position),
                //    Quaternion.Euler(deskSpline.transform.TransformVector(deskSpline.GetDirection(deskProgress).normalized)));


                SoundManager.GetAudio(audioTPID).Play();
                GameObject.Instantiate(Resources.Load("Prefabs/TPCloud"),pcc.transform.position, pcc.transform.rotation);

                float dotMagnitude = Vector3.Dot(otherPcc.transform.forward, Camera.main.transform.right);
                //Debug.Log("dotMagnitude:" + dotMagnitude);

                otherPcc.transform.SetPositionAndRotation(deskSpline.GetNearestPointOnSpline(otherPcc.transform.position),
                    Quaternion.LookRotation(dotMagnitude * deskSpline.transform.TransformVector(deskSpline.GetDirection(deskProgress).normalized), Vector3.up));
            }
            else
            {
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, false);
                otherPcc.ChangeStatus(PlayerCharacterController.StatusListElement.BEHINDPOT, false);
                otherPcc.progress = otherPcc.lSpline.GetNearestProgressOnSpline(otherPcc.transform.position);


                SoundManager.GetAudio(audioTPID).Play();
                GameObject.Instantiate(Resources.Load("Prefabs/TPCloud"), pcc.transform.position, pcc.transform.rotation);

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
        float dotMagnitude = Vector3.Dot(deskSpline.transform.TransformVector(deskSpline.GetDirection(deskProgress).normalized), direction);
        if (!otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED])
        {
            if (Math.Abs(dotMagnitude) > 0.05f)
            {

                deskProgress = deskSpline.GetLengthAtDistFromParametric(Math.Sign(dotMagnitude) * otherPcc.charSpeed * Time.deltaTime, deskProgress);
                Vector3 newPosition = deskSpline.GetPoint(deskProgress);

                Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * deskSpline.transform.TransformVector(deskSpline.GetDirection(deskProgress).normalized), Vector3.up);
                Quaternion newRotation = Quaternion.Lerp(otherPcc.pcc_rigidbody.rotation, targetRotation, otherPcc.turnSmooth);

                otherPcc.transform.SetPositionAndRotation(newPosition, newRotation);

                otherPcc.pcc_animator.SetFloat("Speed", 5.7f, otherPcc.speedDamptime, Time.deltaTime);
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
                otherPcc.transform.rotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * deskSpline.GetDirection(deskProgress).normalized, Vector3.up);
                otherPcc.pcc_animator.SetFloat("Speed", 0f, otherPcc.speedDamptime, Time.deltaTime);
            }
        }
    }
}
