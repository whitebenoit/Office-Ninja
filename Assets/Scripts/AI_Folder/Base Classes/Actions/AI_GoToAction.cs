using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "AI_Enjmin/Action/GoTo")]
public class AI_GoToAction : AI_Action
{
    public override void Act(AI_BehaviorBrain brain)
    {
        GoTo(brain);
    }

    private void GoTo(AI_BehaviorBrain brain)
    {
        NavMeshAgent agent = brain.navMeshAgent;
        AI_GoToData goToData = brain.GetComponent<AI_GoToData>();


        if (agent != null && goToData.destination != null) {
            agent.destination = goToData.destination;
            agent.stoppingDistance = goToData.stopDist;
            //if(Vector3.Distance(brain.transform.position, agent.destination)<= agent.stoppingDistance)
            //{
            //    agent.isStopped = true;
            //}
            //else
            //{
            //    agent.isStopped = false;
            //}
        }
    }

}
