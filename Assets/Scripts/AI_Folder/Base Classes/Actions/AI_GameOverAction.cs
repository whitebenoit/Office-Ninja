using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Action/GameOver")]
public class AI_GameOverAction : AI_Action {

    SplinePlayerCharacterController spcc;
    public float fadeInDuration =  10.0f;
    public string text = "Game Over";


    public override void Act(AI_BehaviorBrain brain)
    {
        spcc = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<SplinePlayerCharacterController>();
        spcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = true;
        FadeInOutController.instance.FadeIn(() =>
        {
            GameMasterManager.instance.Restart(brain);
        }, fadeInDuration, text);

        //GameMasterManager.Instance.Restart(brain);
    }
    
}
