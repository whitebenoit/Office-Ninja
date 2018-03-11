using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_BehaviorBrain : MonoBehaviour {

    public AI_State initialState;
    [HideInInspector] public AI_State currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;


    private void Awake()
    {
        currentState = initialState;
        navMeshAgent = this.transform.GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        AI_State nextState = currentState.UpdateState(this);
        if (nextState != null)
            currentState = nextState;
    }
}
