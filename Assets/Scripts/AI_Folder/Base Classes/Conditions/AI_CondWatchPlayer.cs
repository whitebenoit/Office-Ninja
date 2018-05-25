using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Condition/WatchPlayer")]
public class AI_CondWatchPlayer : AI_Condition {

    public bool isNinjaOnly = false;
    public bool isSalarymanOnly = false;
    public Color gizmosColor = Color.red;
    private GameObject gameObjectToWatchFor;
    private string tag = Tags.player;
    private SplinePlayerCharacterController.StatusListElement statusHIDDEN = PlayerCharacterController.StatusListElement.HIDDEN;

    public float height = 1;
    public float watchRange = 2;
    public float watchRadius = 90;

    private void Awake()
    {
        GameObject[] lsGO = GameObject.FindGameObjectsWithTag(tag);
        if(lsGO.Length > 0)
        {
             gameObjectToWatchFor = GameObject.FindGameObjectsWithTag(tag)[0];
        }
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
        SplinePlayerCharacterController spcc = gameObjectToWatchFor.GetComponent<SplinePlayerCharacterController>();
        if( (!isNinjaOnly && !isSalarymanOnly) ||
            (isNinjaOnly && spcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA]) ||
            (isSalarymanOnly && !spcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA]))
        {
            if (!spcc.currPlayerStatus[statusHIDDEN])
            {
                Vector3 gORelatPos = gameObjectToWatchFor.transform.position - brain.transform.position;
                //Debug.Log("gameObjectToWatchFor" + gameObjectToWatchFor.transform.position);
                if (gORelatPos.magnitude < watchRange)
                {
                    //Debug.Log("Player Found");
                    Vector3 frwdVect = brain.transform.forward;
                    float angle = Vector3.Angle(frwdVect, gORelatPos);
                    if (angle < watchRadius) return true;
                }
            }
        }
        
        return false;

        //\\ METHODE SPRITE WITH COLLIDER
        
    }

    public override void InitializeStateForBrain(AI_BehaviorBrain brain)
    {

    }

    public override void EndStateForBrain(AI_BehaviorBrain brain)
    {

    }
}
