using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class InstanModifier : MonoBehaviour {

    public Instantiator instan;

    private void Awake()
    {
        instan = transform.GetComponent<Instantiator>();
    }

    public abstract void Modify(GameObject gO);
}
