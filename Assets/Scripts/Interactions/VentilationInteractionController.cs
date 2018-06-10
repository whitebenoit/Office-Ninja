using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilationInteractionController : ObjectInteractionController
{
    public SplineLine VentiSpline;
    public float duration = 2;
    public float startProgress = 0;
    private float currProgress = 0;
    private SplinePlayerCharacterController spcc;
    int audioScrewdriver;
    int audioVentilation;

    private void Start()
    {
        AddAudioObject();
        //audioScrewdriver = GameObject.Find("ScriptManager").GetComponent<Dictionaries>().dic_audioID[Dictionaries.AudioName.SCREWDRIVER];
        //audioVentilation = GameObject.Find("ScriptManager").GetComponent<Dictionaries>().dic_audioID[Dictionaries.AudioName.VENTILATION];

        audioScrewdriver = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.SCREWDRIVER];
        audioVentilation = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.VENTILATION];
    }

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {

        UseVentialtion(oicCaller, other);
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
    

    private void UseVentialtion(ObjectInteractionController oicCaller, Collider other)
    {
        spcc = other.GetComponent<SplinePlayerCharacterController>();
        if (pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.BLOCKED])
        {
            pcc.ChangeStatus(PlayerCharacterController.StatusListElement.BLOCKED, true);
            PlayerUnitytoSpineController pUSC = StateMachineBehaviourUtilities.GetBehaviourByName<PlayerUnitytoSpineController>(pcc.pcc_animator, "Screwdriver");

            pUSC.onStateExitCallbacks.Add(() =>
            {
                StartToMoveInVentialtion(oicCaller, other);
                pUSC.onStateExitCallbacks.Clear();
            });
            pcc.pcc_animator.SetBool("isUsingScrew", true);
            SoundManager.GetAudio(audioScrewdriver).Play();
        }
    }

    private void StartToMoveInVentialtion(ObjectInteractionController oicCaller, Collider other)
    {
        pcc.pcc_animator.SetBool("isUsingScrew", false);
        if(pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.DETECTED] == false)
        {
            pcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, true);
            pcc.ninjaModel.SetActive(false);
            currProgress = startProgress;
            pcc.StartCoroutine(MoveInVentialtion());
            SoundManager.GetAudio(audioVentilation).loop = true;
            SoundManager.GetAudio(audioVentilation).Play();
        }
        else
        {
            pcc.ChangeStatus(PlayerCharacterController.StatusListElement.BLOCKED, false);
        }

    }


    private IEnumerator MoveInVentialtion()
    {
        float splineSize = VentiSpline.GetLength(1.0f);

        while (currProgress < 1.0f)
        {
            //currProgress += Time.deltaTime / duration;
            currProgress = VentiSpline.GetLengthAtDistFromParametric(splineSize * Time.deltaTime / duration, currProgress);
            if (currProgress > 1.0f)
                currProgress = 1.0f;
            pcc.transform.SetPositionAndRotation(
                VentiSpline.GetPoint(currProgress),
                Quaternion.Euler(VentiSpline.GetDirection(currProgress)));
            yield return null;
        }

        // CallBack
        EndMoveInVentilation();
    }

    private void EndMoveInVentilation()
    {
        spcc.progress = spcc.lSpline.GetNearestProgressOnSpline(spcc.transform.position);
        spcc.transform.SetPositionAndRotation(
            spcc.lSpline.GetPoint(spcc.progress),
            Quaternion.Euler( spcc.lSpline.GetDirection(spcc.progress)));


        SoundManager.GetAudio(audioVentilation).loop = false;
        SoundManager.GetAudio(audioVentilation).Stop();

        pcc.ninjaModel.SetActive(true);
        GameObject.Instantiate(Resources.Load("Prefabs/TPCloud"), pcc.transform);
        pcc.ChangeStatus(PlayerCharacterController.StatusListElement.BLOCKED, false);
        pcc.ChangeStatus(PlayerCharacterController.StatusListElement.HIDDEN, false);
    }
}
