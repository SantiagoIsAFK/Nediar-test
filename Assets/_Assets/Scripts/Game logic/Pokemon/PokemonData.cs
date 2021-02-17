using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonData
{
    private string id;
    private string name;
    private string types;
    private string weight;
    private string height;
    private Sprite[] sprites;
    
    public string ID => id;

    public string Name => name;

    public string Types => types;

    public string Weight => weight;

    public string Height => height;

    public Sprite[] Sprites => sprites;

    public PokemonData()
    {
        
    }
    
    public PokemonData(string id, string name, string types, string weight, 
        string height, Sprite[] sprites)
    {
        this.id = id;
        this.name = name;
        this.types = types;
        this.weight = weight;
        this.height = height;
        this.sprites = sprites;
    }
}
