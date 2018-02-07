using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacterController : MonoBehaviour {

    public float turnSmooth = 15f;
    public float speedDamptime = 0.1f;

    [HideInInspector]
	public enum ActionListElement {HIDE, INTERACT, DASH, USE};

    private Animator pcc_animator;
    private Rigidbody pcc_rigidbody;
    private Collider pcc_collider;

    public delegate void NextAction();

    private void Awake()
    {
        pcc_animator = GetComponent<Animator>();
        pcc_rigidbody = GetComponent<Rigidbody>();
        pcc_collider = GetComponent<Collider>();
    }





    public void DoAction(ActionListElement actionName)
    {

    }

    public void Move(Vector3 direction)
    {
        if(direction.magnitude > 0.1f)
        {
            // Lerp-Rotate the rigidbody toward the direction
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(pcc_rigidbody.rotation, targetRotation, turnSmooth);
            pcc_rigidbody.MoveRotation(newRotation);

            pcc_animator.SetFloat("Speed",5.7f, speedDamptime, Time.deltaTime);
        }
        else
        {
            pcc_animator.SetFloat("Speed",0f,speedDamptime, Time.deltaTime);
        }
    }
}
