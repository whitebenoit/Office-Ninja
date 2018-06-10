using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSpriteController : MonoBehaviour {

    public SightPosName currSight = SightPosName.BLUE_DOWN;
    SpriteRenderer sprRend;

    public enum SightPosName { BLUE_DOWN, RED_DOWN, RED_STRAIGHT, NONE };

    public struct SightPosition
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Color color;

        public SightPosition(Vector3 position, Vector3 rotation, Vector3 scale, Color color)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.color = color;
        }

    }


    public Dictionary<SightPosName, SightPosition> dic_SightPosition = new Dictionary<SightPosName, SightPosition>();
    

    private void Start()
    {
        sprRend = this.GetComponentInChildren<SpriteRenderer>();

        dic_SightPosition.Add(SightPosName.RED_STRAIGHT,
            new SightPosition(
                new Vector3(0.0f, 1.4f, 0.17f),
                new Vector3(0.0f, 90.0f, 0.0f),
                new Vector3(0.2f, 0.19f, 1.0f),
               Dictionaries.instance.dic_Colors[Dictionaries.ColorNames.SIGHT_RED]));
        
        dic_SightPosition.Add(SightPosName.BLUE_DOWN,
            new SightPosition(
                new Vector3(0.0f, 1.23f, 0.286f),
                new Vector3(0.0f, 90.0f, 76.22f),
                new Vector3(0.2f, 0.13f, 1.0f),
               Dictionaries.instance.dic_Colors[Dictionaries.ColorNames.SIGHT_BLUE]));


        dic_SightPosition.Add(SightPosName.RED_DOWN,
            new SightPosition(
                new Vector3(0.0f, 1.23f, 0.286f),
                new Vector3(0.0f, 90.0f, 76.22f),
                new Vector3(0.2f, 0.13f, 1.0f),
               Dictionaries.instance.dic_Colors[Dictionaries.ColorNames.SIGHT_RED]));


        dic_SightPosition.Add(SightPosName.NONE,
            new SightPosition(
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 90.0f, 0.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
               Dictionaries.instance.dic_Colors[Dictionaries.ColorNames.SIGHT_RED]));


        SetSightState(currSight);
    }


    public void SetSightState(SightPosName newSight)
    {
        //Debug.Log(newSight.ToString());
        SightPosition sPos = dic_SightPosition[newSight];
        this.transform.localPosition = sPos.position;
        this.transform.localRotation = Quaternion.Euler(sPos.rotation);
        this.transform.localScale = sPos.scale;
        sprRend.color = sPos.color;
    }


}
