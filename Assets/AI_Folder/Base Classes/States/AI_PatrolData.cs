using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PatrolData : MonoBehaviour {

    public Transform[] patrolPoints;
    public int startPatrolPointIndex = 0;
    public int currentTransformIndex;

    private void OnEnable()
    {
        currentTransformIndex = startPatrolPointIndex;
    }

}
