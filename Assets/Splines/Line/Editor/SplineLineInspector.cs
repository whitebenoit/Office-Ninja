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
        DrawLoopToggle();
        DrawTSelector();
        DrawDistSelector();
        if (selectedIndex >= 0 && selectedIndex < spline.GetControlPointCount)
        {
            DrawSelectedPointInspector();
        }

        EditorGUILayout.BeginHorizontal();
        DrawAddCurveButton();
        DrawRemoveCurveButton();
        EditorGUILayout.EndHorizontal();

    }

    private void DrawAddCurveButton()
    {

        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve(selectedIndex);
            EditorUtility.SetDirty(spline);
        }
    }

    private void DrawRemoveCurveButton()
    {

        if (GUILayout.Button("Remove Curve"))
        {
            Undo.RecordObject(spline, "Remove Curve");
            spline.RemovePoint(selectedIndex);
            EditorUtility.SetDirty(spline);
        }
    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position "+ selectedIndex, spline.GetControlPoint(selectedIndex));
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
            spline.cDistance = spline.GetLength(t);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawDistSelector()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Dist value");
        EditorGUI.BeginChangeCheck();
        float dist = EditorGUILayout.Slider(spline.cDistance, 0f, spline.GetLength(1f));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Dist change");
            EditorUtility.SetDirty(spline);
            spline.cDistance = dist;
            spline.t = spline.GetParametricLength(dist);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawLoopToggle()
    {
        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop",spline.Loop);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Toggle Loop");
            EditorUtility.SetDirty(spline);
            spline.Loop = loop;
        }
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
            {
                Handles.DrawLine(handleTransform.TransformPoint(spline.GetControlPoint(i)), handleTransform.TransformPoint(spline.GetControlPoint(i + 1)));
            }
        }
        Handles.color = Color.red;
        float size = HandleUtility.GetHandleSize(points[0]);
        Handles.Button(spline.GetPoint(spline.t), Quaternion.identity, size*handleSize, size * pickSize, Handles.DotHandleCap);
        // Debug.Log("Length :" + spline.GetLength(spline.t) + "Param : "+ spline.GetParametricLength(spline.GetLength(spline.t)));
    }


    private Vector3 ShowPoint(int index)
    {

        Vector3 point = handleTransform.TransformPoint((spline.GetControlPoint(index)));
        float size = HandleUtility.GetHandleSize(point);
        Handles.color = Color.white;
        if (Handles.Button(point, handleRotation, size* handleSize, size* pickSize, Handles.DotHandleCap))
        {
        //    selectedIndex = index;
        //    Repaint();
        //}
        //if (selectedIndex == index)
        //{
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
