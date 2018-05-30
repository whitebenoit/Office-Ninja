using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AI_BehaviorBrain : MonoBehaviour {

    Vector3 velocity;
    public float moveSpeedDamp = 0.1f;
    public AI_State initialState;
    //[HideInInspector]
    public AI_State currentState;
                      private AI_State previousState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator brain_animator;
    [HideInInspector] public Rigidbody brain_rigidbody;


    private void Awake()
    {
        currentState = initialState;
        navMeshAgent = this.transform.GetComponent<NavMeshAgent>();
        brain_animator = this.transform.GetComponent<Animator>();
        brain_rigidbody = this.transform.GetComponent<Rigidbody>();
        if(currentState != null)
        {
            currentState.InitializeStateForBrain(this);
        }
    }


    private void Update()
    {
        if (currentState != null)
        {
            AI_State nextState = currentState.UpdateState(this);
            if(nextState is AI_PreviousState)
            {
                nextState = previousState;
            }
            if (nextState != null)
            {
                ChangeState(nextState);
            }
            //Update Animation Speed
            velocity = navMeshAgent.velocity;

            //if (brain_rigidbody.velocity.magnitude > magnitudeMax)
            //{
            //    magnitudeMax = brain_rigidbody.velocity.magnitude;
            //}
            //Debug.Log(gameObject.name + " - RB " + brain_rigidbody.velocity.magnitude.ToString());
            //Debug.Log(gameObject.name + " - AN " + brain_animator.velocity.magnitude.ToString());
            //Debug.Log(gameObject.name + " - NM " + navMeshAgent.velocity.magnitude.ToString());

            if (velocity.magnitude > 1.6f) { brain_animator.SetFloat("Speed", 5.6f, moveSpeedDamp, Time.deltaTime); }
            else if (velocity.magnitude > 0.1f) { brain_animator.SetFloat("Speed", 2.0f, moveSpeedDamp, Time.deltaTime); }
            else { brain_animator.SetFloat("Speed", 0f, moveSpeedDamp, Time.deltaTime); }
        }
    }

    public void ChangeState(AI_State nextState)
    {
        currentState.EndStateForBrain(this);
        previousState = currentState;
        currentState = nextState;
        currentState.InitializeStateForBrain(this);
    }
}
