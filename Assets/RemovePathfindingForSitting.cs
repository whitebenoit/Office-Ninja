using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RemovePathfindingForSitting : MonoBehaviour {

    NavMeshAgent navMeshAgent;
    Animator animator;
    AI_BehaviorBrain brain;

    // Use this for initialization
    void Awake () {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        brain = gameObject.GetComponent<AI_BehaviorBrain>();
        animator = gameObject.GetComponent<Animator>();
        if(brain.initialState.brainAnimBoolList != null && brain.initialState.brainAnimBoolList.Length>0 && brain.initialState.brainAnimBoolList[0] == "isSitting")
        {
            navMeshAgent.enabled = false;
        }
        else if (!animator.GetBool("isSitting"))
        {
            navMeshAgent.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
