using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "AI_Enjmin/Action/Patrol")]
public class AI_PatrolAction : AI_Action
{

    public override void Act(AI_BehaviorBrain brain)
    {
        Patrol(brain);
    }

    private void Patrol(AI_BehaviorBrain brain)
    {
        NavMeshAgent agent = brain.GetComponent<NavMeshAgent>();
        AI_PatrolData patrolData = brain.GetComponent<AI_PatrolData>();
        Transform[] patrolPoints = patrolData.patrolPoints;
        int currentTransformIndex = patrolData.currentTransformIndex;

        if (agent != null)
        {
            if(agent.destination != patrolPoints[currentTransformIndex].position)
            {
                agent.destination = patrolPoints[currentTransformIndex].position;
            }
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance
                && !agent.pathPending)
            {
                patrolData.currentTransformIndex = (currentTransformIndex + 1) % patrolPoints.Length;
            }
        }
    }
}
