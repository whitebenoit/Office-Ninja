﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToiletInteractionController : ObjectInteractionController
{
    public int toiletNum;
    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            string saveName = "Save_001";
            GameData gd = SaveManager.LoadFile(saveName);
            //Debug.Log(Time.time + " |Toilet Num Before" + gd.toiletNum.ToString());

            gd.toiletNum = toiletNum;
            gd.sceneName = SceneManager.GetActiveScene().name;

            SaveManager.Save(gd, saveName);


            //SaveManager.Load(saveName);
            //gd = SaveManager.LoadFile(saveName);
            //Debug.Log(Time.time + " |Toilet Num After" + gd.toiletNum.ToString());
        }
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
