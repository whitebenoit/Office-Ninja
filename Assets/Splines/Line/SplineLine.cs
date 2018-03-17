using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineLine : MonoBehaviour {

    [Range(0f,1f)]
    public float t;

    [SerializeField]
    private Vector3[] points;

    public int GetControlPointCount
    {
        get
        {
            return points.Length;
        }
    }
    public Vector3 GetControlPoint(int index)
    {
        return points[index];
    }
    public void SetControlPoint(int index, Vector3 point)
    {
        points[index] = point;
    }

    

    public int GetLineCount
    {
        get
        {
            return points.Length - 1;
        }
    }

    public Vector3 GetPoint(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = GetLineCount-1;
        }
        else
        {
            t = Mathf.Clamp01(t) * GetLineCount;
            i = (int)t;
            t -= i;
        }
        return transform.TransformPoint(Line.GetPoint(points[i], points[i + 1], t));

    }


    public void AddCurve()
    {
        Vector3 point = points[points.Length - 1];
        Array.Resize(ref points, points.Length + 1);
        point.x += 1f;
        points[points.Length - 1] = point;
    }

    public void RemovePoint(int index)
    {
        Vector3[] dest = new Vector3[points.Length - 1];
        if (index > 0)
            Array.Copy(points, 0, dest, 0, index);
        if (index < points.Length - 1)
            Array.Copy(points, index + 1, dest, index, points.Length - index - 1);
        points = dest;
    }

    //public static T[] RemoveAt<T>(this T[] source, int index)
    //{
    //    T[] dest = new T[source.Length - 1];
        //if (index > 0)
        //    Array.Copy(source, 0, dest, 0, index);

        //if (index<source.Length - 1)
        //    Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

    //    return dest;
    //}
}
