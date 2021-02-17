using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class PokemonFactory : Factory
{
    [SerializeField]
    private Transform[] respawnPoints;

    [SerializeField]
    private float respawnRateTime;
    private float currentTime;

    private void Start()
    {
        currentTime = respawnRateTime - 5;
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
            currentTime+= Time.deltaTime;
        }
    }

    /// <summary>
    /// @return
    /// </summary>
    protected override Transform GetRespawn()
    {
        return respawnPoints[Random.Range(0, respawnPoints.Length)];
    }

    /// <summary>
    /// @return
    /// </summary>
    protected override void ConfigEntity(GameObject entity)
    {
        PokemonData pokemon = new PokemonData();

        StartCoroutine(Pokedex.instance.GetPokemonData(
            Pokedex.instance.IDToCatchList[Random.Range(0, Pokedex.instance.IDToCatchList.Count)],
            result =>
            {
                entity.GetComponent<Pokemon>().ConfigEntity(result); 
            }
        ));

    }
    
    

}