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

    private void Patrol(AI_BehaviorBrain brain)
    {
        NavMeshAgent agent = brain.navMeshAgent;
        AI_PatrolData patrolData = brain.GetComponent<AI_PatrolData>();
        ptrSplineLine = patrolData.ptrSplineLine;
        //currentTransformProgress = patrolData.currentTransformProgress;

        if (agent != null && ptrSplineLine !=null)
        {
            //Vector3 nextDest = ptrSplineLine.GetNextControlPoint(currentTransformProgress);
            //if(ptrSplineLine.GetControlPointCount> 8)
            //{
            //    Debug.Log(Math.Round(Time.timeSinceLevelLoad, 1)
            //                +"("+ (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) + ")"
            //                + "Arrived Pt ="+ (currentTransformProgress * ptrSplineLine.GetControlPointCount)
            //                + "Futur Pt ="+ ptrSplineLine.GetNextControlPointIndex((int)(currentTransformProgress * ptrSplineLine.GetControlPointCount))
            //                + " CurDest ="+ ptrSplineLine.GetNextControlPoinIndex(currentTransformProgress)
            //                );
            //}

            currentPreviousSplinePoint = patrolData.currentPreviousSplinePoint;

            //if (ptrSplineLine.GetControlPointCount > 8)
            //{
            //    Debug.Log(Math.Round(Time.timeSinceLevelLoad, 1)
            //                + "Dist :" + Vector3.Distance(ptrSplineLine.transform.TransformPoint(ptrSplineLine.GetNextControlPoint(currentPreviousSplinePoint)),
            //                                            brain.transform.position)
            //                + "(" + (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) + ")"
            //                + "CurPrevPt =" + currentPreviousSplinePoint
            //                + "NextPrevPt =" + ptrSplineLine.GetNextControlPointIndex(currentPreviousSplinePoint)
            //                //+ " CurDest =" + ptrSplineLine.GetNextControlPointIndex(currentPreviousSplinePoint)
            //                );
            //}
            agent.destination = ptrSplineLine.transform.TransformPoint(ptrSplineLine.GetNextControlPoint(currentPreviousSplinePoint));
            agent.isStopped = false;
            agent.stoppingDistance = stopDist;
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                //int pointCnt = ptrSplineLine.GetControlPointCount;
                //int nextPointIndex = ptrSplineLine.GetNextControlPointIndex(currentPreviousSplinePoint);
                patrolData.currentPreviousSplinePoint = ptrSplineLine.GetNextControlPointIndex(currentPreviousSplinePoint);
            }

            //agent.destination = ptrSplineLine.transform.TransformPoint(ptrSplineLine.GetNextControlPoint(currentTransformProgress));
            //agent.isStopped = false;
            //agent.stoppingDistance = stopDist;
            //if (agent.remainingDistance <= agent.stoppingDistance
            //    && !agent.pathPending)
            //{
            //    int pointCnt = ptrSplineLine.GetControlPointCount;
            //    int nextPointIndex = ptrSplineLine.GetNextControlPoinIndex(currentTransformProgress);
            //    patrolData.currentTransformProgress = (float)nextPointIndex / pointCnt;
            //}
            //float agSpeed = agent.speed;
            //if (agSpeed > 0.1f) brain.brain_animator.SetFloat("Speed", agent.speed, speedDamptime, Time.deltaTime);
            //else brain.brain_animator.SetFloat("Speed", 0f, speedDamptime, Time.deltaTime);

        }
    }
}
