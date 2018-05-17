using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class ObjectInteractionController : MonoBehaviour {

    public bool isNinjaOnly = false;
    public bool isSalaryManOnly = false;
    public Vector3 buttonPosition = new Vector3(0, 2, 0);
    //private GameObject buttonUIGO_TEST ;
    private ButtonUIImageController btnUIImageController;
    private PlayerCharacterController pcc;
    private GameObject buttonCanvasGO;
    public PlayerCharacterController.ActionListElement actionType;


    public abstract void Interaction(ObjectInteractionController oicCaller, Collider other);

    public bool isModifyingMove = false;
    protected abstract void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other);

    private void Awake()
    {
        //Debug.Log(transform.gameObject.name);
        SpawnButton();
    }

    protected void SpawnButton()
    {
        buttonCanvasGO = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonUICanvas"), this.transform.TransformVector(buttonPosition), Quaternion.LookRotation(this.transform.forward));
        buttonCanvasGO.transform.position = this.transform.TransformPoint(buttonPosition);
        buttonCanvasGO.transform.SetParent(this.transform);
        btnUIImageController = buttonCanvasGO.GetComponent<ButtonUIImageController>();
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

    private bool checkTransfoStatus(bool isNinja)
    {
        return (!isNinjaOnly && !isSalaryManOnly) || (isNinja && isNinjaOnly) || (!isNinja && isSalaryManOnly) ;
    }


    private void AddInteraction(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
                if (checkTransfoStatus(isNinja))
                {
                    pcc.AddAction(actionType, Interaction, this, other);
                }
            }
        }
    }

    private void RemoveInteraction(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
                if (checkTransfoStatus(isNinja))
                {
                    pcc.RemoveAction(actionType, Interaction, this, other);
                }
            }
        }
    }

    private void AddMove(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
                if (checkTransfoStatus(isNinja))
                {
                    pcc.AddMove(ModifiedMove, this, other);
                }
            }
        }
    }

    private void RemoveMove(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
                if (checkTransfoStatus(isNinja))
                {
                    pcc.RemoveMove(ModifiedMove, this, other);
                }
            }
        }
    }


    private void ShowInteraction(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
                if (checkTransfoStatus(isNinja))
                {
                    buttonCanvasGO.SetActive(true);
                }
            }
        }
        
    }

    private void HideInteraction(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                bool isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
                if (checkTransfoStatus(isNinja))
                {
                    buttonCanvasGO.SetActive(false);
                }
            }
        }
    }

}
