
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameLogic
{
    
    public class Pokedex : MonoBehaviour
    {
        
        // <summary> Total number of pokemon in the API</summary>
        [SerializeField] const int TotalNumOfPokemon = 893;
        
        // <summary> Singleton</summary>
        public static Pokedex instance;
        
        // <summary> API reference</summary>
        [SerializeField] private PokeAPIInterface pokeApi;

        public Pokedex()
        {
            instance = this;
        }
        
        // <summary>Array with the All Pokemon Data, it is </summary>
        [SerializeField] private PokemonData[] currentPokemonData;
        
        // <summary> Array with the id of all pokemon</summary>
        [SerializeField] private int[] pokemonIDs;
        
        // <summary> List of the caught pokemon and list of the pokemon to catch</summary>
        [SerializeField] private List<int> idToCatchList, idCaughtList;

        public List<int> IDToCatchList => idToCatchList;
        public List<int> IDCaughtList => idCaughtList;

        private void Awake()
        {
            CreatePokemonLists();
        }

        
        /// <summary>
        /// Create all the lists necessaries
        /// </summary>
        public void CreatePokemonLists()
        {
            pokemonIDs = new int[TotalNumOfPokemon];
            currentPokemonData = new PokemonData[TotalNumOfPokemon];

            for (int i = 0; i < TotalNumOfPokemon; i++)
            {
                pokemonIDs[i] = i + 1;
            }

            idToCatchList = pokemonIDs.ToList();
            idCaughtList = new List<int>();
        }

        /// <summary>
        /// Search an pokemon in the list, if its data does not exist, then, ask to the API for the pokemon data
        /// </summary>
        /// <param name="index">Index in the pokedex</param>
        /// <param name="callback">Pokemon data result</param>
        /// <returns>Courutine</returns>
        public IEnumerator GetPokemonData(int index, Action<PokemonData> callback)
        {
            if (index >= 0 && index < TotalNumOfPokemon)
            {
                if (currentPokemonData[index] != null)
                {
                    callback(currentPokemonData[index]);

                    yield break;
                }
                else //if its data does not exist
                {
                    PokemonData tPokemon = new PokemonData();

                    yield return StartCoroutine(pokeApi.GetPokemonBaseData(index, (result) => { tPokemon = result; }));

                    currentPokemonData[index] = tPokemon;

                    callback(tPokemon);
                }
            }
        }

        /// <summary>
        /// Add a new pokemon in the caught list
        /// </summary>
        /// <param name="idCaught">Pokemon id</param>
        public void CatchNewPokemon(int idCaught)
        {
            int i = idToCatchList.FindIndex(poke => poke == idCaught);
            if (i >= 0)
            {
                idToCatchList.RemoveAt(i);
                idCaughtList.Add(idCaught);
            }
            else
            {
                Debug.Log("This pokemon is already in the caughtList");
            }
        }

    }
}