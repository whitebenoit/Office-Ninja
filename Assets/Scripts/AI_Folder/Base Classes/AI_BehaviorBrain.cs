using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AI_BehaviorBrain : MonoBehaviour {

    public AI_State initialState;
    [HideInInspector] public AI_State currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator brain_animator;


    private void Awake()
    {
        currentState = initialState;
        navMeshAgent = this.transform.GetComponent<NavMeshAgent>();
        brain_animator = this.transform.GetComponent<Animator>();
    }


    private void Update()
    {
        AI_State nextState = currentState.UpdateState(this);
        if (nextState != null)
            currentState = nextState;
    }
}
