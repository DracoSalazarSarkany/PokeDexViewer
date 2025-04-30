using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

/// <summary>
/// Se encarga de actualizar los InputField, Text, Image en el Canvas.
/// </summary>
public class PokemonUIController : MonoBehaviour
{
    public TMP_InputField pokemonInputField; // Campo donde el usuario introduce el nombre o ID

    public Button searchButton; // Boton de búsqueda

    public TextMeshProUGUI pokemonName; // Texto para mostrar el nombre

    public TextMeshProUGUI pokemonType; // Texto para mostrar los tipos

    public TextMeshProUGUI pokemonWeightHeight; // Texto para peso y altura

    public Image pokemonImage; // Imagen para mostrar el sprite

    public TextMeshProUGUI errorText; // Texto para mostrar el mensaje de error

    public PokemonApiManager pokemonAPIManager; // Referencia a la API

    
    // Start is called before the first frame update
    void Start()
    {
        // Asigna el método OnSearchClicked al boton cuando se hace clic
        searchButton.onClick.AddListener(OnSearchClicked);

        // Oculta el texto de error al iniciar (esto tengo que hacerlo en Qios Tablet con los paneles del principio)
        errorText.gameObject.SetActive(false);
    }

    // Método que se ejecuta al hacer clic en el boton de buscar
    public void OnSearchClicked()
    {
        string input = pokemonInputField.text; // Obtiene el texto introducido por el usuario

        // Si no esta vacio, hace la busqueda
        if (!string.IsNullOrEmpty(input))
        {
            pokemonAPIManager.GetPokemonData(input, OnPokemonFound, OnError);
        }
    }

    // Metodo que se llama si la busqueda es exitosa
    public void OnPokemonFound(PokemonDataClasses data)
    {
        // Llama a la corrutina para cargar la imagen desde la URL del sprite
        StartCoroutine(LoadImage(data.sprites.front_default));

        // Muestra el nombre en mayusculas
        pokemonName.text = data.name.ToUpper();

        // Muestra los tipos separados por comas
        pokemonType.text = "Tipo: " + string.Join(", ", data.types.ConvertAll(t => t.type.name));

        // Muestra peso y altura
        pokemonWeightHeight.text = $"Peso: {data.weight} Altura: {data.height}";

        // Oculta el texto de error si estaba visible
        errorText.gameObject.SetActive(false);
    }

    // Metodo que se llama si ocurre un error en la busqueda
    public void OnError(string message)
    {
        // Limpia los datos de la UI
        pokemonImage.sprite = null;
        pokemonName.text = "";
        pokemonType.text = "";
        pokemonWeightHeight.text = "";

        // Muestra el mensaje de error
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }

    // Corrutina para cargar una imagen desde una URL y mostrarla en la UI
    private IEnumerator LoadImage(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest(); // Espera la respuesta

        // Si se descargo correctamente, convierte la textura a sprite
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(request);
            pokemonImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            // Si falla la descarga, muestra un error en la consola
            Debug.LogError("Error al cargar la imagen: " + request.error);
        }
    }

    
}
