using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpriteController : MonoBehaviour {

    public float rotSpeed = 1;
    public Vector3 rotation = new Vector3(1, 1, 1);

    private void Update()
    {
        transform.Rotate(rotSpeed*Time.deltaTime * rotation);
    }

}
