using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Action/CallManager")]
public class AI_CallManagers : AI_Action
{
    public AI_State newState;
    public float range = 5;
    public int minimumChanged = 1;
    public float stopDist = 1.0f;

    private AI_BehaviorBrain[] managerBrainList;

    public override void Act(AI_BehaviorBrain brain)
    {
        CallManager(brain);
    }

    private void CallManager(AI_BehaviorBrain brain)
    {
        managerBrainList = FindManagers(brain, range, minimumChanged);
        if(managerBrainList.Length >0) SetStates(brain,managerBrainList, newState);
    }

    private AI_BehaviorBrain[] FindManagers(AI_BehaviorBrain brain, float range, int minimumFound)
    {
        List<AI_BehaviorBrain> returnedBrainList = new List<AI_BehaviorBrain>();
        GameObject[] tempManagerList = GameObject.FindGameObjectsWithTag(Tags.manager);
        // Sort by distance
        Array.Sort(tempManagerList,
            delegate (GameObject g1, GameObject g2)
            {
                float dist1 = Vector3.Distance(g1.transform.position, brain.transform.position);
                float dist2 = Vector3.Distance(g2.transform.position, brain.transform.position);
                return (int)(100 * (dist1 - dist2));
            });
        
        // Remove unwanted Managers (outside of range and/or more than min)
        for (int i = 0; i < tempManagerList.Length; i++)
        {
            if (i < minimumFound || Vector3.Distance(tempManagerList[i].transform.position, brain.transform.position) <= range)
            {
                returnedBrainList.Add(tempManagerList[i].GetComponent<AI_BehaviorBrain>());
            }
            else break;
        }
        return returnedBrainList.ToArray();
    }

    private void SetStates(AI_BehaviorBrain brain,AI_BehaviorBrain[] brainList, AI_State state)
    {
        foreach (AI_BehaviorBrain currbrain in brainList)
        {
            currbrain.currentState = state;

            AI_GoToData goToData = currbrain.GetComponent<AI_GoToData>();
            if (goToData != null) {
                goToData.destination = brain.transform.position;
                goToData.stopDist = stopDist;
            }
        }
    }

}
