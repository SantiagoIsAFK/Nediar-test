using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    /// <summary>
    /// Is the factory that allow the creation and the launch of the pokemon
    /// </summary>
    public class PokemonFactory : Factory
    {
        // <summary> Array with the respawn points </summary>
        [SerializeField] private Transform[] respawnPoints;

        // <summary> Time to respawn </summary>
        [SerializeField] private float respawnRateTime;

        // <summary> Step to reach the respawnRateTime </summary>
        private float currentTime;

        private void Start()
        {
            //var initialization
            currentTime = respawnRateTime;
        }

        private void Update()
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

        
        protected override Transform GetRespawn()
        {
            return respawnPoints[Random.Range(0, respawnPoints.Length)];
        }

        protected override void ConfigEntity(GameObject entity)
        {
            PokemonData pokemon = new PokemonData();

            //Here are a coroutine  because the obtain method need be asynchronous
            //because the web request can take time
            StartCoroutine(Pokedex.instance.GetPokemonData(
                Pokedex.instance.IDToCatchList[Random.Range(0, Pokedex.instance.IDToCatchList.Count)],
                result => { entity.GetComponent<Pokemon>().ConfigEntity(result); }
            ));
        }
    }
}