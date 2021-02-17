using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonDetailedStatsUI : PokemonBaseStatsUI
{
    [SerializeField]
    private Text txtSpecies, txtWeight, txtHeight;


    [SerializeField] private Image sprite;
    
    public override void ClearStats()
    {
        base.UpdateStats();
        txtSpecies.text = "";
        txtWeight.text = "";
        txtHeight.text = "";
        sprite.sprite = null;
    }
    
    protected override void UpdateStats()
    {
        base.UpdateStats();
        txtSpecies.text = "Species: "+m_pokemonData.Types;
        txtWeight.text = "Weight: "+m_pokemonData.Weight;
        txtHeight.text = "Height: "+m_pokemonData.Height;
        sprite.sprite = m_pokemonData.Sprites[0];
    }
}
