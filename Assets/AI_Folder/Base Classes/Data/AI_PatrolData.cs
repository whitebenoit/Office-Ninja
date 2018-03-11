using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_PatrolData : MonoBehaviour {

    public Transform[] patrolPoints;
    public int startPatrolPointIndex = 0;
    public int currentTransformIndex;

    private void OnEnable()
    {
        currentTransformIndex = startPatrolPointIndex;
    }

}
