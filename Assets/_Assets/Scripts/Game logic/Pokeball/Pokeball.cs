using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pokeball  : MonoBehaviour 
{
    public delegate void stateAction(string state);
    public stateAction UpdateState;
    private string state;
    
    
    private Pokemon pokemonInside;
    private int attemptToCatch;
    [SerializeField] private int percentOfCatch;

    private Animator m_animator;
    private Transform m_transform;
    private Rigidbody m_rigidbody;
    

    #region basicMethods
    /// <summary>
    /// @param _state 
    /// @return
    /// </summary>
    public void ChangeState(string newState)
    {
        state = newState;
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
            case "InCatch":
                consumingLifetime = false;

                TryToFinishCatch();
                break;
            case "Die":
                DesactiveInstance();
                break;
            
        }
    }

    public void ConfigEntity()
    {
        m_animator = this.GetComponent<Animator>();
        m_transform = this.GetComponent<Transform>();
        m_rigidbody = this.GetComponent<Rigidbody>();

        remainingLifetime = lifetime;
        attemptToCatch = 0;
        
        m_animator.SetTrigger("WakeUp");
    }

    /// <summary>
    /// @return
    /// </summary>
    public void DesactiveInstance()
    {
        pokemonInside = null;
        m_rigidbody.isKinematic = true;
        m_rigidbody.Sleep();
        gameObject.SetActive(false);
    }
    #endregion


    #region Launch&CatchSystem
    /// <summary>
    /// @param _pokemon
    /// </summary>
    public bool Launch(Vector3 direction)
    {
        if (state == "InHand")
        {
            m_rigidbody.isKinematic = false;
            m_transform.SetParent(null);
            m_rigidbody.AddForce(direction, ForceMode.Impulse);
            m_animator.SetTrigger("Launch");

            consumingLifetime = true;
            return true;
        }

        return false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pokemon"))
        {
            CatchPokemon(other.gameObject.GetComponent<Pokemon>());
        }
    }
    /// <summary>
    /// @param _pokemon
    /// </summary>
    private void CatchPokemon(Pokemon pokemon) {
        
        m_animator.SetTrigger("Catch");
        m_rigidbody.isKinematic = true;


        pokemon.GetCaught(this);
        pokemonInside = pokemon;
    }

    private void TryToFinishCatch()
    {
        if (attemptToCatch == 3)
        {
            m_animator.SetTrigger("Caught");
            pokemonInside.CatchResult(true);
            Pokedex.instance.CatchNewPokemon(Int32.Parse(pokemonInside.MPokemonData.ID));
        }
        else
        {
            if (Random.Range(0, 100) < percentOfCatch)
            {
                attemptToCatch++;
            }
            else
            {
                m_animator.SetTrigger("Fail");
                pokemonInside.CatchResult(false);
            }
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

    #endregion
    
    

}