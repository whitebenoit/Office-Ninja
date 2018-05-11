using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionaries : MonoBehaviour
{
    [SerializeField]
    public Dictionary<DirectionElement, Vector3> dic_directions = new Dictionary<DirectionElement, Vector3>();

    [SerializeField]
    public Dictionary<PlayerCharacterController.ActionListElement, Sprite> dic_BtnUIImageRess = new Dictionary<PlayerCharacterController.ActionListElement, Sprite>();

    public enum DirectionElement { FORWARD, RIGHT, LEFT, BACK }
    //{
    //    {PlayerCharacterController.ActionListElement.INTERACT, (Sprite)Resources.Load("Sprites/btn_A")},
    //    {PlayerCharacterController.ActionListElement.HIDE, (Sprite)Resources.Load("Sprites/btn_B")},
    //    {PlayerCharacterController.ActionListElement.USE, (Sprite)Resources.Load("Sprites/btn_X")},
    //    {PlayerCharacterController.ActionListElement.DASH, (Sprite)Resources.Load("Sprites/btn_Y")}
    //};


    private void Start()
    {
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.INTERACT, Resources.Load<Sprite>("Sprites/btn_A"));
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.HIDE, Resources.Load<Sprite>("Sprites/btn_B"));
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.USE, Resources.Load<Sprite>("Sprites/btn_X"));
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.DASH, Resources.Load<Sprite>("Sprites/btn_Y"));

        dic_directions.Add(DirectionElement.FORWARD, Vector3.forward);
        dic_directions.Add(DirectionElement.RIGHT, Vector3.right);
        dic_directions.Add(DirectionElement.LEFT, Vector3.left);
        dic_directions.Add(DirectionElement.BACK, Vector3.back);
    }
}
