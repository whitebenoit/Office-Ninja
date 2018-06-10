using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUnitytoSpineController : StateMachineBehaviour
{

    protected AnimatorStateInfo stateInfo;
    public AnimatorStateInfo StateInfo
    {
        get { return stateInfo; }
    }

    public string motionName;
    public float speedAnim = 1;

   

    //[SerializeField]
    //public List<ChangeBool> onEnterChangeBool = new List<ChangeBool>();
    //[SerializeField]
    //public List<ChangeBool> onExitChangeBool = new List<ChangeBool>();

    public delegate void callbackFunc();
    public List<callbackFunc> onStateEnterCallbacks = new List<callbackFunc>();
    public List<callbackFunc> onStateUpdateCallbacks = new List<callbackFunc>();
    public List<callbackFunc> onStateExitCallbacks = new List<callbackFunc>();






    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.stateInfo = stateInfo;
        for (int i = 0; i < onStateEnterCallbacks.Count; i++)
        {
            onStateEnterCallbacks[i]();

        }

        //if (onEnterChangeBool.Count > 0)
        //{
        //    for (int i = 0; i < onEnterChangeBool.Count; i++)
        //    {
        //        animator.SetBool(onEnterChangeBool[i].name, onEnterChangeBool[i].newValue);
        //    }
        //}

        SkeletonAnimation skelAnim;
        if (animator.GetBool("isNinja"))
        {
            skelAnim = animator.GetComponent<PlayerCharacterController>().ninjaModel.GetComponent<SkeletonAnimation>();
        }
        else
        {
            skelAnim = animator.GetComponent<PlayerCharacterController>().salarymanModel.GetComponent<SkeletonAnimation>();
        }
        Spine.TrackEntry currTrack = skelAnim.AnimationState.GetCurrent(0);
        if (currTrack == null || currTrack.Animation != skelAnim.skeleton.Data.FindAnimation(motionName))
        {
            skelAnim.AnimationState.SetAnimation(0, motionName, true).timeScale = speedAnim;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < onStateUpdateCallbacks.Count; i++)
        {
            onStateUpdateCallbacks[i]();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (onExitChangeBool.Count > 0)
        //{
        //    for (int i = 0; i < onExitChangeBool.Count; i++)
        //    {
        //        animator.SetBool(onExitChangeBool[i].name, onExitChangeBool[i].newValue);
        //    }
        //}

        for (int i = 0; i < onStateExitCallbacks.Count; i++)
        {
            onStateExitCallbacks[i]();
        }

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}


public class ChangeBool
{
    public string name;
    public bool newValue;

    public void setName(string name)
    {
        this.name = name;
    }
    public void setNewValue(bool newValue)
    {
        this.newValue = newValue;
    }
}

public static class StateMachineBehaviourUtilities
{
    public static T GetBehaviour<T>(this Animator animator, AnimatorStateInfo stateInfo) where T : PlayerUnitytoSpineController
    {
        return animator.GetBehaviours<T>().ToList().First(behaviour => behaviour.StateInfo.fullPathHash == stateInfo.fullPathHash);
    }

    public static List<T> GetBehaviours<T>(this Animator animator) where T : PlayerUnitytoSpineController
    {
        return animator.GetBehaviours<T>().ToList();
    }

    public static T GetBehaviourByName<T>(this Animator animator, string name) where T: PlayerUnitytoSpineController
    {
        List<T> tList = GetBehaviours<T>(animator);
        foreach (T item in tList)
        {
            if (item.motionName == name)
            {
                return item;
            }
        }
        return null;
    }
}


