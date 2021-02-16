using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pokemon  : MonoBehaviour 
{
    private PokemonData _pokemonData;

    private pokemonState state;


    /// <summary>
    /// @param _state 
    /// @return
    /// </summary>
    private void ChangeState(pokemonState _state) {
    }

    /// <summary>
    /// @param _pokeball  
    /// @return
    /// </summary>
    public IEnumerator  GetCaught(Pokeball _pokeball ) {
        yield return new WaitForSeconds(5);
    }

    /// <summary>
    /// @return
    /// </summary>
    private IEnumerator EscapeFromPokeball() {
        // TODO implement here
        return null;
    }

    /// <summary>
    /// @return
    /// </summary>
    private void CheckPlayerDistance() {
    }

    /// <summary>
    /// @return
    /// </summary>
    private void UpdateAnimator() {
    }

}