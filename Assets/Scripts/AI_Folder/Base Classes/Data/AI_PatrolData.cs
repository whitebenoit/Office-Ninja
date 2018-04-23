using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_PatrolData : MonoBehaviour {

    public SplineLine ptrSplineLine;
    public float speed = 0.05f;
    public float startPatrolPointProgress = 0;
    public float currentTransformProgress;

    private void OnEnable()
    {
        currentTransformProgress = startPatrolPointProgress;
        transform.GetComponent<NavMeshAgent>().speed = speed;
    }

}
