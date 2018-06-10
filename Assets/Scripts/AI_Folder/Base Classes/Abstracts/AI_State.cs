using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AI_State : ScriptableObject {

    public bool isToStopOnEnter = false;

    public string[] brainAnimBoolList;


    [System.Serializable]
    [HideInInspector]
    public struct ActionListElement { public AI_Condition condition;public AI_Action action; }
    public ActionListElement[] actionsList;

    [System.Serializable]
    [HideInInspector]
    public struct TransitionListElement {  public AI_Condition condition; public AI_State nextState; }
    public TransitionListElement[] transitionsList;

    

    /// <summary>
    /// Executes all the actions (and BeforeActions and AfterActions) then return the next State
    /// </summary>
    /// <param name="brain"> behavior brain </param>
    /// <returns></returns>
    public AI_State UpdateState(AI_BehaviorBrain brain)
    {
        BeforeActions(brain);
        DoActions(brain);
        AfterActions(brain);
        return CheckTransitions(brain);
    }
    private void DoActions (AI_BehaviorBrain brain)
    {
        for (int i = 0; i < actionsList.Length; i++)
        {
            if (actionsList[i].condition.Evaluate(brain))
            {
                Dictionaries.AudioName aN = actionsList[i].action.audioToPlay;
                if(aN != Dictionaries.AudioName.NONE)
                {
                    int ID = Dictionaries.instance.dic_audioID[aN];
                    if(!SoundManager.GetAudio(ID).playing)
                        SoundManager.GetAudio(ID).Play();
                }
                
               actionsList[i].action.Act(brain);
            }
            BetweenActions(brain);
        }
    }
    private AI_State CheckTransitions(AI_BehaviorBrain brain)
    {
        for (int i = 0; i < transitionsList.Length; i++)
        {
            TransitionListElement currTransListElmt = transitionsList[i];
            if(currTransListElmt.condition.Evaluate(brain)
                && currTransListElmt.nextState != null)
            {
                return currTransListElmt.nextState;
            }
            
        }
        return null;
    }

    public void InitializeStateForBrain(AI_BehaviorBrain brain)
    {
        if (isToStopOnEnter)
            brain.navMeshAgent.isStopped = true;
        for (int i = 0; i < brainAnimBoolList.Length; i++)
        {
            brain.brain_animator.SetBool(brainAnimBoolList[i], true);
        }
        for (int i = 0; i < transitionsList.Length; i++)
        {
            transitionsList[i].condition.InitializeStateForBrain(brain);
        }
    }

    public void EndStateForBrain(AI_BehaviorBrain brain)
    {
        for (int i = 0; i < brainAnimBoolList.Length; i++)
        {
            brain.brain_animator.SetBool(brainAnimBoolList[i], false);
        }
        for (int i = 0; i < transitionsList.Length; i++)
        {
            transitionsList[i].condition.EndStateForBrain(brain);
        }
    }

    public abstract void BeforeActions(AI_BehaviorBrain brain);
    public abstract void BetweenActions(AI_BehaviorBrain brain);
    public abstract void AfterActions(AI_BehaviorBrain brain);



}
