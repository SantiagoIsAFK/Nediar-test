using System;
using System.Collections;
using UnityEngine;
using GameLogic;

namespace PokeUI
{
    /// <summary>
    /// Manage the other elements of the UI. 
    /// </summary>
    public class PokedexUI : MonoBehaviour
    {
        // <summary>Singleton</summary>
        public static PokedexUI instance { get; private set; }

        
        // <summary>Array with the basic info of the pokemon</summary>
        [SerializeField] private PokemonBaseStatsUI[] pokemonStatsArray;
        
        // <summary>Reference to detailed the pokemon</summary>
        [SerializeField] private PokemonBaseStatsUI detailedPokemonBase;


        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// Start the coroutine to update all array with the pokemon basic info
        /// </summary>
        /// <param name="sliderIndex">index of the slider</param>
        public void UpdateStatsList(float sliderIndex)
        {
            StopAllCoroutines();
            StartCoroutine(GetData((int) sliderIndex));
        }

        /// <summary>
        /// Coroutine to obtain the data
        /// </summary>
        /// <param name="sliderIndex">index of the slider</param>
        /// <returns>coroutine</returns>
        private IEnumerator GetData(int sliderIndex)
        {
            for (var i = 0; i < 10; i++) //firsts, clean the UI data
            {
                pokemonStatsArray[i].ClearStats();
            }

            for (var i = 0; i < 10; i++)
            {
                yield return StartCoroutine(Pokedex.instance.GetPokemonData((sliderIndex * 10) + i,
                    result => { pokemonStatsArray[i].SetNewPokemon(result); }));
            }
        }

        /// <summary>
        /// Receive a pokemon data to tell to the detailedPokemonBase reference for its detail
        /// </summary>
        /// <param name="data">Pokemon data to detail</param>
        public void SetDetailedPokemon(PokemonData data)
        {
            detailedPokemonBase.SetNewPokemon(data);
        }
    }
}