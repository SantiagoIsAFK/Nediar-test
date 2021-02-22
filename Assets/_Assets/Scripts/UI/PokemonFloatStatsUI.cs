using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

namespace PokeUI
{
    /// <summary>
    /// Write the basic data of a Pokemon in a UI with a reference in its own GameObject
    /// Necessary to represent the data over the pokemon
    /// </summary>
    public class PokemonFloatStatsUI : PokemonBaseStatsUI
    {
        [SerializeField] private Pokemon _pokemon;

        void Awake()
        {
            _pokemon.UpdateState += UpdatePokemonData;
        }

        
        /// <summary>
        /// Update the data when the pokemon changes and it wake up
        /// </summary>
        /// <param name="state">state</param>
        void UpdatePokemonData(string state)
        {
            if (state == "WakeUp")
            {
                base.SetNewPokemon(_pokemon.MPokemonData);
                base.UpdateStats();
            }
        }
    }
}