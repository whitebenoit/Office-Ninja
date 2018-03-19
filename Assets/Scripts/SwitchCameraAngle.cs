using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraAngle : MonoBehaviour {
    public SplineCameraController.cameraPosition newCameraPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SplineCameraController mCC = Camera.main.GetComponent<SplineCameraController>();
            if(mCC != null)
            {
                mCC.currCameraPos = newCameraPosition;
            }
        }
    }
}
