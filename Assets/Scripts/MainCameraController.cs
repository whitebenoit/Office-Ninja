using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public float cameraDistance = 10;
    public float cameraHeight = 1;
    public float forwardCameraDistance = 3;
    public float linearLerpSpeed = 0.1f;
    public float angularLerpSpeed = 0.05f;

    public enum cameraPosition { right,left,front,back};
    public cameraPosition currCameraPos;

    public GameObject gO;

    private Vector3 objPosition;
    private Quaternion objRotation;


    private void Update()
    {
        objPosition = new Vector3();
        objRotation = new Quaternion();

        objPosition += gO.transform.position + cameraHeight * Vector3.up;
        switch (currCameraPos)
        {
            case cameraPosition.right:
                objPosition += cameraDistance * Vector3.right + forwardCameraDistance * Vector3.forward;
                objRotation = Quaternion.Euler(0, -90f, 0);
                break;
            case cameraPosition.left:
                objPosition += -cameraDistance * Vector3.right - forwardCameraDistance * Vector3.forward;
                objRotation = Quaternion.Euler(0, 90f, 0);
                break;
            case cameraPosition.front:
                objPosition += cameraDistance * Vector3.forward + forwardCameraDistance * Vector3.right;
                objRotation = Quaternion.Euler(0, -180f, 0);
                break;
            case cameraPosition.back:
                objPosition += -cameraDistance * Vector3.forward - forwardCameraDistance * Vector3.right;
                objRotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                break;
        }

        transform.position = Vector3.Lerp(transform.position, objPosition, linearLerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, objRotation, angularLerpSpeed);
    }

}
