using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionController : ObjectInteractionController
{
    public Dictionaries.ItemName itemName;
    int audioNewObject;

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        GameMasterManager gMM = GameMasterManager.instance;
        switch (itemName)
        {
            case Dictionaries.ItemName.SCREWDRIVER:
                if (!gMM.gd_currentLevel.isScrewUnlocked)
                    AddItem(itemName, other);
                RemoveInteraction(other);
                Destroy(gameObject);
                break;
            case Dictionaries.ItemName.LAXATIVE:
                if (!gMM.gd_currentLevel.isLaxaUnlocked)
                    AddItem(itemName, other);
                RemoveInteraction(other);
                Destroy(gameObject);
                break;
            default:
                break;
        }
        if (CheckInteractionValidity(other))
        {
            pcc.UpdateObjectStatus();
        }
        
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        
        SpawnButton();
    }

    private void Start()
    {
        GameData gd = SaveManager.LoadFile(GameMasterManager.instance.saveName);
        bool isUnlocked = false;
        if (itemName == Dictionaries.ItemName.SCREWDRIVER)
            isUnlocked = gd.isScrewUnlocked;
        else if (itemName == Dictionaries.ItemName.LAXATIVE)
            isUnlocked = gd.isLaxaUnlocked;

        if (isUnlocked)
        {
            Destroy(gameObject);
        }
        AddAudioObject();
        audioNewObject =Dictionaries.instance.dic_audioID[Dictionaries.AudioName.NEW_OBJECT];
    }

    private void AddItem(Dictionaries.ItemName itemName, Collider other)
    {
        GameMasterManager gMM = GameMasterManager.instance;

        GameData gd = gMM.gd_currentLevel;
        switch (itemName)
        {
            case Dictionaries.ItemName.SCREWDRIVER:
                gd.isScrewUnlocked = true;
                break;
            case Dictionaries.ItemName.LAXATIVE:
                gd.isLaxaUnlocked = true;
                break;
            default:
                break;
        }
        PauseController.instance.UpdateItemDisplay();
        SoundManager.GetAudio(audioNewObject).Play();
        SaveManager.Save(gd, gMM.saveName);
        gMM.gd_currentLevel = gd;

        gMM.SetUpSPCC(other.GetComponent<SplinePlayerCharacterController>(),ref gd);
    }
}
