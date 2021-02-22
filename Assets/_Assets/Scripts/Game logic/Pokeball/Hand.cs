using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic
{
    /// <summary>
    /// Is the factory that allow the creation and the launch of the pokeball
    /// </summary>
    public class Hand : Factory
    {
        // <summary> Respawn point </summary>
        [SerializeField] private Transform handAxis;

        // <summary> Time to respawn </summary>
        [SerializeField] private float respawnRateTime;

        // <summary> Step to reach the respawnRateTime </summary>
        private float currentTime;

        //<summary> state </summary>
        private bool handIsEmpty;

        //<summary> reference to the last pokeball launched </summary>
        private Pokeball lastPokeball;

        //<summary> force to launch </summary>
        [SerializeField] private float launchForce;

        private void Start()
        {
            //var initialization
            currentTime = respawnRateTime;
            handIsEmpty = true;
        }

        /// <summary>
        /// Empty the hand and call the method Launch of the Pokeball
        /// </summary>
        public void Launch()
        {
            if (lastPokeball.Launch(launchForce * Vector3.up))
            {
                handIsEmpty = true;
            }
        }

        private void Update()
        {
            //Calculate the respawn time of the Pokeball
            if (handIsEmpty)
            {
                if (currentTime >= respawnRateTime)
                {
                    InstantiateEntity();
                    currentTime = 0;
                }
                else
                {
                    currentTime += Time.deltaTime;
                }
            }
        }

        protected override Transform GetRespawn()
        {
            return handAxis;
        }

        protected override void ConfigEntity(GameObject entity)
        {
            lastPokeball = entity.GetComponent<Pokeball>();
            lastPokeball.ConfigEntity();

            handIsEmpty = false;
        }
    }
}