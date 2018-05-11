using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI_Enjmin/Action/GameOver")]
public class AI_GameOverAction : AI_Action {

    public override void Act(AI_BehaviorBrain brain)
    {
        GameMasterManager.Instance.Restart(brain);
    }
    
}
