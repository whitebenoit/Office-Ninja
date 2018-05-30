using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Condition/Timer")]
public class AI_CondTimer : AI_Condition
{
    public float duration;
    private Dictionary<AI_BehaviorBrain, float> brainList = new Dictionary<AI_BehaviorBrain, float>();

    public bool isCondNoOut = false;
    public AI_Condition ai_CondNoOut;


    public override void InitializeStateForBrain(AI_BehaviorBrain brain)
    {
        brainList.Add(brain, Time.realtimeSinceStartup);
    }

    public override bool Evaluate(AI_BehaviorBrain brain)
    {
        if (isCondNoOut && ai_CondNoOut !=null && ai_CondNoOut.Evaluate(brain))
        {
            brainList[brain] = Time.realtimeSinceStartup;
        }
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
