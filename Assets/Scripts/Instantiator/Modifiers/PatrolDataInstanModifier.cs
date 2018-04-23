using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Instantiator))]
public class PatrolDataInstanModifier : InstanModifier {
    
    public SplineLine sl;

    public override void Modify(GameObject gO)
    {
        AI_PatrolData pd = gO.GetComponent<AI_PatrolData>();
        if (pd != null)
        {
            pd.ptrSplineLine = sl;
        }
    }
    

}
