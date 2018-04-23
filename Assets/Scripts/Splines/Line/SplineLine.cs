using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineLine : MonoBehaviour {

    [Range(0f,1f)]
    public float t;

    public float cDistance;

    [SerializeField]
    private Vector3[] points;

    [SerializeField]
    private bool loop = false;

    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
            if (value == true)
            {
                SetControlPoint(0, points[0]);
            }
        }
    }

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
        if (loop)
        {
            if (index == 0)
            {
                points[points.Length - 1] = point;
            }
        }
        points[index] = point;
    }

    public int GetNextControlPointIndex(int index)
    {
        int indexMax = points.Length - 1;
        if (index < indexMax) { return index + 1; }
        else
        {
            if (Loop) { return 0;}
            else { return indexMax; }
        }
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

    public float GetLength(float t)
    {
        float lgth = 0f;
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = GetLineCount - 1;
        }
        else
        {
            t = Mathf.Clamp01(t) * GetLineCount;
            i = (int)t;
            t -= i;
        }

        for (int j = 0; j < i; j++)
        {
            lgth += Vector3.Distance(points[j], points[j + 1]);
        }
        lgth += Line.GetLength(points[i], points[i + 1], t);
        return lgth;
    }

    public float GetParametricLength(float dist)
    {
        float fullLength = GetLength(1f);
        while (dist < 0f)
        {
            if (Loop) { dist += fullLength; }
            else { return 0f; }
        }
        while (dist > fullLength)
        {
            if (Loop) { dist -= fullLength; }
            else { return 1f; }
        }

        float currDist = 0f;
        int currIndex = 0;

        for (int i = 0; i < GetLineCount; i++)
        {
            float lineDist = Vector3.Distance(points[i], points[i + 1]);
            if(currDist + lineDist >= dist)
            {
                currIndex = i;
                break;
            }
            else
            {
                currDist += lineDist;
            }
        }
        return ( currIndex + Line.GetParametricLength(points[currIndex], points[currIndex + 1], dist - currDist)) / GetLineCount;  
    }

    public float GetLengthAtDistFromParametric(float movedDist, float originT)
    {
        float newT = this.GetParametricLength(movedDist + this.GetLength(originT));
        if (newT >= 1)
        {
            if (Loop) newT -= 1f;
            else newT = 1f;
        }
        return newT;
    }

    public Vector3 GetPointAtDistFromParametric( float movedDist, float originT)
    {
        return GetPoint(GetLengthAtDistFromParametric(movedDist, originT));
    }


    public Vector3 GetPreviousControlPoint (float t)
    {
        if (t == 1.0f)
        {
            return GetControlPoint(GetLineCount-1);
        }
        float tempT = t;
        if (Loop)
        {
            while (tempT > 1f) tempT -= 1f;
            while (tempT < 0f) tempT += 1f;
        }
        tempT = Mathf.Clamp01(tempT) * GetLineCount;
        int i = (int)tempT;
        return GetControlPoint(i);
    }

    public Vector3 GetNextControlPoint(float t)
    {
        if (t == 1.0f)
        {
            return GetControlPoint(GetLineCount);
        }
        float tempT = t;
        if (Loop)
        {
            while (tempT > 1f) tempT -= 1f;
            while (tempT < 0f) tempT += 1f;
        }
        tempT = Mathf.Clamp01(tempT) * GetLineCount;
        int i = (int)tempT;
        return GetControlPoint(i+1);
    }

    public Vector3 GetDirection(float t)
    {
        return GetNextControlPoint(t) - GetPreviousControlPoint(t);
    }


    public Vector3 GetNearestPointOnSpline(Vector3 outsidePt)
    {
        Vector3 resultPoint = new Vector3();
        float currDist = -1;
        for (int i = 1; i < points.Length; i++)
        {
            Vector3 startPointLine = this.transform.TransformPoint(points[i - 1]);
            Vector3 endPointLine = this.transform.TransformPoint(points[i]);
            Vector3 tempPoint = Line.GetNearestPointOnLine(startPointLine, endPointLine, outsidePt);
            float tempDist = Vector3.Distance(tempPoint, outsidePt);
            if(tempDist < currDist || currDist <= 0)
            {
                currDist = tempDist;
                resultPoint = tempPoint;
            }
        }
        return resultPoint;
    }

    public float GetNearestProgressOnSpline(Vector3 outsidePt)
    {
        float tempT = -1 ;
        float currDist = -1;
        for (int i = 1; i < points.Length; i++)
        {
            Vector3 startPointLine = this.transform.TransformPoint(points[i - 1]);
            Vector3 endPointLine = this.transform.TransformPoint(points[i]);
            Vector3 tempPoint = Line.GetNearestPointOnLine(startPointLine, endPointLine, outsidePt);
            float tempDist = Vector3.Distance(tempPoint, outsidePt);
            if (tempDist < currDist || currDist <= 0)
            {
                currDist = tempDist;
                tempT = i - 1 + Line.GetNearestProgressOnLine(startPointLine, endPointLine, outsidePt); ;
            }
        }
        return tempT/this.GetLineCount;
    }





    //public void AddCurve()
    //{
    //    int ptLength = points.Length;
    //    Array.Resize(ref points, ptLength + 1);
    //    ptLength++;
    //    if (Loop)
    //    {
    //        points[ptLength - 1] = points[0];
    //        if (ptLength >= 3)
    //        {
    //            points[ptLength - 2] = (points[0] + points[ptLength - 3]) / 2;
    //        }
    //        else
    //        {
    //            points[ptLength - 2] = points[0] + Vector3.right;
    //        }

    //    }
    //    else
    //    {
    //        Vector3 point = points[ptLength - 1];
    //        point.x += 1f;
    //        points[ptLength - 1] = point;
    //    }
    //}

    public void AddCurve(int index)
    {
        int ptLength = points.Length;
        if (index == -1 || ptLength <= 1) AddCurve(ptLength -1);
        else
        {
            if (ptLength > 1)
            {
                Array.Resize(ref points, ptLength + 1);
                ptLength++;
                for (int i = ptLength - 1; i > index; i--)
                {
                    points[i] = points[i - 1];
                }
                points[index] = (points[index + 1] + points[index - 1]) / 2;
            }
        }
    }

    public void RemovePoint(int index)
    {
        if (index == -1)
        {
            if (Loop) { index = points.Length - 2; }
            else { index = points.Length - 1; }
        }
           
        Vector3[] dest = new Vector3[points.Length - 1];
        if (index > 0)
            Array.Copy(points, 0, dest, 0, index);
        if (index < points.Length - 1)
            Array.Copy(points, index + 1, dest, index, points.Length - index - 1);
        points = dest;
    }
    
    public void Reset()
    {
        points = new Vector3[] {
            new Vector3(1f, 0f, 0f),
            new Vector3(2f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
            new Vector3(4f, 0f, 0f)
        };

    }




}
