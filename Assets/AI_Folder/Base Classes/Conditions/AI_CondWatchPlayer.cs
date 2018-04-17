using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Condition/WatchPlayer")]
public class AI_CondWatchPlayer : AI_Condition {

    public Color gizmosColor = Color.red;
    private GameObject gameObjectToWatchFor;
    private string tag = Tags.player;
    private SplinePlayerCharacterController.StatusListElement statusHIDDEN = PlayerCharacterController.StatusListElement.HIDDEN;

    public float height = 1;
    public float watchRange = 2;
    public float watchRadius = 35;

    private void Awake()
    {
        gameObjectToWatchFor = GameObject.FindGameObjectsWithTag(tag)[0];
    }



    public override bool Evaluate(AI_BehaviorBrain brain)
    {
        //\\ METHOD RAYCASTING 
        //RaycastHit[] rayCastHits;
        //rayCastHits = Physics.SphereCastAll(brain.transform.position+ height*brain.transform.up, watchSphereRadius, brain.transform.forward, watchRange);
        ////Gizmos.DrawRay(brain.transform.position + height * brain.transform.up, brain.transform.forward);
        //foreach (RaycastHit rayCastHit in rayCastHits)
        //{
        //    if (gameObjectToWatchFor.tag.Equals(rayCastHit.transform.gameObject.tag))
        //    {
        //        return true;
        //    }
        //}
        //return false;

        //\\ METHOD SIGHT CONE
        gameObjectToWatchFor = GameObject.FindGameObjectsWithTag(tag)[0];
        if (! gameObjectToWatchFor.GetComponent<SplinePlayerCharacterController>().currPlayerStatus[statusHIDDEN])
        {
            Vector3 gORelatPos = gameObjectToWatchFor.transform.position - brain.transform.position;
            Debug.Log("gameObjectToWatchFor" + gameObjectToWatchFor.transform.position);
            if (gORelatPos.magnitude < watchRange)
            {
                Debug.Log("Player Found");
                Vector3 frwdVect = brain.transform.forward;
                float angle = Vector3.Angle(frwdVect, gORelatPos);
                if (angle < watchRadius) return true;
            }
        }
        
        return false;

        //\\ METHODE SPRITE WITH COLLIDER
        






        return false;
    }
}
