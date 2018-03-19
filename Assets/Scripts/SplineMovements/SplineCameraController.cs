using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCameraController : MonoBehaviour {

    public float cameraDistance = 4;
    public float cameraHeight = 1;
    public float forwardCameraDistance = 2;
    public float linearLerpSpeed = 0.1f;
    public float angularLerpSpeed = 0.05f;

    public enum cameraPosition { right, left, front, back };
    public cameraPosition currCameraPos = cameraPosition.left;

    public GameObject gO;

    private Vector3 objPosition;
    private Quaternion objRotation;
    private Vector3 gOforward;

    private void Update()
    {
        objPosition = new Vector3();
        objRotation = new Quaternion();
        gOforward = gO.transform.forward;

        objPosition += gO.transform.position + cameraHeight * Vector3.up;
        switch (currCameraPos)
        {
            case cameraPosition.right:
                objPosition += cameraDistance * Vector3.forward + forwardCameraDistance * Vector3.right * Math.Sign(gOforward.x);
                objRotation = Quaternion.Euler(0, -180f, 0);
                break;
            case cameraPosition.left:
                objPosition += -cameraDistance * Vector3.forward + forwardCameraDistance * Vector3.right * Math.Sign(gOforward.x);
                objRotation = Quaternion.Euler(0, 0, 0);
                break;
            case cameraPosition.front:
                objPosition += cameraDistance * Vector3.right + forwardCameraDistance * Vector3.forward * Math.Sign(gOforward.z);
                objRotation = Quaternion.Euler(0, -90f, 0);
                break;
            case cameraPosition.back:
                objPosition += -cameraDistance * Vector3.right + forwardCameraDistance * Vector3.forward * Math.Sign(gOforward.z);
                objRotation = Quaternion.Euler(0, 90f, 0);
                break;
            default:
                break;
        }

        transform.position = Vector3.Lerp(transform.position, objPosition, linearLerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, objRotation, angularLerpSpeed);
    }

}
