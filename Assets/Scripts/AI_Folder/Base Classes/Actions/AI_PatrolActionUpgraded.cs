using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI_Enjmin/Action/UpgradedPatrol")]
public class AI_PatrolActionUpgraded : AI_PatrolAction
{
    protected override void AfterChangingDestination(AI_BehaviorBrain brain)
    {
        //Debug.Log("NEW AFTER");

    }

    protected override void BeforeChangingDestination(AI_BehaviorBrain brain)
    {
        //Debug.Log("NEW AFTER");

    }

}
