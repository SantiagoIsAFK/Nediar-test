using UnityEngine;


public class PokemonFactory : Factory {

    public PokemonFactory() {
    }

    /// <summary>
    /// @return
    /// </summary>
    protected override Transform GetRespawn()
    {
        return null;
    }

    /// <summary>
    /// @return
    /// </summary>
    protected override void ConfigEntity() {
    }

}