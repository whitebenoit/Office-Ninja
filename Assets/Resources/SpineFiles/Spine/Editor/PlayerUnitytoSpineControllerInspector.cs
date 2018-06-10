using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlayerUnitytoSpineController))]
public class PlayerUnitytoSpineControllerInspector : Editor
{
    private PlayerUnitytoSpineController pUSC;

    

    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    pUSC = target as PlayerUnitytoSpineController;
    //    EditorGUILayout.LabelField("Bool To modify");
    //    DrawChangeBool("Bool change on Entry",ref  pUSC.onEnterChangeBool);
    //    DrawChangeBool("Bool change on Exit", ref pUSC.onExitChangeBool);




    //}




    private void DrawChangeBool(string name, ref List<ChangeBool> changeBool)
    {
        DrawChangeBoolTitle(name,ref changeBool);
        DrawChangeBoolValues(ref changeBool);
    }


    private void DrawChangeBoolTitle(string name,ref List<ChangeBool> changeBool)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name + " [" + changeBool.Count + "]");
        if (GUILayout.Button("+"))
        {
            changeBool.Add(new ChangeBool());
        }
        if (GUILayout.Button("-") && changeBool.Count > 0)
        {
            changeBool.RemoveAt(changeBool.Count - 1);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawChangeBoolValues(ref List<ChangeBool> changeBool)
    {
        if (changeBool.Count > 0)
        {
            for (int i = 0; i < changeBool.Count; i++)
            {
                DrawChangeBoolValue(ref changeBool, i);
            }
        }
    }

    private void DrawChangeBoolValue(ref List<ChangeBool> changeBool, int i)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("[" + i + "]", GUILayout.MaxWidth(20.0f));

        DrawStringField("Bool Name", ref changeBool, i);
        
        DrawToggle("", ref changeBool, i);

        if (GUILayout.Button("Remove"))
        {
            changeBool.RemoveAt(i);
        }

        EditorGUILayout.EndHorizontal();
    }


    private void DrawStringField(string name, ref List<ChangeBool> changeBool, int i)
    {
        EditorGUI.BeginChangeCheck();
        string isPropertyTemp = EditorGUILayout.TextField(name, changeBool[i].name);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(pUSC, name + " change");
            EditorUtility.SetDirty(pUSC);
            changeBool[i].setName(isPropertyTemp);
            //Debug.Log("ChangeBool_" + i + " - " + changeBool[i].name);
        }
    }


    //private void DrawStringField(string name, ref string isProperty)
    //{
    //    EditorGUI.BeginChangeCheck();
    //    string isPropertyTemp = EditorGUILayout.TextField(name, isProperty);
    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        Undo.RecordObject(pUSC, name + " change");
    //        EditorUtility.SetDirty(pUSC);
    //        isProperty = isPropertyTemp;
    //    }
    //}

    private void DrawToggle(string v, ref List<ChangeBool> changeBool, int i)
    {
        EditorGUI.BeginChangeCheck();
        bool isPropertyTemp = EditorGUILayout.Toggle(name, changeBool[i].newValue, GUILayout.MaxWidth(20.0f));
        if (EditorGUI.EndChangeCheck())
        {
            //Debug.Log("ChangeBool [" + i + "] - " + isPropertyTemp);
            Undo.RecordObject(pUSC, name + " change");
            EditorUtility.SetDirty(pUSC);
            changeBool[i].setNewValue(isPropertyTemp);
        }
    }

    private void DrawToggle(string name, ref bool isProperty)
    {
        EditorGUI.BeginChangeCheck();
        bool isPropertyTemp = EditorGUILayout.Toggle(name, isProperty, GUILayout.MaxWidth(20.0f));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(pUSC, name + " change");
            EditorUtility.SetDirty(pUSC);
            isProperty = isPropertyTemp;
        }
    }



}
