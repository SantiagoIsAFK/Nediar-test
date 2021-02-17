using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PokemonState : StateMachineBehaviour
{
    private Pokemon m_pokemon;
    private string[] states = new string[] {"WakeUp", "InScene", "InCatch", "Caught", "Escaping", "InDanger", "Die"};
        
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (m_pokemon == null)
        {
            m_pokemon = animator.gameObject.GetComponent<Pokemon>();
        }

        

        foreach (var state in states)
        {
            if (Animator.StringToHash(state) == stateInfo.shortNameHash)
            {
                try
                {
                    m_pokemon.ChangeState(state);
                }
                catch (Exception e)
                {
                }
            }
        }
        
    }

    
}
