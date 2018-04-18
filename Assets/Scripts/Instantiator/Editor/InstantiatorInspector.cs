using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Instantiator))]
public class InstantiatorInspector : Editor
{
    private Instantiator instan;
    private Transform handleTransform;
    private Quaternion handleRotation;


    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private bool isInstanPosSelected = false;

    private void OnSceneGUI()
    {
        instan = target as Instantiator;
        handleTransform = instan.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
             handleTransform.rotation : Quaternion.identity;
        DrawSceneHandle("Instantiate Position", ref instan.instantiatePosition,ref isInstanPosSelected);
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        instan = target as Instantiator;
        //DrawDebug("Cuurent Timer", instan.timerValue.ToString());
        //DrawDebug("Cuurent isDelayPassed", instan.isDelayPassed.ToString());
        EditorGUILayout.LabelField("Properties");
        DrawToggle("Start on Awake", ref instan.startOnAwake);
        DrawProperty("Delay", ref instan.isDelay, ref instan.delay);
        DrawProperty("Cooldown", ref instan.isCooldown, ref instan.cooldown);
        DrawProperty("MaxInst", ref instan.isMaxInst, ref instan.maxInst);
        if (instan.isMaxInst)
            DrawToggle("Stop at Max Instance", ref instan.isStopOnMax);
        DrawVector3("Instantiate Position", ref instan.instantiatePosition);
        DrawGameObject("GameObject Instantiated", ref instan.gOToInst);

    }


    //Draw Inspector Properties Functions

    private void DrawProperty(string name, ref bool isProperty,ref float property)
    {

        bool isPropertyTemp = isProperty;

        EditorGUILayout.BeginHorizontal();

        DrawLeftToggle(name, ref isProperty);
        if (isPropertyTemp)
            DrawValue(name, ref property);

        EditorGUILayout.EndHorizontal();
    }
    private void DrawProperty(string name, ref bool isProperty, ref int property)
    {

        bool isPropertyTemp = isProperty;
        int propertyTemp = property;

        EditorGUILayout.BeginHorizontal();

        DrawLeftToggle(name, ref isProperty);
        if (isPropertyTemp)
            DrawValue(name, ref property);

        EditorGUILayout.EndHorizontal();
    }

    private void DrawLeftToggle(string name, ref bool isProperty)
    {
        EditorGUI.BeginChangeCheck();
        bool isPropertyTemp = EditorGUILayout.ToggleLeft(name, isProperty);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instan, name + " change");
            EditorUtility.SetDirty(instan);
            isProperty = isPropertyTemp;
        }
    }
    private void DrawToggle(string name, ref bool isProperty)
    {
        EditorGUI.BeginChangeCheck();
        bool isPropertyTemp = EditorGUILayout.Toggle(name, isProperty);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instan, name + " change");
            EditorUtility.SetDirty(instan);
            isProperty = isPropertyTemp;
        }
    }

    private void DrawValue(string name, ref int property)
    {
        EditorGUI.BeginChangeCheck();
        int propertyTemp = EditorGUILayout.IntField(property);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instan, name + " change");
            EditorUtility.SetDirty(instan);
            property = propertyTemp;
        }
    }
    private void DrawValue(string name, ref float property)
    {
        EditorGUI.BeginChangeCheck();
        float propertyTemp = EditorGUILayout.FloatField(property);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instan, name + " change");
            EditorUtility.SetDirty(instan);
            property = propertyTemp;
        }
    }

    private void DrawDebug(string name, string property)
    {
        EditorGUILayout.LabelField(name+" = "+property);
    }

    private void DrawVector3(string name, ref Vector3 property)
    {
        EditorGUI.BeginChangeCheck();
        Vector3 propertyTemp = EditorGUILayout.Vector3Field(name, property);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instan, name + " change");
            EditorUtility.SetDirty(instan);
            property = propertyTemp;
        }
    }

    private void DrawGameObject(string name, ref GameObject property)
    {
        EditorGUI.BeginChangeCheck();
        GameObject propertyTemp = (GameObject)EditorGUILayout.ObjectField(name, property, typeof(GameObject));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(instan, name + " change");
            EditorUtility.SetDirty(instan);
            property = propertyTemp;
        }
    }
    //Draw Scene Functions

    private void DrawSceneHandle(string name, ref Vector3 position,ref bool isSelected)
    {
        Vector3 positionTemp = handleTransform.TransformPoint(position);
        float size = HandleUtility.GetHandleSize(positionTemp);
        Handles.color = Color.white;
        if (Handles.Button(positionTemp, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
        {
            isSelected = true;
            Repaint();
        }
        if (isSelected)
        {
            EditorGUI.BeginChangeCheck();
            positionTemp = Handles.DoPositionHandle(positionTemp, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(instan, name+" change");
                EditorUtility.SetDirty(instan);
                position = handleTransform.InverseTransformPoint(positionTemp);
            }
        }
        
    }
    

}
