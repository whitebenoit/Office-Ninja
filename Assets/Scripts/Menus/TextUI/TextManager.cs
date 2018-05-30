using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public float letterPause = 0.2f;
    public float maxTime = 3f;

    //private string message;
    private string[] msgList;

    public Text textComp;
    
    [HideInInspector]
    public bool isPaused = false;

    [HideInInspector]
    public bool isFinished = true;
    [HideInInspector]
    public int currentIndex = -1;


    private Coroutine coroutine;

    // Use this for initialization
    private void Start()
    {
        textComp.text = "";
        gameObject.SetActive(false);
    }

    public void TypeText(string[] listMessage)
    {
        //textComp = GetComponent<Text>();
        ResetTextManager();
        msgList = listMessage;
        if (listMessage.Length > 0)
        {
            currentIndex = 0;
            TypeText(currentIndex, msgList);
        }
    }

    public void NextTextPart()
    {
        if(currentIndex+1 < msgList.Length)
        {
            currentIndex++;
            TypeText(currentIndex, msgList);
        }
    }

    private void TypeText(int curIndex,string[] listMessage)
    {
        isFinished = false;
        isPaused = false;
        textComp.text = "";
        float interval = Mathf.Min(maxTime, letterPause * listMessage[currentIndex].Length) / listMessage[currentIndex].Length;
        coroutine = StartCoroutine(TypeText(listMessage, curIndex, interval));
    }

    private void ResetTextManager()
    {
        StopAllCoroutines();
        textComp.text = "";
        msgList = null;
        isFinished = true;
        isPaused = false;
        currentIndex = -1;
    }

    IEnumerator TypeText(string[] listMessage,int index, float interval)
    {
        int listLgth = listMessage.Length;
        if (listLgth > 0 && index < listLgth)
        {
            foreach (char letter in listMessage[index].ToCharArray())
            {
                textComp.text += letter;
                yield return 0;
                yield return new WaitForSeconds(interval);
            }
            if (index+ 1 < listLgth) isPaused = true;
            else isFinished = true;
        }
    }

    //IEnumerator TypeText(float interval)
    //{
    //    foreach (char letter in message.ToCharArray())
    //    {
    //        textComp.text += letter;
    //        yield return 0;
    //        yield return new WaitForSeconds(interval);
    //    }
    //    isFinished = true;
    //}

    public void ForceText()
    {
        StopCoroutine(coroutine);
        textComp.text = msgList[currentIndex];

        if (currentIndex + 1 < msgList.Length)
        {
            isPaused = true;
            isFinished = false;
        }
        else
        {
            isPaused = false;
            isFinished = true;
        }
    }
}
