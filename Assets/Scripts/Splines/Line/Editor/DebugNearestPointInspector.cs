using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugNearestPoint))]
public class DebugNearestPointInspector : Editor {


    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private DebugNearestPoint dnp;
    private Transform handleTransform;
    private Quaternion handleRotation;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        dnp = target as DebugNearestPoint;
        SplineLine spline = dnp.spline;
        Vector3 position = dnp.transform.position;


        DrawDebug("Nearest Progress :", spline.GetNearestProgressOnSpline(position).ToString());

    }

    private void OnSceneGUI()
    {


        dnp = target as DebugNearestPoint;

        handleTransform = dnp.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
             handleTransform.rotation : Quaternion.identity;


        SplineLine spline = dnp.spline;
        Vector3 position = dnp.transform.position;

        Vector3 nearestPoint = spline.GetNearestPointOnSpline(position);
        //Vector3 nearestPoint = spline.GetNextControlPoint(0);
        Debug.Log(nearestPoint.ToString());
        float size = HandleUtility.GetHandleSize(position);
        //spline.transform.TransformPoint(nearestPoint)
        Handles.DrawLine(nearestPoint, position);
        Handles.Button(nearestPoint, Quaternion.identity, size * handleSize, size * pickSize, Handles.DotHandleCap);

    }

    private void DrawDebug(string name, string property)
    {
        EditorGUILayout.LabelField(name + " = " + property);
    }
}
