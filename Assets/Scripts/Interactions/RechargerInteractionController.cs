using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargerInteractionController : ObjectInteractionController
{

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        //string saveName = "Save_001";

        GameMasterManager.instance.Restart(this);

    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
