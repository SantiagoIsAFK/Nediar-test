using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PokedexUI : MonoBehaviour {
    public static PokedexUI instance { get; private set; }
    
    [SerializeField]
    private PokemonBaseStatsUI[] pokemonStatsArray;

    [SerializeField]
    private PokemonBaseStatsUI detailedPokemonBase;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //UpdateStatsList(0);
    }

    public void UpdateStatsList(float sliderIndex)
    {
        StopAllCoroutines();
        StartCoroutine(GetData((int)sliderIndex));
    }

    private IEnumerator GetData(int sliderIndex)
    {
        for (var i = 0; i < 10; i++)
        {
            pokemonStatsArray[i].ClearStats();
        }
        
        for (var i = 0; i < 10; i++)
        {
            yield return  StartCoroutine(Pokedex.instance.GetPokemonData((sliderIndex*10)+i+1, result =>
            {
                pokemonStatsArray[i].SetNewPokemon(result);
            }));
        }
        
    }

    /// <summary>
    /// @param _pokemon
    /// </summary>
    public void SetDetailedPokemon(PokemonData data)
    {
        detailedPokemonBase.SetNewPokemon(data);
    }

}