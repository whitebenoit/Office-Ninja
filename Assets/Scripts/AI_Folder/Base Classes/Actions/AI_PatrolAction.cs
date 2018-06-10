using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "AI_Enjmin/Action/Patrol")]
public class AI_PatrolAction : AI_Action
{
    private SplineLine ptrSplineLine;
    //[HideInInspector]
    //public float currentTransformProgress;
    public int currentPreviousSplinePoint ;
    public float speedDamptime = 0.1f;
    public float stopDist = 0.1f;

    public override void Act(AI_BehaviorBrain brain)
    {
        Patrol(brain);
    }

    protected void Patrol(AI_BehaviorBrain brain)
    {
        NavMeshAgent agent = brain.navMeshAgent;
        AI_PatrolData patrolData = brain.GetComponent<AI_PatrolData>();
        ptrSplineLine = patrolData.ptrSplineLine;

        if (agent != null && ptrSplineLine !=null)
        {
            

            currentPreviousSplinePoint = patrolData.currentPreviousSplinePoint;
            
            agent.destination = ptrSplineLine.transform.TransformPoint(ptrSplineLine.GetNextControlPoint(currentPreviousSplinePoint));
            agent.isStopped = false;
            agent.stoppingDistance = stopDist;
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                //int pointCnt = ptrSplineLine.GetControlPointCount;
                //int nextPointIndex = ptrSplineLine.GetNextControlPointIndex(currentPreviousSplinePoint);
                ArrivedAtDestination(brain);
            }
        }
    }

    protected void ArrivedAtDestination(AI_BehaviorBrain brain)
    {
        NavMeshAgent agent = brain.navMeshAgent;
        AI_PatrolData patrolData = brain.GetComponent<AI_PatrolData>();
        ptrSplineLine = patrolData.ptrSplineLine;

        if (agent != null && ptrSplineLine != null)
        {
            BeforeChangingDestination(brain);
            patrolData.currentPreviousSplinePoint = ptrSplineLine.GetNextControlPointIndex(patrolData.currentPreviousSplinePoint);
            AfterChangingDestination(brain);
        }
    }

    protected virtual void AfterChangingDestination(AI_BehaviorBrain brain)
    {
        //Debug.Log("OLD AFTER");
    }

    protected virtual void BeforeChangingDestination(AI_BehaviorBrain brain)
    {
        //throw new NotImplementedException();
    }
}
