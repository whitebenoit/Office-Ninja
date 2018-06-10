using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToiletInteractionController : ObjectInteractionController
{
    public int toiletNum;
    private float fadeInDuration = 0.5f;
    private string text = "sauvegarde";
    int audioToilet;

    private void Start()
    {
        AddAudioObject();
        audioToilet =Dictionaries.instance.dic_audioID[Dictionaries.AudioName.TOILET];
    }


    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        if (other.tag == Tags.player)
        {
            PlayerCharacterController pcc = other.GetComponent<PlayerCharacterController>();
            if(pcc != null)
            {
                pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = true;
                if (pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA])
                {
                    FadeInOutController.instance.FadeIn(() =>
                    {
                        TransformToSalaryman(pcc);
                        SoundManager.GetAudio(audioToilet).Play();
                        FadeInOutController.instance.FadeOut(() =>
                        {
                            pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = false;
                            this.SaveGame();
                        });
                    }, fadeInDuration, text);
                    
                }
                else
                {
                    FadeInOutController.instance.FadeIn(() =>
                    {
                        TransformToNinja(pcc);
                        SoundManager.GetAudio(audioToilet).Play();
                        FadeInOutController.instance.FadeOut(() =>
                        {
                            pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.ROOTED] = false;
                            this.SaveGame();
                        });
                    },fadeInDuration, text);
                }
            }
        }
    }

    private void SaveGame()
    {
        string saveName = "Save_001";
        GameData gd = SaveManager.LoadFile(saveName);

        gd.isNinja = pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA];
        gd.toiletNum = toiletNum;
        gd.sceneName = SceneManager.GetActiveScene().name;

        SaveManager.Save(gd, saveName);
    }

    private void TransformToNinja(PlayerCharacterController pcc)
    {
        pcc.ChangeStatus(PlayerCharacterController.StatusListElement.NINJA, true);
    }

    private void TransformToSalaryman(PlayerCharacterController pcc)
    {
        pcc.ChangeStatus(PlayerCharacterController.StatusListElement.NINJA, false);
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
}
