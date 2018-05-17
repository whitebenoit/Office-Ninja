using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "AI_Enjmin/Action/Patrol")]
public class AI_PatrolAction : AI_Action
{
    private SplineLine ptrSplineLine;
    public float currentTransformProgress;
    public float speedDamptime = 0.1f;

    public override void Act(AI_BehaviorBrain brain)
    {
        Patrol(brain);
    }

    private void Patrol(AI_BehaviorBrain brain)
    {
        
        NavMeshAgent agent = brain.navMeshAgent;
        AI_PatrolData patrolData = brain.GetComponent<AI_PatrolData>();
        ptrSplineLine = patrolData.ptrSplineLine;
        currentTransformProgress = patrolData.currentTransformProgress;

        if (agent != null)
        {
            //Vector3 nextDest = ptrSplineLine.GetNextControlPoint(currentTransformProgress);
            agent.destination = ptrSplineLine.transform.TransformPoint(ptrSplineLine.GetNextControlPoint(currentTransformProgress));
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance
                && !agent.pathPending)
            {
                int pointCnt = ptrSplineLine.GetControlPointCount;
                int nextPointIndex = ptrSplineLine.GetNextControlPointIndex((int)(currentTransformProgress * pointCnt));
                patrolData.currentTransformProgress = (float)nextPointIndex / pointCnt;
            }
            float agSpeed = agent.speed;
            if (agSpeed > 0.1f) brain.brain_animator.SetFloat("Speed", agent.speed, speedDamptime, Time.deltaTime);
            else brain.brain_animator.SetFloat("Speed", 0f, speedDamptime, Time.deltaTime);

        }
    }
}
