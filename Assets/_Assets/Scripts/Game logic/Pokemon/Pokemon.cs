using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pokemon  : MonoBehaviour
{
    public delegate void stateAction(string state);
    public stateAction UpdateState;
    
    private PokemonData m_pokemonData;

    public PokemonData MPokemonData
    {
        get => m_pokemonData;
    }
    
    private Collider m_collider;
    private Animator m_animator;
    [SerializeField]
    private SpriteRenderer frontSprite, backSprite;


    #region basicMethods
    /// <summary>
    /// @param _state 
    /// @return
    /// </summary>
    public void ChangeState(string state)
    {
        try
        {
            UpdateState(state);
        }
        catch (Exception e)
        {
            //Without suscriptions
        }
        
        switch (state)      
        {
            case "InScene":
            case "InDanger":
                consumingLifetime = true;
                break;
            case "Die":
                DesactiveInstance();
                break;
            default:
                consumingLifetime = false;
                break;
        }
    }

    public void ConfigEntity(PokemonData pokemon)
    {
        m_animator = this.GetComponent<Animator>();
        m_collider = this.GetComponent<Collider>();
        m_pokemonData = pokemon;
        
        frontSprite.sprite = m_pokemonData.Sprites[0];
        backSprite.sprite = m_pokemonData.Sprites[1];
        
        
        remainingLifetime = lifetime;
        m_collider.enabled = true;
        
        m_animator.SetTrigger("WakeUp");
    }

    /// <summary>
    /// @return
    /// </summary>
    public void DesactiveInstance()
    {
        m_pokemonData = null;
        gameObject.SetActive(false);
    }
    #endregion


    #region CatchSystem
    /// <summary>
    /// @param _pokeball  
    /// @return
    /// </summary>
    public void GetCaught(Pokeball _pokeball )
    {
        m_collider.enabled = false;
        m_animator.SetTrigger("Catch");
    }

    /// <summary>
    /// @return
    /// </summary>
    public void CatchResult(bool result) {
        if(result) 
            m_animator.SetTrigger("Caught");
        else
        {
            m_collider.enabled = true;
            m_animator.SetTrigger("CatchFailed");
        }
    }
    #endregion


    #region OtherFunctions
    private bool consumingLifetime=false;
    
    [SerializeField]
    private float lifetime;

    private float remainingLifetime;

    private void Update()
    {
        if (consumingLifetime)
        {
            ConsumeLifetime();
        }
    }

    private void ConsumeLifetime() {
        
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0)
        {
            consumingLifetime = false;
            m_animator.SetTrigger("LifeTimeIsOver");
        }
    }

    private bool inDanger;
    /// <summary>
    /// @return
    /// </summary>
    private void CheckPlayerDistance() {
    }
    #endregion
    
    

}