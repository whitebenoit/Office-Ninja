using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractionController : ObjectInteractionController
{
    public string text;
    //private Canvas canvas;
    private TextManager textMan;

    private void Awake()
    {
        //canvas = GameObject.FindGameObjectWithTag(Tags.textUI).GetComponent<Canvas>();
        SpawnButton();
        textMan = GameObject.FindGameObjectWithTag(Tags.textUI).GetComponent<TextManager>();
    }
    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            SplinePlayerCharacterController otherPcc = other.transform.GetComponent<SplinePlayerCharacterController>();
            bool isReading = otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.READING];
            if (isReading)
            {
                if (textMan.isFinished)
                {
                    otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.READING] = false;
                    otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = false;
                    textMan.textComp.text = "";
                    textMan.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    textMan.ForceText();
                }
            }
            else
            {
                // Start reading
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.READING] = true;
                otherPcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = true;
                textMan.textComp.text = text;
                textMan.transform.parent.gameObject.SetActive(true);
                textMan.TypeText();
            }
        }
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
