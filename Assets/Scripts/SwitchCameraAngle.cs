using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraAngle : MonoBehaviour {
    public MainCameraController.cameraPosition newCameraPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MainCameraController mCC = Camera.main.GetComponent<MainCameraController>();
            if(mCC != null)
            {
                mCC.currCameraPos = newCameraPosition;
            }
        }
    }
}
