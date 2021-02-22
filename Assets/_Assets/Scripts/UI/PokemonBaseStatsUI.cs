using UnityEngine;
using UnityEngine.UI;
using GameLogic;

namespace PokeUI
{
    /// <summary>
    /// Allow write the basic data of a pokemon in the UI
    /// </summary>
    public class PokemonBaseStatsUI : MonoBehaviour
    {
        protected PokemonData m_pokemonData;

        public PokemonData Pokemon
        {
            get => m_pokemonData;
        }


        [SerializeField] private Text txtID, txtName;

        /// <summary>
        /// Change the pokemon
        /// </summary>
        /// <param name="data">Pokemon data to read</param>
        public void SetNewPokemon(PokemonData data)
        {
            m_pokemonData = data;
            UpdateStats();
        }

        public virtual void ClearStats()
        {
            m_pokemonData = null;
            txtID.text = "Loading";
            txtName.text = "...";
        }

        protected virtual void UpdateStats()
        {
            txtID.text = "ID: #" + m_pokemonData.ID;
            txtName.text = m_pokemonData.Name;
        }

        /// <summary>
        /// Send the pokemon data of this object, to the Pokedex UI to detail its data
        /// </summary>
        public void DetailPokemon()
        {
            if (m_pokemonData != null)
            {
                PokedexUI.instance.SetDetailedPokemon(m_pokemonData);
            }
            else
            {
                Debug.LogWarning("without pokemon data");
            }
        }
    }
}