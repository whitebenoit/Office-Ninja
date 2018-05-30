using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonUIImageController : MonoBehaviour {

    private PlayerCharacterController.ActionListElement actionType;
    private Image currentImage;
    private Sprite dicSprite;
    

	// Use this for initialization
	void Start () {
        currentImage = this.transform.parent.GetComponentInChildren<Image>();
        SetImage();
    }
	
	// Update is called once per frame
	//void Update () {
    //    //SetImage();
    //}


    void SetImage()
    {
        //actionType = this.transform.parent.GetComponentInChildren<ObjectInteractionController>().actionType;
        dicSprite = GameObject.Find("ScriptManager").GetComponent<Dictionaries>().dic_BtnUIImageRess[actionType];
        
        currentImage.sprite = Instantiate<Sprite>(dicSprite);
    }

    public void SetAction(PlayerCharacterController.ActionListElement actionType)
    {
        this.actionType = actionType;
    }
}
