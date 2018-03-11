using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterInteractionController : ObjectInteractionController
{
    public Transform hidePosition;
    public Transform outPosition;
    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        other.transform.SetPositionAndRotation(hidePosition.position, hidePosition.rotation);
    }
}
