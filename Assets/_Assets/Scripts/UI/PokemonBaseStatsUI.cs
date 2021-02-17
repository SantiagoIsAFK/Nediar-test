using UnityEngine;
using UnityEngine.UI;

public class PokemonBaseStatsUI : MonoBehaviour {

    protected PokemonData m_pokemonData;

    public PokemonData Pokemon
    {
        get => m_pokemonData;
    }

    
    [SerializeField]
    private Text txtID, txtName;

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
        txtID.text = "ID: #"+m_pokemonData.ID;
        txtName.text = m_pokemonData.Name;
    }

    public void DetailPokemon() {
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