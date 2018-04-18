using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {

    private bool isActive;
    public float timerValue;
    public GameObject gOToInst;
    private List<GameObject> gOToInstList = new List<GameObject>();

    public bool startOnAwake = true;
    public bool stopAtMax = true;

    public bool isDelay;
    public float delay;
    public bool isDelayPassed = false;

    public bool isCooldown;
    public float cooldown;
    private float hardMinCooldown = 0.2f;

    public bool isMaxInst;
    public bool isStopOnMax;
    public int maxInst;
    private int hardMinMaxInst = 1;

    public Vector3 instantiatePosition;


    private void ActiveRoutine()
    {
        timerValue += Time.fixedDeltaTime;
        if( isDelayOk() 
            && isCdOk()
            && isMaxInstOk())
        {
            this.Instantiate();
            timerValue = 0.0f;
        }
    }

    private bool isDelayOk()
    {
        if (isDelay)
        {
            if (!isDelayPassed )
            {
                if (timerValue >= delay)
                {
                    isDelayPassed = true;
                    Instantiate();
                    timerValue = 0.0f;
                    return false;
                }
                else return false;
            }
            return true;
        }
        else return true;
    }

    private bool isCdOk()
    {
        float cd;
        if (isCooldown) { cd = cooldown; }
        else { cd = 1.0f; }

        if (timerValue >= Mathf.Max(cd, hardMinCooldown))
        {
            //timerValue = 0.0f;
            return true;
        }
        else return false;
    }

    private bool isMaxInstOk()
    {
        int mxObj;
        if (isMaxInst) { mxObj = Mathf.Max(maxInst, hardMinMaxInst); }
        else { mxObj = 50; }

        if (gOToInstList.Count >= mxObj)
        {
            if(isStopOnMax) { return false; }
            else
            {
                int objNumToRemove = gOToInstList.Count - mxObj;
                for (int i = 0; i < objNumToRemove; i++)
                {
                    Destroy(gOToInstList[i]);
                }
                gOToInstList.RemoveRange(0, objNumToRemove);
                return true;
            }
        }
        else return true;


    }

    private void Instantiate()
    {
        GameObject tempGO = Instantiate(gOToInst, transform.TransformPoint(instantiatePosition), new Quaternion(0, 0, 0, 0));
        gOToInstList.Add(tempGO);
    } 


	// Use this for initialization
	void Start () {
        if (startOnAwake) { Activate(); }
        else { Deactivate(); }
	}

    public void Activate()
    {
        isActive = true;
        isDelayPassed = false;
        timerValue = 0.0f;
    }

    public void Deactivate()
    {
        isActive = false;
    } 
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            this.ActiveRoutine();
        }
	}
    



}
