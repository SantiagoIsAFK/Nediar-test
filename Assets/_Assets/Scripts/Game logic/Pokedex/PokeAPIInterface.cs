using System;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class PokeAPIInterface : MonoBehaviour{

    private static readonly string basePokeURL = "https://pokeapi.co/api/v2/";
  
    
    /// <summary>
    /// @param _id
    /// </summary>
    public IEnumerator GetPokemonBaseData(int id, Action<PokemonData> callback)
    {
        
        // Get Pokemon Info

        string pokemonURL = basePokeURL + "pokemon/" + id.ToString();
        // Example URL: https://pokeapi.co/api/v2/pokemon/151

        UnityWebRequest pokeInfoRequest = UnityWebRequest.Get(pokemonURL);

        yield return pokeInfoRequest.SendWebRequest();

        if (pokeInfoRequest.isNetworkError || pokeInfoRequest.isHttpError)
        {
            Debug.LogError(pokeInfoRequest.error);
            yield break;
        }

        JSONNode pokeInfo = JSON.Parse(pokeInfoRequest.downloadHandler.text);

        string name = pokeInfo["name"];
        string weight = pokeInfo["weight"];
        string height = pokeInfo["height"];
        

        JSONNode pokeTypes = pokeInfo["types"];
        string types = "";

        for (int i = 0, j = pokeTypes.Count - 1; i < pokeTypes.Count; i++, j--)
        {
            types += pokeTypes[i]["type"]["name"]+" ";
        }

        // Get Pokemon Sprite

        string[] spritesURL = new string[4];
        spritesURL[0] = pokeInfo["sprites"]["front_default"];
        spritesURL[1] = pokeInfo["sprites"]["back_default"];
        spritesURL[2] = pokeInfo["sprites"]["front_shiny"];
        spritesURL[3] = pokeInfo["sprites"]["back_shiny"];



        UnityWebRequest pokeSpriteRequest;
        Sprite[] sprites = new Sprite[4];

        
        for (int i = 0; i < 4; i++)
        {
            pokeSpriteRequest = UnityWebRequestTexture.GetTexture(spritesURL[i]);
            
            yield return pokeSpriteRequest.SendWebRequest();
        
            if (pokeSpriteRequest.isNetworkError || pokeSpriteRequest.isHttpError)
            {
                Debug.LogWarning(pokeSpriteRequest.error);

                if (i > 0)
                {
                    sprites[i] = Sprite.Create(sprites[i-1].texture, new Rect(0, 0, sprites[i-1].texture.width, sprites[i-1].texture.height),new Vector2(0.5f, 0.5f));
                }
                else
                {
                    sprites[i] = Sprite.Create(Texture2D.normalTexture, new Rect(0, 0, Texture2D.normalTexture.width, Texture2D.normalTexture.height),new Vector2(0.5f, 0.5f));
                }
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(pokeSpriteRequest);

                sprites[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }

            
        }

        PokemonData newPokemon = new PokemonData(id+"", name, types, weight, height, sprites);

        callback(newPokemon);
    }

    
    /*
    public IEnumerator GetPokemonSpriteData(int id, Action<Sprite[]> callback)
    {
        
        // Get Pokemon Info
        string pokemonURL = basePokeURL + "pokemon/" + id.ToString();
        // Example URL: https://pokeapi.co/api/v2/pokemon/151

        
        // Get Pokemon Sprite

        string[] spritesURL = new string[4];
        spritesURL[0] = pokeInfo["sprites"]["front_default"];
        spritesURL[1] = pokeInfo["sprites"]["front_shiny"];
        spritesURL[2] = pokeInfo["sprites"]["back_default"];
        spritesURL[3] = pokeInfo["sprites"]["back_shiny"];



        UnityWebRequest pokeSpriteRequest;
        Sprite[] sprites = new Sprite[4];

        
        for (int i = 0; i < 4; i++)
        {
            pokeSpriteRequest = UnityWebRequestTexture.GetTexture(spritesURL[i]);
            
            yield return pokeSpriteRequest.SendWebRequest();
        
            if ((pokeSpriteRequest.result == UnityWebRequest.Result.ConnectionError) || (pokeSpriteRequest.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.LogError(pokeSpriteRequest.error);
                yield break;
            }
            
            // Set UI Objects
            Texture2D texture = DownloadHandlerTexture.GetContent(pokeSpriteRequest);

            sprites[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        callback(sprites);
    }
    */
}