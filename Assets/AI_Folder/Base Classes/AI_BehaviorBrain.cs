using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_BehaviorBrain : MonoBehaviour {

    public AI_State initialState;
    [HideInInspector]
    public AI_State currentState;

    private void Awake()
    {
        currentState = initialState;
    }


    private void Update()
    {
        AI_State nextState = currentState.UpdateState(this);
        if (nextState != null)
            currentState = nextState;
    }
}
