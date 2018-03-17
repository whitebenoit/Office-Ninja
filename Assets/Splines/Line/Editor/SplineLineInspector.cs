using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SplineLine))]
public class SplineLineInspector : Editor {

    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private int selectedIndex = -1;


    private SplineLine spline;
    private Transform handleTransform;
    private Quaternion handleRotation;


    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        spline = target as SplineLine;
        DrawTSelector();
        if (selectedIndex >= 0 && selectedIndex < spline.GetControlPointCount)
        {
            DrawSelectedPointInspector();
        }
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }
        

    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Move Point");
            EditorUtility.SetDirty(spline);
            spline.SetControlPoint(selectedIndex, point);
        }
    }

    private void DrawTSelector()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("t value");
        EditorGUI.BeginChangeCheck();
        float t = EditorGUILayout.Slider(spline.t, 0f, 1f);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "T change");
            EditorUtility.SetDirty(spline);
            spline.t = t;
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnSceneGUI()
    {
        spline = target as SplineLine;
        int length = spline.GetControlPointCount;
        Vector3[] points = new Vector3[length];
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
             handleTransform.rotation : Quaternion.identity;
        
        for (int i = 0; i < length; i++)
        {
            points[i] = ShowPoint(i);
            if (i + 1 != length)
                Handles.DrawLine(handleTransform.TransformPoint(spline.GetControlPoint(i)), handleTransform.TransformPoint(spline.GetControlPoint(i + 1)));

        }

        Debug.Log("t_value = "+spline.t+" Point ="+spline.GetPoint(spline.t));
        Handles.color = Color.red;
        Handles.Button(spline.GetPoint(spline.t), Quaternion.identity, handleSize, pickSize, Handles.DotHandleCap);
        
    }


    private Vector3 ShowPoint(int index)
    {

        Vector3 point = handleTransform.TransformPoint((spline.GetControlPoint(index)));
        float size = HandleUtility.GetHandleSize(point);
        Handles.color = Color.white;
        if (Handles.Button(point, handleRotation, size* handleSize, size* pickSize, Handles.DotHandleCap))
        {
            selectedIndex = index;
            Repaint();
        }
        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
            }
        }


        return point;
    }
}
