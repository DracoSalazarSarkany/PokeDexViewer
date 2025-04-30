using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    // Salir del juego
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
