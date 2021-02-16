
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Pokedex : MonoBehaviour
{
    const int NumOfPokemon = 893;
    
    public static Pokedex instance;
    
    [SerializeField]
    private PokeAPIInterface pokeApi;

    public Pokedex()
    {
        instance = this;
    }

    [SerializeField]
    private PokemonData[] currentPokemonData;
    [SerializeField]
    private int[] pokemonIDs;
    [SerializeField]
    private List<int> idToCatchList, idCaughtList;

    private void Awake()
    {
        CreatePokemonList();
    }

    public void CreatePokemonList()
    {
        pokemonIDs = new int[NumOfPokemon];
        currentPokemonData = new PokemonData[NumOfPokemon];
        
        for (int i = 0; i < NumOfPokemon; i++)
        {
            pokemonIDs[i] = i+1;
        }
        
        idToCatchList = pokemonIDs.ToList();
        idCaughtList = new List<int>();
    }

    public IEnumerator GetPokemonData(int index, Action<PokemonData> callback)
    {
        if (index>0 && index <NumOfPokemon)
        {
            if (currentPokemonData[index - 1] != null)
            {
                callback(currentPokemonData[index - 1]);
                
                yield break;
            }
            else
            {
                PokemonData tPokemon = new PokemonData();

                yield return StartCoroutine(pokeApi.GetPokemonBaseData(index, (result) => { tPokemon = result; }));

                currentPokemonData[index - 1] = tPokemon;
            
                callback(tPokemon);
            }
        }
    }

    /// <summary>
    /// @param _pokemon
    /// </summary>
    private void CatchNewPokemon(int idCaught)
    {
        int i = idToCatchList.FindIndex(poke => poke == idCaught);
        if (i>=0)
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