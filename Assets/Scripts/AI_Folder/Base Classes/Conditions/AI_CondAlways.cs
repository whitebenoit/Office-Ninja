using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Condition/Always")]
public class AI_CondAlways : AI_Condition
{

    public override bool Evaluate(AI_BehaviorBrain brain)
    {
        return true;
    }

    public override void InitializeStateForBrain(AI_BehaviorBrain brain)
    {

    }
    public override void EndStateForBrain(AI_BehaviorBrain brain)
    {

    }
}
