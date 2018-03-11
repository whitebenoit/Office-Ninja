using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Condition/Watch")]
public class AI_CondWatch : AI_Condition {

    public Color gizmosColor = Color.red;
    public GameObject gameObjectToWatchFor;
    public float height = 1;
    public float watchRange = 2;
    public float watchSphereRadius = 1;

    


    public override bool Evaluate(AI_BehaviorBrain brain)
    {
        RaycastHit[] rayCastHits;
        rayCastHits = Physics.SphereCastAll(brain.transform.position+ height*brain.transform.up, watchSphereRadius, brain.transform.forward, watchRange);
        //Gizmos.DrawRay(brain.transform.position + height * brain.transform.up, brain.transform.forward);
        foreach (RaycastHit rayCastHit in rayCastHits)
        {
            if (gameObjectToWatchFor.tag.Equals(rayCastHit.transform.gameObject.tag))
            {
                return true;
            }
        }
        return false;
    }
}
