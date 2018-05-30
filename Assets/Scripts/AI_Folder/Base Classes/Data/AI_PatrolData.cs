using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_PatrolData : MonoBehaviour {

    public SplineLine ptrSplineLine;
    public float speed = 1.0f;
    //public int startDestinationIndex = 0;
    //public float startPatrolPointProgress = 0;
    //public float currentTransformProgress;
    public int startingDestPointIndex = 0;
    [HideInInspector]
    public int currentPreviousSplinePoint = 0;

    private void OnEnable()
    {
        if(ptrSplineLine != null)
        {
            //Debug.Log("sDPI: " + startingDestPointIndex + " -> " + ptrSplineLine.GetPreviousControlPointIndex(startingDestPointIndex));
            currentPreviousSplinePoint = ptrSplineLine.GetPreviousControlPointIndex(startingDestPointIndex);
        }
        transform.GetComponent<NavMeshAgent>().speed = speed;
    }

}
