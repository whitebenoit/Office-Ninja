using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class ObjectInteractionController : MonoBehaviour {

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
        Debug.Log(transform.gameObject.name);
        SpawnButton();
    }

    protected void SpawnButton()
    {
        buttonCanvasGO = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonUICanvas"), this.transform.TransformVector(buttonPosition), Quaternion.LookRotation(this.transform.forward));
        buttonCanvasGO.transform.position = this.transform.TransformPoint(buttonPosition);
        buttonCanvasGO.transform.SetParent(this.transform);
        btnUIImageController = buttonCanvasGO.GetComponent<ButtonUIImageController>();
        HideInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter:"+transform.gameObject.name);
        AddInteraction(other);
        if (isModifyingMove) AddMove(other);
        ShowInteraction();
    }

  

    private void OnTriggerExit(Collider other)
    {
        RemoveInteraction(other);
        if (isModifyingMove) RemoveMove(other);
        HideInteraction();
    }


    private void AddInteraction(Collider other)
    {
        if (other.tag == Tags.player)
        {
            pcc = other.GetComponent<PlayerCharacterController>();
            if (pcc != null)
            {
                pcc.AddAction(actionType, Interaction, this, other);
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
                pcc.RemoveAction(actionType, Interaction, this, other);
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
                pcc.AddMove(ModifiedMove, this, other);
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
                pcc.RemoveMove(ModifiedMove, this, other);
            }
        }
    }


    private void ShowInteraction()
    {
        buttonCanvasGO.SetActive(true);
    }

    private void HideInteraction()
    {
        buttonCanvasGO.SetActive(false);
    }

}
