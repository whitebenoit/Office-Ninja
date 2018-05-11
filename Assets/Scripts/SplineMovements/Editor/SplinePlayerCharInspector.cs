using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SplinePlayerCharacterController))]
public class SplinePlayerCharInspector : Editor{

    private float progressPosition;
    private Vector3 charDotSpeed;
    private SplinePlayerCharacterController spcc;

    private void OnSceneGUI()
    {
        spcc = target as SplinePlayerCharacterController;

        Handles.color = Color.yellow;
        Vector3 centerVect = spcc.transform.position + new Vector3(0, 1, 0);
        Handles.DrawLine(centerVect, centerVect + spcc.transform.forward);


    }
}
