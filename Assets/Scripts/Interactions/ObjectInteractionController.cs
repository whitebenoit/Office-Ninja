using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class ObjectInteractionController : MonoBehaviour {

    public Vector3 buttonPosition = new Vector3(-6, 2, 2);
    //private GameObject buttonUIGO_TEST ;
    private ButtonUIImageController btnUIImageController;
    private PlayerCharacterController pcc;
    private GameObject buttonCanvasGO;
    public PlayerCharacterController.ActionListElement actionType;
    public abstract void Interaction(ObjectInteractionController oicCaller, Collider other);

    private void Awake()
    {
        buttonCanvasGO = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonUICanvas"), this.transform.TransformVector(buttonPosition), new Quaternion());
        buttonCanvasGO.transform.SetParent(this.transform);
        btnUIImageController = buttonCanvasGO.GetComponent<ButtonUIImageController>();
        HideInteraction();
    }


    private void OnTriggerEnter(Collider other)
    {
        AddInteraction(other);
        ShowInteraction();
    }

  

    private void OnTriggerExit(Collider other)
    {
        RemoveInteraction(other);
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


    private void ShowInteraction()
    {
        buttonCanvasGO.SetActive(true);
    }

    private void HideInteraction()
    {
        buttonCanvasGO.SetActive(false);
    }

}
