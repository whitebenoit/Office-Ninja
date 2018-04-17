using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {

    private bool isActive;
    private float timerValue;
    private float hardMinCooldown = 0.2f;
    public GameObject gOToInst;
    private List<GameObject> gOToInstList = new List<GameObject>();

    public bool startOnAwake = true;

    public bool isDelay;
    public float delay;

    public bool isCooldown;
    public float cooldown;

    public bool isMaxInst;
    public int maxInst;

    public Vector3 instantiatePosition;



	// Use this for initialization
	void Start () {
        isActive = startOnAwake;
        if (isActive)
            timerValue = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timerValue += Time.fixedDeltaTime;

        float cd;
        if(isCooldown) { cd = cooldown; }
        else { cd = 1.0f; }

        int mxObj;
        if (isMaxInst) {mxObj = Mathf.Max(maxInst,1);}
        else { mxObj = 50; }

        if (timerValue >= Mathf.Max(cd, hardMinCooldown))
        {
            if (gOToInstList.Count >= mxObj)
            {
                Destroy(gOToInstList[0]);
                gOToInstList.RemoveAt(0);
            }
            GameObject tempGO = Instantiate(gOToInst, transform.TransformPoint(instantiatePosition), new Quaternion(0, 0, 0, 0));
            gOToInstList.Add(tempGO);
            //gOToInstList.Add(Instantiate(gOToInst, transform.TransformPoint(instantiatePosition), new Quaternion(0,0,0,0)));
            timerValue = 0.0f;
        }
	}
}
