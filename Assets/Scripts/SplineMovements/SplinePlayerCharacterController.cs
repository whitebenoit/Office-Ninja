using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SplinePlayerCharacterController : PlayerCharacterController
{
    [Header("Character Movements")]
    public float charSpeed = 1f;
    public float turnSmooth = 15f;
    [Header("Spline Parameters")]
    public SplineLine lSpline;
    public float progress = 0;
    //public Vector3 TESTSplinePosition;

   

    public override void ImplementedMove(Vector3 direction)
    {
        if (lSpline != null)
        {
            float dotMagnitude = Vector3.Dot(lSpline.GetDirection(progress).normalized, direction);
            if (!currPlayerStatus[StatusListElement.ROOTED])
            {
                if (Math.Abs(dotMagnitude) > 0.05f)
                {
                    // Lerp-Rotate the rigidbody toward the direction
                    //Math.Sign(dotMagnitude) * charSpeed * Time.deltaTime
                    progress = lSpline.GetLengthAtDistFromParametric(Math.Sign(dotMagnitude) * charSpeed * Time.deltaTime, progress);
                    Vector3 newPosition = lSpline.GetPoint(progress);

                    Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * lSpline.GetDirection(progress).normalized, Vector3.up);
                    Quaternion newRotation = Quaternion.Lerp(pcc_rigidbody.rotation, targetRotation, turnSmooth);

                    transform.SetPositionAndRotation(newPosition, newRotation);


                    pcc_animator.SetFloat("Speed", 4.5f, speedDamptime, Time.deltaTime);
                }
                else
                {
                    pcc_animator.SetFloat("Speed", 0f, speedDamptime, Time.deltaTime);
                }
            }
            else
            {
                if (Math.Abs(dotMagnitude) > 0.05f)
                {
                    transform.rotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * lSpline.GetDirection(progress).normalized, Vector3.up);
                }
                pcc_animator.SetFloat("Speed", 0f, speedDamptime, Time.deltaTime);
            }
        }
        
    }
    
}

