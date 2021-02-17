using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballState : StateMachineBehaviour
{
    private Pokeball m_pokeball;
    [SerializeField]
    private string[] states = new string[] {"WakeUp", "InHand", "InLaunch", "InCatch", "SuccessfulCatch", "FailedCatch", "Disappearing", "Die"};
        
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (m_pokeball == null)
        {
            m_pokeball = animator.gameObject.GetComponent<Pokeball>();
        }

        

        foreach (var state in states)
        {
            if (Animator.StringToHash(state) == stateInfo.shortNameHash)
            {
                try
                {
                    m_pokeball.ChangeState(state);
                }
                catch (Exception e)
                {
                }
            }
        }
        
    }
}
