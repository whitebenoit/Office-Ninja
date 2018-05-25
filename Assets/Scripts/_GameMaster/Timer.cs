using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public static IEnumerator NewTimer(float duration, Action callback)
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + duration)
        {
            yield return null;
        }

        if (callback != null) callback();
    }

}
