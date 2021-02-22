using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameLogic
{
    public class PokemonState : StateMachineBehaviour
    {
        // <summary>Reference to the entity</summary>
        private Pokemon m_pokemon;

        //<summary>Group of the states that will be notify to the entity</summary>
        [SerializeField] private string[] states = new string[]
            {"WakeUp", "InScene", "InCatch", "Caught", "Escaping", "InDanger", "Die"};

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            //Make the entity reference
            if (m_pokemon == null)
            {
                m_pokemon = animator.gameObject.GetComponent<Pokemon>();
            }


            //Search each state and compare the states array, if it find ones, then, notify
            foreach (var state in states)
            {
                if (Animator.StringToHash(state) == stateInfo.shortNameHash) //NameHash is the only way to identify the animator state
                {
                    try
                    {
                        m_pokemon.ChangeState(state); //change the state, the entity will notify its state
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }
    }
}