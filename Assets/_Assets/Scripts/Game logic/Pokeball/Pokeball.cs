using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    public class Pokeball : MonoBehaviour
    {
        public delegate void stateAction(string state);

        // <summary> Notify a new state through a string</summary>
        public stateAction UpdateState;

        // <summary> The current state</summary>
        private string state;


        // <summary> Pokemon with  which it is in the process of catch </summary>
        private Pokemon pokemonInside;

        // <summary> Count to try to catch, from 0 to 3 to finish, may fail on each try</summary>
        private int attemptToCatch;

        // <summary> Percent to keep in catch, necessary for each count</summary>
        [SerializeField] private int percentOfCatch;

        private Animator m_animator;
        private Transform m_transform;
        private Rigidbody m_rigidbody;


        // <summary> Reference of the particles to the result of the catch</summary>
        [SerializeField] private ParticleSystem successParticles, failParticles;


        #region basicMethods

        /// <summary>
        /// Allow change the state and notify with a event. It method is called by the StateMachineBehavior of the animator
        /// </summary>
        /// <param name="newState">new state</param>
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

            switch (state) //behavior demanded by the animator
            {
                case "InCatch":
                    consumingLifetime = false;

                    TryToFinishCatch();
                    break;
                case "Die":
                    DeactivateInstance();
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

            m_animator.SetTrigger("WakeUp"); //Notify a new state transition
        }

        /// <summary>
        /// Deactivate an instance to return the entity to the pool
        /// Clean the entity to a new use
        /// behavior demanded by the animator
        /// </summary>
        public void DeactivateInstance()
        {
            pokemonInside = null;
            m_rigidbody.isKinematic = true; //The first state need a pokeball in kinematic
            m_rigidbody.Sleep();
            gameObject.SetActive(false);
        }

        #endregion


        #region Launch&CatchSystem

        /// <summary>
        /// Apply force to launch the pokeball
        /// </summary>
        /// <param name="direction">local direction to launch</param>
        /// <returns>if the pokeball was launched</returns>
        public bool Launch(Vector3 direction)
        {
            if (state == "InHand")
            {
                m_rigidbody.isKinematic = false;
                m_transform.SetParent(null);
                m_rigidbody.AddRelativeForce(direction, ForceMode.Impulse);
                m_animator.SetTrigger("Launch"); //Notify a new state transition

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
        /// Start the flow of catch pokemon
        /// </summary>
        /// <param name="pokemon">pokemon with wich collision the pokeball</param>
        private void CatchPokemon(Pokemon pokemon)
        {
            m_animator.SetTrigger("Catch");
            m_rigidbody.isKinematic = true;


            pokemon.GetCaught(this);
            pokemonInside = pokemon;
        }

        /// <summary>
        /// Iterations to finish the flow of catch pokemon.
        /// Each that TryCatch animation ends, the pokeball try to finish the catch
        /// If the try is success this is a "attempt", with 3 success attempts the pokemon is caught
        /// If the attempt fail, the pokemon just escape
        /// </summary>
        private void TryToFinishCatch()
        {
            if (attemptToCatch == 3)
            {
                m_animator.SetTrigger("Caught"); //Notify a new state transition
                pokemonInside.CatchResult(true); //Notify the success catch to the pokemon
                Pokedex.instance.CatchNewPokemon(Int32.Parse(pokemonInside.MPokemonData
                    .ID)); //Notify the success catch to the pokedex
                successParticles.Play();
            }
            else
            {
                if (Random.Range(0, 100) < percentOfCatch)
                {
                    attemptToCatch++;
                }
                else
                {
                    m_animator.SetTrigger("Fail"); //Notify a new state transition
                    pokemonInside.CatchResult(false); //Notify the fail catch to the pokemon
                    failParticles.Play();
                }
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

        /// <summary>
        /// Consume the lifetime to die
        /// </summary>
        private void ConsumeLifetime()
        {
            remainingLifetime -= Time.deltaTime;
            if (remainingLifetime <= 0)
            {
                consumingLifetime = false;
                m_animator.SetTrigger("LifeTimeIsOver"); //Notify a new state transition
            }
        }

        #endregion
    }
}