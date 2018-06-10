using EazyTools.SoundManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class ObjectInteractionController : MonoBehaviour {

    public bool isNinjaOnly = false;
    public bool isSalaryManOnly = false;
    public bool isRequireObject = false;
    public Dictionaries.ItemName requiredObject;
    public Vector3 buttonPosition = new Vector3(0, 2, 0);
    //private GameObject buttonUIGO_TEST ;
    private int audioTPID;
    private ButtonUIImageController btnUIImageController;
    protected PlayerCharacterController pcc;
    private GameObject buttonCanvasGO;
    public PlayerCharacterController.ActionListElement actionType;


    public abstract void Interaction(ObjectInteractionController oicCaller, Collider other);

    public bool isModifyingMove = false;
    protected abstract void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other);

    private void Awake()
    {
        //Debug.Log(transform.gameObject.name);
        SpawnButton();
        //audioTPID = SoundManager.PlaySound((AudioClip)Resources.Load("Sounds/audioTP"));
        //SoundManager.GetAudio(audioTPID).Stop();
    }

    private void Start()
    {
        AddAudioObject();
    }
    protected void AddAudioObject()
    {
        audioTPID = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.TP];
    }

    protected void SpawnButton()
    {
        buttonCanvasGO = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonUICanvas"), this.transform.TransformVector(buttonPosition), Quaternion.LookRotation(this.transform.forward));
        buttonCanvasGO.transform.position = this.transform.TransformPoint(buttonPosition);
        buttonCanvasGO.transform.SetParent(this.transform);
        btnUIImageController = buttonCanvasGO.GetComponent<ButtonUIImageController>();
        btnUIImageController.SetAction(actionType);
        buttonCanvasGO.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter:"+transform.gameObject.name);
        AddInteraction(other);
        if (isModifyingMove) AddMove(other);
        ShowInteraction(other);
    }



    private void OnTriggerExit(Collider other)
    {
        RemoveInteraction(other);
        if (isModifyingMove) RemoveMove(other);
        HideInteraction(other);
    }

    private bool CheckTransfoStatus(PlayerCharacterController pcc)
    {
        bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
        return (!isNinjaOnly && !isSalaryManOnly) || (isNinja && isNinjaOnly) || (!isNinja && isSalaryManOnly);
    }

    private bool CheckObjectStatus(PlayerCharacterController pcc)
    {
        if (isRequireObject)
        {
            return pcc.currPlayerObjectStatus[requiredObject];
        }
        else
        {
            return true;
        }
    }

    protected bool CheckInteractionValidity(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                return (CheckTransfoStatus(pcc) && CheckObjectStatus(pcc));
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void AddInteraction(Collider other)
    {
        if (CheckInteractionValidity(other))
        {

            pcc.AddAction(actionType, Interaction, this, other);
        }
    }

    private void RemoveInteraction(Collider other)
    {
        if (CheckInteractionValidity(other))
        {
            pcc.RemoveAction(actionType, Interaction, this, other);
        }
    }

    private void AddMove(Collider other)
    {
        if (CheckInteractionValidity(other))
        {
            pcc.AddMove(ModifiedMove, this, other);
        }
    }

    private void RemoveMove(Collider other)
    {
        if (CheckInteractionValidity(other))
        {
            pcc.RemoveMove(ModifiedMove, this, other);
        }
    }


    private void ShowInteraction(Collider other)
    {
        if (CheckInteractionValidity(other))
        {
            buttonCanvasGO.SetActive(true);
        }
    }

    private void HideInteraction(Collider other)
    {
        if (CheckInteractionValidity(other))
        {
            buttonCanvasGO.SetActive(false);
        }
    }




    protected void TpPlayerOut(Vector3 nextPos, Quaternion nextRot, PlayerCharacterController pcc)
    {
        //Animator anim = pcc.pcc_animator;
        //PlayerUnitytoSpineController pUSC = pcc.pcc_animator.GetBehaviour<PlayerUnitytoSpineController>();

        PlayerUnitytoSpineController pUSC = StateMachineBehaviourUtilities.GetBehaviourByName<PlayerUnitytoSpineController>(pcc.pcc_animator, "TP Out");
        
        pUSC.onStateExitCallbacks.Add(() =>
        {
            TpOutPlayerDelegate(nextPos, nextRot);
            pUSC.onStateExitCallbacks.Clear();
        });
        pcc.pcc_animator.SetBool("isTPOut", true);
        SoundManager.GetAudio(audioTPID).Play();
        GameObject.Instantiate(Resources.Load("Prefabs/TPCloud"), pcc.transform);

    }
    

    private void TpOutPlayerDelegate(Vector3 nextPos, Quaternion nextRot)
    {

        pcc.pcc_animator.SetBool("isTPOut", false);
        PlayerUnitytoSpineController pUSC = StateMachineBehaviourUtilities.GetBehaviourByName<PlayerUnitytoSpineController>(pcc.pcc_animator, "TP In");


        pUSC.onStateExitCallbacks.Add(() =>
        {
            TpInPlayerDelegate();
            pUSC.onStateExitCallbacks.Clear();
        });
        pcc.transform.SetPositionAndRotation(nextPos, nextRot);
        pcc.pcc_animator.SetBool("isTPIn", true);
        GameObject.Instantiate(Resources.Load("Prefabs/TPCLoud"),pcc.transform);
        SoundManager.GetAudio(audioTPID).Play();
    }

    private void TpInPlayerDelegate()
    {
        pcc.pcc_animator.SetBool("isTPIn", false);

    }
}
