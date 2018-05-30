using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionController : ObjectInteractionController
{
    public Dictionaries.ItemName itemName;

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        GameMasterManager gMM = GameMasterManager.instance;
        switch (itemName)
        {
            case Dictionaries.ItemName.SCREWDRIVER:
                if (!gMM.gd_currentLevel.isScrewUnlocked)
                    AddItem(itemName);
                break;
            case Dictionaries.ItemName.LAXATIVE:
                if (!gMM.gd_currentLevel.isLaxaUnlocked)
                    AddItem(itemName);
                break;
            default:
                break;
        }
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }

    private void AddItem(Dictionaries.ItemName itemName)
    {
        GameMasterManager gMM = GameMasterManager.instance;
        switch (itemName)
        {
            case Dictionaries.ItemName.SCREWDRIVER:
                gMM.gd_currentLevel.isScrewUnlocked = true;
                break;
            case Dictionaries.ItemName.LAXATIVE:
                gMM.gd_currentLevel.isLaxaUnlocked = true;
                break;
            default:
                break;
        }
        PauseController.instance.UpdateItemDisplay();
        SaveManager.Save(gMM.gd_currentLevel, gMM.saveName);
    }
}
