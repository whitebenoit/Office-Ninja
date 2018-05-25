using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Condition : ScriptableObject
{
    public abstract bool Evaluate(AI_BehaviorBrain brain);

    public abstract void InitializeStateForBrain(AI_BehaviorBrain brain);
    public abstract void EndStateForBrain(AI_BehaviorBrain brain);
}
