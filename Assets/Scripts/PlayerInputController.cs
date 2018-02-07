using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacterController))]
public class PlayerInputController : MonoBehaviour {

    private PlayerCharacterController pic_charContr;
    private Transform pic_CamTrans;

    // 
    private Vector3 pic_CamTransFoward;

    private void Awake()
    {
        if(Camera.main != null)
        {
            pic_CamTrans = Camera.main.transform;
        }

        pic_charContr = GetComponent<PlayerCharacterController>();
    }

    private void FixedUpdate()
    {
        pic_charContr.Move(MovementDirectionCalc());

    }

    private Vector3 MovementDirectionCalc()
    {
        Vector3 moveDirection;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if(pic_CamTrans != null)
        {
            pic_CamTransFoward = Vector3.Scale(pic_CamTrans.forward, new Vector3(1, 0, 1)).normalized;
            moveDirection = v * pic_CamTransFoward + h * pic_CamTrans.right;
        }
        else
        {
            moveDirection = v * Vector3.forward + h * Vector3.right;
        }

        return moveDirection;
    }



}
