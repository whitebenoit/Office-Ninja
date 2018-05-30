using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeUnitytoSpineController : StateMachineBehaviour
{
    public string motionName;
    public float speedAnim = 1;

    public SightSpriteController.SightPosName sightNinja = SightSpriteController.SightPosName.RED_DOWN;
    public SightSpriteController.SightPosName sightSalaryman = SightSpriteController.SightPosName.BLUE_DOWN;
    private SightSpriteController ssc;
    private PlayerCharacterController pcc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ssc = animator.GetComponentInChildren<SightSpriteController>();
        GameObject[] lsGO = GameObject.FindGameObjectsWithTag(Tags.player);
        if (lsGO.Length > 0)
        {
            pcc = lsGO[0].GetComponent<PlayerCharacterController>();
        }
        SkeletonAnimation skelAnim = animator.GetComponentInChildren<SkeletonAnimation>();
        Spine.TrackEntry currTrack = skelAnim.AnimationState.GetCurrent(0);
        if (currTrack == null || currTrack.Animation != skelAnim.skeleton.Data.FindAnimation(motionName))
        {
            skelAnim.AnimationState.SetAnimation(0, motionName, true).timeScale = speedAnim;
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ssc != null && pcc != null)
        {
            if (pcc.currPlayerStatus[PlayerCharacterController.StatusListElement.NINJA])
                 ssc.SetSightState(sightNinja);
            else ssc.SetSightState(sightSalaryman);
           
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
