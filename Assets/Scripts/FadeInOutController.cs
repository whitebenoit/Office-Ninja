using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using System;
using UnityEngine.UI;

public class FadeInOutController : MonoBehaviour {

    private static FadeInOutController _instance = null;
    private static bool initialized = false;
    private static Text textElmt;

    public delegate void callBackDel(); 
    public Image panelImage;
    public float standardTime = 0.2f;

    public static FadeInOutController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (FadeInOutController)FindObjectOfType(typeof(FadeInOutController));
                if (_instance == null)
                {
                    // Create gameObject and add component
                    _instance = (new GameObject("FadeInOutControllerObj")).AddComponent<FadeInOutController>();
                }
            }
            return _instance;
        }
    }


    /// <summary>
    /// The gameobject that the FadeInOutController is attached to
    /// </summary>
    public static GameObject gameobject { get { return instance.gameObject; } }

    private void Awake()
    {
        instance.Init();
        panelImage = GameObject.FindGameObjectWithTag(Tags.fadePanel).GetComponent<Image>();
        textElmt = panelImage.gameObject.GetComponentInChildren<Text>();
    }

    void Init()
    {
        if (!initialized)
        {
            initialized = true;
            GameMasterManager.DontDestroyOnLoad(this);
        }
    }
    
    public void FadeIn()
    {
        FadeIn(null, standardTime,"");
    }

    public void FadeIn(String Text)
    {
        FadeIn(null, standardTime, Text);
    }

    public void FadeIn(float duration)
    {
        FadeIn(null, duration, "");
    }

    public void FadeIn(float duration, String Text)
    {
        FadeIn(null, duration, Text);
    }

    public void FadeIn(callBackDel callbackFunc)
    {
        FadeIn(callbackFunc, standardTime, "");
    }

    public void FadeIn(callBackDel callbackFunc, String Text)
    {
        FadeIn(callbackFunc, standardTime, Text);
    }

    public void FadeIn(float duration, callBackDel callbackFunc)
    {
        FadeIn(callbackFunc, duration, "");
    }

    public void FadeIn(callBackDel callbackFunc,float duration, String Text)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(1.0f, duration, callbackFunc,Text));
    }


    public void FadeOut()
    {
        FadeOut(null, standardTime, "");
    }

    public void FadeOut(String Text)
    {
        FadeOut(null, standardTime, Text);
    }

    public void FadeOut(float duration)
    {
        FadeOut(null, duration, "");
    }

    public void FadeOut(float duration, String Text)
    {
        FadeOut(null, duration, Text);
    }

    public void FadeOut(callBackDel callbackFunc)
    {
        FadeOut(callbackFunc, standardTime, "");
    }

    public void FadeOut(callBackDel callbackFunc, String Text)
    {
        FadeOut(callbackFunc, standardTime, Text);
    }

    public void FadeOut(float duration, callBackDel callbackFunc)
    {
        FadeOut(callbackFunc, duration, "");
    }

    public void FadeOut(callBackDel callbackFunc, float duration, String Text)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(0.0f, duration, callbackFunc, Text));
    }

    public IEnumerator FadeTo(float targetAlpha, float duration, callBackDel callbackFunc, String Text)
    {
        if (panelImage == null)
        {
            panelImage = GameObject.FindGameObjectWithTag(Tags.fadePanel).GetComponent<Image>();
            textElmt = panelImage.gameObject.GetComponentInChildren<Text>();
        }
        float currentAlpha = panelImage.color.a;
        textElmt.text = Text;

        while (Math.Abs(currentAlpha - targetAlpha) >= 0.01f)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.fixedDeltaTime/duration);
            var tempColor = panelImage.color;
            var tempTxtColor = textElmt.color;
            tempColor.a = currentAlpha;
            tempTxtColor.a = currentAlpha;
            panelImage.color = tempColor;
            textElmt.color = tempTxtColor;
            yield return null;
        }

        if (callbackFunc != null)
        {
            callbackFunc();
        }
    }
}
