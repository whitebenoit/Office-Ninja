using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line  {

    Vector3 originPt;
    Vector3 endPt;

    public static Vector3 GetPoint(Vector3 originPt, Vector3 endPt, float t)
    {
        return ((1 - t) * originPt + t*endPt);
    }

    public static float GetLength(Vector3 originPt, Vector3 endPt, float t)
    {
        return Vector3.Distance(originPt, GetPoint(originPt, endPt, t));
    }

    //public  static float GetDistance(Vector3 originPt, Vector3 endPt)
    //{
    //    return Vector3.Distance(originPt, endPt);
    //}

    public static float GetParametricLength(Vector3 originPt, Vector3 endPt, float dist)
    {
        return dist/ Vector3.Distance(originPt, endPt);
    }


    public Vector3 GetPoint(float t)
    {
        return GetPoint(originPt, endPt, t);
    }


}
