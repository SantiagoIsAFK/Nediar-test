using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class PokeballState : StateMachineBehaviour
    {
        // <summary>Reference to the entity</summary>
        private Pokeball m_pokeball;

        //<summary>Group of the states that will be notify to the entity</summary>
        [SerializeField] private string[] states = new string[]
            {"WakeUp", "InHand", "InLaunch", "InCatch", "SuccessfulCatch", "FailedCatch", "Disappearing", "Die"};

        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            //Make the entity reference
            if (m_pokeball == null)
            {
                m_pokeball = animator.gameObject.GetComponent<Pokeball>();
            }

            //Search each state and compare the states array, if it find ones, then, notify
            foreach (var state in states)
            {
                if (Animator.StringToHash(state) == stateInfo.shortNameHash) //NameHash is the only way to identify the animator state
                {
                    try
                    {
                        m_pokeball.ChangeState(state); //change the state, the entity will notify its state
                    }
                    catch (Exception e)
                    {
                    }
                }
            }

        }
    }
}