using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public float letterPause = 0.2f;
    public float maxTime = 3f;

    private string message;
    public Text textComp;
    [HideInInspector]
    public bool isFinished = true;

    private Coroutine coroutine;

    // Use this for initialization
    void Start()
    {
        TypeText();
    }

    public void TypeText()
    {
        //textComp = GetComponent<Text>();
        StopAllCoroutines();
        isFinished = false;
        message = textComp.text;
        textComp.text = "";
        float interval = Mathf.Min(maxTime, letterPause * message.Length) / message.Length;
        coroutine = StartCoroutine(TypeText(interval));
    }

    IEnumerator TypeText(float interval)
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            yield return 0;
            yield return new WaitForSeconds(interval);
        }
        isFinished = true;
    }

    public void ForceText()
    {
        StopCoroutine(coroutine);
        textComp.text = message;
        isFinished = true;
    }
}
