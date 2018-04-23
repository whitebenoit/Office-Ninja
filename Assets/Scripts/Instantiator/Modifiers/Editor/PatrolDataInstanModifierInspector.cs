using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatrolDataInstanModifier))]
public class PatrolDataInstanModifierInspector : Editor
{
    private PatrolDataInstanModifier instanMod;
    private Transform handleTransform;
    private Quaternion handleRotation;


    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private bool isInstanPosSelected = false;

    private void OnSceneGUI()
    {
        instanMod = target as PatrolDataInstanModifier;
        handleTransform = instanMod.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
             handleTransform.rotation : Quaternion.identity;


    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        instanMod = target as PatrolDataInstanModifier;
        EditorGUILayout.LabelField("Properties");

        //AI_PatrolData pd = instanMod.instan.gOToInst.GetComponent<AI_PatrolData>();
        //if (pd != null)
        //{
            DrawSplineLine("Spline line", ref instanMod.sl);
        //}
        //else
        //{
        //    DrawDebugString("Error, no Ai_PAtrolData found in GameObject");
        //}

    }


    private void DrawSplineLine(string name, ref SplineLine property)
    {
        EditorGUI.BeginChangeCheck();
        SplineLine propertyTemp = (SplineLine)EditorGUILayout.ObjectField(name, property, typeof(SplineLine));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instanMod, name + " change");
            EditorUtility.SetDirty(instanMod);
            property = propertyTemp;
        }
    }

    private void DrawDebugString(string str)
    {
        EditorGUILayout.LabelField(str);
    }
}
