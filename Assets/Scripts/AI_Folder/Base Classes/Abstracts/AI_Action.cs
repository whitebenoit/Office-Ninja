using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Action : ScriptableObject
{
    public abstract void Act(AI_BehaviorBrain brain);
    public Dictionaries.AudioName audioToPlay;

    
}
