using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CondTimerNoOutCond : AI_Condition
{
    public float duration;
    private Dictionary<AI_BehaviorBrain, float> brainList = new Dictionary<AI_BehaviorBrain, float>();


    public override void InitializeStateForBrain(AI_BehaviorBrain brain)
    {
        brainList.Add(brain, Time.realtimeSinceStartup);
    }

    public override bool Evaluate(AI_BehaviorBrain brain)
    {
        if (brainList.ContainsKey(brain))
        {
            float startTime = brainList[brain];
            if (duration > Time.realtimeSinceStartup - startTime) return false;
            else return true;
        }
        else return false;
    }

    public override void EndStateForBrain(AI_BehaviorBrain brain)
    {
        brainList.Remove(brain);
    }
}
