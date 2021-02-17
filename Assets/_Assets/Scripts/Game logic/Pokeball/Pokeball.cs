using UnityEngine;
public class Pokeball  : MonoBehaviour 
{
    public delegate void stateAction(string state);
    public stateAction UpdateState;
    
    
    private Pokemon pokemonInside;


    /// <summary>
    /// @param _pokemon
    /// </summary>
    private void CatchPokemon(Pokemon _pokemon) {
        // TODO implement here
    }

}