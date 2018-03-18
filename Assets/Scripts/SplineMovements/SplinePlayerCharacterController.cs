using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class SplinePlayerCharacterController : MonoBehaviour
{
    public float charSpeed = 1f;
    public float turnSmooth = 15f;
    public float speedDamptime = 0.1f;
    public SplineLine lSpline;
    public float progress = 0;
    public Vector3 TESTSplinePosition;

    private Animator pcc_animator;
    private Rigidbody pcc_rigidbody;
    private Collider pcc_collider;

    [HideInInspector]
    public enum ActionListElement { HIDE, INTERACT, DASH, USE };

    public delegate void NextAction(ObjectInteractionController oicCaller, Collider other);
    public struct NextActionStruct
    {
        public ObjectInteractionController oicCaller;
        public Collider other;
        public NextAction Interaction;
    }
    [HideInInspector]
    public List<NextActionStruct> hideActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<NextActionStruct> interactActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<NextActionStruct> dashActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<NextActionStruct> useActionList = new List<NextActionStruct>();

    private void Awake()
    {
        pcc_animator = GetComponent<Animator>();
        pcc_rigidbody = GetComponent<Rigidbody>();
        pcc_collider = GetComponent<Collider>();
    }


    public void AddAction(ActionListElement actionName, NextAction nextAction, ObjectInteractionController oicCaller, Collider other)
    {
        NextActionStruct NAStructToAdd = new NextActionStruct
        {
            Interaction = nextAction,
            oicCaller = oicCaller,
            other = other
        };
        switch (actionName)
        {
            case ActionListElement.HIDE:
                hideActionList.Add(NAStructToAdd);
                break;
            case ActionListElement.INTERACT:
                interactActionList.Add(NAStructToAdd);
                break;
            case ActionListElement.DASH:
                dashActionList.Add(NAStructToAdd);
                break;
            case ActionListElement.USE:
                useActionList.Add(NAStructToAdd);
                break;
            default:
                break;
        }
    }

    public void RemoveAction(ActionListElement actionName, NextAction nextAction, ObjectInteractionController oicCaller, Collider other)
    {
        List<NextActionStruct> listToRemoveFrom;
        switch (actionName)
        {
            case ActionListElement.HIDE:
                listToRemoveFrom = hideActionList;
                break;
            case ActionListElement.INTERACT:
                listToRemoveFrom = interactActionList;
                break;
            case ActionListElement.DASH:
                listToRemoveFrom = dashActionList;
                break;
            case ActionListElement.USE:
                listToRemoveFrom = useActionList;
                break;
            default:
                listToRemoveFrom = null;
                break;
        }

        if (listToRemoveFrom != null)
        {
            for (int i = 0; i < listToRemoveFrom.Count; i++)
            {
                NextActionStruct currNAS = listToRemoveFrom[i];
                if (oicCaller == currNAS.oicCaller
                    && other == currNAS.other
                    && nextAction == currNAS.Interaction)
                {
                    listToRemoveFrom.Remove(currNAS);
                    break;
                }
            }
        }

    }

    public void DoAction(ActionListElement actionName)
    {
        switch (actionName)
        {
            case ActionListElement.HIDE:
                if (hideActionList.Count != 0)
                {
                    NextActionStruct NAStruct = hideActionList[0];
                    NAStruct.Interaction(NAStruct.oicCaller, NAStruct.other);
                }
                break;
            case ActionListElement.INTERACT:
                if (interactActionList.Count != 0)
                {
                    NextActionStruct NAStruct = interactActionList[0];
                    NAStruct.Interaction(NAStruct.oicCaller, NAStruct.other);
                }
                break;
            case ActionListElement.DASH:
                if (dashActionList.Count != 0)
                {
                    NextActionStruct NAStruct = dashActionList[0];
                    NAStruct.Interaction(NAStruct.oicCaller, NAStruct.other);
                }
                break;
            case ActionListElement.USE:
                if (useActionList.Count != 0)
                {
                    NextActionStruct NAStruct = useActionList[0];
                    NAStruct.Interaction(NAStruct.oicCaller, NAStruct.other);
                }
                break;
            default:
                break;
        }
    }

    //private void Update()
    //{
    //    progress = lSpline.GetLengthAtDistFromParametric(charSpeed, progress);
    //    transform.position = lSpline.GetPoint(progress);
    //}

    public void Move(Vector3 direction)
    {
        if (lSpline != null)
        {
            float dotMagnitude = Vector3.Dot(lSpline.GetDirection(progress).normalized, direction);
            if (Math.Abs(dotMagnitude) > 0.05f)
            {
                // Lerp-Rotate the rigidbody toward the direction
                //Math.Sign(dotMagnitude) * charSpeed * Time.deltaTime
                progress = lSpline.GetLengthAtDistFromParametric(Math.Sign(dotMagnitude) * charSpeed * Time.deltaTime, progress);
                Vector3 newPosition = lSpline.GetPoint(progress);

                Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * lSpline.GetDirection(progress).normalized, Vector3.up);
                Quaternion newRotation = Quaternion.Lerp(pcc_rigidbody.rotation, targetRotation, turnSmooth);

                transform.SetPositionAndRotation(newPosition, newRotation);

                Debug.ClearDeveloperConsole();
                Debug.Log("targetRotation =" + targetRotation.ToString());
                //pcc_rigidbody.rotation = targetRotation;
                //Quaternion targetRotation = Quaternion.LookRotation(Math.Sign(dotMagnitude) * lSpline.GetDirection(progress).normalized, Vector3.up);
                //Quaternion newRotation = Quaternion.Lerp(pcc_rigidbody.rotation, targetRotation, turnSmooth);
                //pcc_rigidbody.MoveRotation(targetRotation);

                pcc_animator.SetFloat("Speed", 5.7f, speedDamptime, Time.deltaTime);
            }
            else
            {
                pcc_animator.SetFloat("Speed", 0f, speedDamptime, Time.deltaTime);
            }
        }
        
    }
}

