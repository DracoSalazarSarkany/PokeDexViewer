using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Hace la llamada UnityWebRequest a la PokeAPI
/// Recoge los datos JSON y los parsea
/// </summary>
public class PokemonApiManager : MonoBehaviour
{
    // URL base de la PokeAPI para obtener datos de un Pokémon por ID o nombre
    private const string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

    /// <summary>
    /// Llama a la API con el nombre o ID del Pokémon.
    /// </summary>
    /// <param name="pokemonNameOrId">Nombre o ID que introdujo el usuario</param>
    /// <param name="onSuccess">Callback que se llama si la respuesta es correcta</param>
    /// <param name="onError">Callback que se llama si hay un error</param>
    public void GetPokemonData(string nameOrId, Action<PokemonDataClasses> onSuccess, Action<string> onError)
    {
        // Inicia la corrutina que realiza la petición web, usando el nombre en minúsculas por si el usuario escribió mayesculas
        StartCoroutine(GetPokemonCoroutine(nameOrId.ToLower(), onSuccess, onError));
    }

    /// <summary>
    /// Corrutina que realiza la petición real y maneja la respuesta.
    /// </summary>
    /// <param name="nameOrId">Nombre o ID en minesculas</param>
    /// <param name="onSuccess">Callback si todo sale bien</param>
    /// <param name="onError">Callback si ocurre un error</param>
    private IEnumerator GetPokemonCoroutine(string nameOrId, Action<PokemonDataClasses> onSuccess, Action<string> onError)
    {
        // Construye la URL completa y crea la solicitud GET
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + nameOrId);

        // Espera a que la petición finalice
        yield return request.SendWebRequest();

        // Si la petición se complete con exito
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Convierte el JSON recibido en un objeto de tipo PokemonDataClasses
            PokemonDataClasses data = JsonUtility.FromJson<PokemonDataClasses>(request.downloadHandler.text);

            // Llama al callback de éxito con los datos
            onSuccess?.Invoke(data);
        }
        else
        {
            // Si hubo un error (por ejemplo, Pokemon no existe), llama al callback de error
            onError?.Invoke("Pokémon no encontrado");
        }
    }
}
