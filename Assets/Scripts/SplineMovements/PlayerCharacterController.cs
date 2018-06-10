using EazyTools.SoundManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public abstract class PlayerCharacterController : MonoBehaviour {


    [Header("Models")]
    public GameObject salarymanModel;
    public GameObject ninjaModel;

    [HideInInspector]
    public Animator pcc_animator;
    [HideInInspector]
    public Rigidbody pcc_rigidbody;
    protected Collider pcc_collider;

    public float speedDamptime = 0.0f;

    [Header("Dectection Cooldown")]
    public float dectectionCd = 10;
    [HideInInspector]
    public float lastDectectionTime = -10000;

    [HideInInspector]
    public enum ActionListElement { HIDE, INTERACT, DASH, USE };

    [HideInInspector]
    public enum StatusListElement { NINJA, ROOTED, BLOCKED, HIDDEN, READING , BEHINDPOT, DETECTED};
    public Dictionary<StatusListElement,bool> currPlayerStatus;

    public Dictionary<Dictionaries.ItemName, bool> currPlayerObjectStatus;

    public delegate void MoveAction(Vector3 direction, ObjectInteractionController oicCaller, Collider other);
    public struct MoveStruct
    {
        public ObjectInteractionController oicCaller;
        public Collider other;
        public MoveAction moveAction;

        public void Move(Vector3 direction)
        {
            moveAction(direction, oicCaller, other);
        }
    }

    public delegate void NextAction(ObjectInteractionController oicCaller, Collider other);
    public struct NextActionStruct
    {
        public ObjectInteractionController oicCaller;
        public Collider other;
        public NextAction Interaction;
    }

    [HideInInspector]
    public int audioDetected;

    [HideInInspector]
    public List<NextActionStruct> hideActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<NextActionStruct> interactActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<NextActionStruct> dashActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<NextActionStruct> useActionList = new List<NextActionStruct>();
    [HideInInspector]
    public List<MoveStruct> moveList = new List<MoveStruct>();


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


        currPlayerObjectStatus = new Dictionary<Dictionaries.ItemName, bool>();
        foreach (Dictionaries.ItemName key in Enum.GetValues(typeof(Dictionaries.ItemName)))
        {
            currPlayerObjectStatus.Add(key, false);
        }
        UpdateObjectStatus();

        PlayerUnitytoSpineController pUSCDetecte = StateMachineBehaviourUtilities.GetBehaviourByName<PlayerUnitytoSpineController>(this.pcc_animator, "Detecte");
        pUSCDetecte.onStateExitCallbacks.Add(() =>
        {
            this.ChangeStatus(StatusListElement.BLOCKED, false);
            this.ChangeStatus(StatusListElement.DETECTED, false);
        });
        
    }
    private void Start()
    {
        audioDetected = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.DETECTED];
    }



    public void AddMove(MoveAction moveAction, ObjectInteractionController oicCaller, Collider other)
    {
        MoveStruct MStructToAdd = new MoveStruct
        {
            moveAction = moveAction,
            oicCaller = oicCaller,
            other = other
        };
        moveList.Add(MStructToAdd);
    }

    public void RemoveMove(MoveAction moveAction, ObjectInteractionController oicCaller, Collider other)
    {
        MoveStruct MStructToAdd = new MoveStruct
        {
            moveAction = moveAction,
            oicCaller = oicCaller,
            other = other
        };
        if (moveList != null)
        {
            for (int i = 0; i < moveList.Count; i++)
            {
                if(moveAction == moveList[i].moveAction
                    && oicCaller == moveList[i].oicCaller
                    && other == moveList[i].other)
                {
                    moveList.Remove(moveList[i]);
                    break;
                }
            }
        }
    }

    public void Move(Vector3 direction)
    {
        if (!currPlayerStatus[StatusListElement.BLOCKED])
        {
            if (moveList.Count != 0)
            {
                moveList[0].Move(direction);
            }
            else ImplementedMove(direction);
        }
        //}
        //else
        //{
        //    pcc_animator.SetFloat("Speed", 0f, speedDamptime, Time.deltaTime);
        //}
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


    public void ChangeStatus(StatusListElement statusListElmt)
    {
        if (currPlayerStatus[statusListElmt]) ChangeStatus(statusListElmt, false);
        else ChangeStatus(statusListElmt, true);
    }


    public void ChangeStatus(StatusListElement statusListElmt, bool valueBool)
    {
        switch (statusListElmt)
        {
            case StatusListElement.NINJA:
                salarymanModel.SetActive(!valueBool);
                ninjaModel.SetActive(valueBool);
                pcc_animator.SetBool("isNinja", valueBool);
                currPlayerStatus[statusListElmt] = valueBool;
                //UpdateEmployeeSight();
                break;
            case StatusListElement.HIDDEN:
                pcc_animator.SetBool("isHiding", valueBool);
                currPlayerStatus[statusListElmt] = valueBool;
                break;
            case StatusListElement.DETECTED:
                //if (!valueBool)
                //{
                //    pcc_animator.SetBool("isDetecte", valueBool);
                //}
                // else if(dectectionCd < Time.timeSinceLevelLoad - lastDectectionTime)
                //{
                //    lastDectectionTime = Time.timeSinceLevelLoad;
                //    pcc_animator.SetBool("isDetecte", valueBool);
                //}
                if (valueBool)
                {
                    pcc_animator.SetTrigger("isDetecte");
                    if (SoundManager.soundsAudio.Count <= 0)
                        Dictionaries.instance.FillDictionaries();
                    SoundManager.GetAudio(audioDetected).Play();
                }
                currPlayerStatus[statusListElmt] = valueBool;
                break;
            default:
                currPlayerStatus[statusListElmt] = valueBool;
                break;
        }
    }

    public void UpdateObjectStatus()
    {
        GameMasterManager gMM = GameMasterManager.instance;
        currPlayerObjectStatus[Dictionaries.ItemName.SCREWDRIVER] = gMM.gd_currentLevel.isScrewUnlocked;
        currPlayerObjectStatus[Dictionaries.ItemName.LAXATIVE] = gMM.gd_currentLevel.isLaxaUnlocked;

    }

}
