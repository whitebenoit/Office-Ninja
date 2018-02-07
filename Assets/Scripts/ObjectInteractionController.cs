using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObjectInteractionController : MonoBehaviour {

    public delegate void NextAction();


    private void OnTriggerEnter(Collider other)
    {
        AddInteraction(other);
    }
    private void OnTriggerExit(Collider other)
    {
        RemoveInteraction(other);
    }


    private void AddInteraction(Collider other)
    {
        if (other.tag == "")
        {

        }
    }

    private void RemoveInteraction(Collider other)
    {

    }

}
