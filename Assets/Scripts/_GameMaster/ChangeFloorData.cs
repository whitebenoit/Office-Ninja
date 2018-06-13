using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorData {

    public int floorDoorNum = 0;
    public bool isNinja;
    public SplineCameraController.cameraPosition cameraPosition;

    public ChangeFloorData(int floorDoorNum)
    {
        this.floorDoorNum = floorDoorNum;
    }

    public ChangeFloorData(int floorDoorNum, bool isNinja)
    {
        this.floorDoorNum = floorDoorNum;
        this.isNinja = isNinja;
    }

    public ChangeFloorData(int floorDoorNum, bool isNinja, SplineCameraController.cameraPosition cameraPosition)
    {
        this.floorDoorNum = floorDoorNum;
        this.isNinja = isNinja;
        this.cameraPosition = cameraPosition;
    }
}
