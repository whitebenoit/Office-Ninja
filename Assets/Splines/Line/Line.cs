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

    public Vector3 GetPoint(float t)
    {
        return GetPoint(originPt, endPt, t);
    }


}
