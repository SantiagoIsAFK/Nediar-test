using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic
{
    public class Pokemon : MonoBehaviour
    {
        public delegate void stateAction(string state);

        // <summary> Notify a new state through a string</summary>
        public stateAction UpdateState;

        // <summary> Data from PokeAPI</summary>
        private PokemonData m_pokemonData;

        public PokemonData MPokemonData
        {
            get => m_pokemonData;
        }

        private Collider m_collider;
        private Animator m_animator;
        [SerializeField] private SpriteRenderer sprite;


        #region basicMethods

        /// <summary>
        /// Allow change the state and notify with a event. It method is called by the StateMachineBehavior of the animator
        /// </summary>
        /// <param name="newState">new state</param>
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

            switch (state) //behavior demanded by the animator
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

            sprite.sprite = m_pokemonData.Sprites[0];

            remainingLifetime = lifetime;
            m_collider.enabled = true;

            m_animator.SetTrigger("WakeUp"); //Notify a new state transition
        }

        /// <summary>
        /// Deactivate an instance to return the entity to the pool
        /// Clean the entity to a new use
        /// behavior demanded by the animator
        /// </summary>
        public void DesactiveInstance()
        {
            m_pokemonData = null;
            gameObject.SetActive(false);
        }

        #endregion


        #region CatchSystem

        /// <summary>
        /// The pokemon was collision by a Pokeball and enter in a new state
        /// </summary>
        /// <param name="_pokeball">Pokeball that collided</param>
        public void GetCaught(Pokeball _pokeball)
        {
            m_collider.enabled = false;
            m_animator.SetTrigger("Catch"); //Notify a new state transition
        }

        /// <summary>
        /// Notify the result of the catch
        /// </summary>
        /// <param name="result">result</param>
        public void CatchResult(bool result)
        {
            if (result)
                m_animator.SetTrigger("Caught"); //Notify a new state transition
            else
            {
                m_collider.enabled = true;
                m_animator.SetTrigger("CatchFailed"); //Notify a new state transition
                remainingLifetime += lifetime / 4;
            }
        }

        #endregion


        #region OtherFunctions

        // <summary>Is consuming the entity life</summary>
        private bool consumingLifetime = false;

        // <summary>Lifetime to die</summary>
        [SerializeField] private float lifetime;

        // <summary>Remaining time of the lifetime</summary>
        private float remainingLifetime;

        private void Update()
        {
            if (consumingLifetime)
            {
                ConsumeLifetime();
            }
        }

        private void ConsumeLifetime()
        {
            remainingLifetime -= Time.deltaTime;
            if (remainingLifetime <= 0)
            {
                consumingLifetime = false;
                m_animator.SetTrigger("LifeTimeIsOver"); //Notify a new state transition
            }
        }

        private bool inDanger;

        /// <summary>
        /// @return
        /// </summary>
        private void CheckPlayerDistance()
        {
        }

        #endregion
    }
}