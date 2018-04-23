using UnityEngine;

public class BezierCurve : MonoBehaviour {

	public Vector3[] points;

    //public static Vector3 BezierGetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    //{
    //    t = Mathf.Clamp01(t);
    //    float oneMinusT = 1f - t;
    //    return
    //        oneMinusT * oneMinusT * p0 +
    //        2f * oneMinusT * t * p1 +
    //        t * t * p2;
    //}

    //public static Vector3 BezierGetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    //{
    //    return
    //        2f * (1f - t) * (p1 - p0) +
    //        2f * t * (p2 - p1);
    //}

    public static Vector3 BezierGetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float OneMinusT = 1f - t;
        return
            OneMinusT * OneMinusT * OneMinusT * p0 +
            3f * OneMinusT * OneMinusT * t * p1 +
            3f * OneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    public static Vector3 BezierGetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }

    public static float BezierGetApproxLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int segmentNum)
    {
        float result = 0f;
        Vector3 previousPoint = p0;
        Vector3 currentPoint;
        for (int i = 1; i <= segmentNum; i++)
        {
            currentPoint = BezierGetPoint(p0, p1, p2, p3, (float)i / (float)segmentNum);
            result += Vector3.Distance(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
        return result;
    }

    public Vector3 GetPoint (float t) {
		return transform.TransformPoint(BezierGetPoint(points[0], points[1], points[2], points[3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		return transform.TransformPoint(BezierGetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}
    
    public float GetApproxIntegral (float t, int segmentNum)
    {
        return BezierGetApproxLength(points[0], points[1], points[2], points[3],segmentNum);
    }

    public float GetApproxLength(int segmentNum)
    {
        return BezierGetApproxLength(points[0], points[1], points[2], points[3], segmentNum);
    }



    public void Reset () {
		points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
    }
}