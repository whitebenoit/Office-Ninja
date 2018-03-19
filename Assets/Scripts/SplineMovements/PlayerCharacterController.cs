using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public abstract class PlayerCharacterController : MonoBehaviour {


    protected Animator pcc_animator;
    protected Rigidbody pcc_rigidbody;
    protected Collider pcc_collider;

    [HideInInspector]
    public enum ActionListElement { HIDE, INTERACT, DASH, USE };

    [HideInInspector]
    public enum StatusListElement { ROOTED, HIDDEN };
    public Dictionary<StatusListElement,bool> currPlayerStatus;

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

    protected void Awake()
    {
        pcc_animator = GetComponent<Animator>();
        pcc_rigidbody = GetComponent<Rigidbody>();
        pcc_collider = GetComponent<Collider>();

        currPlayerStatus = new Dictionary<StatusListElement, bool>();
        foreach (StatusListElement key in Enum.GetValues(typeof(StatusListElement)))
        {
            currPlayerStatus.Add(key, false);
        }
    }
    public void Move(Vector3 direction)
    {
        if(!currPlayerStatus[StatusListElement.ROOTED]) ImplementedMove(direction);
    }

    public abstract void ImplementedMove(Vector3 direction);

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

}
