using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define clases para leer el JSON (name, sprites, types, altura y peso).
/// </summary>

[Serializable]
public class PokemonDataClasses
{
    public string name; // Nombre del Pokemon
    public float weight; // Peso
    public float height; // altura
    public Sprites sprites; // sprite
    public List<PokemonType> types; // lista de tipos
}

[Serializable]
public class PokemonType
{
    // El campo "type" contiene el nombre real del tipo
    public TypeInfo type;
}

[Serializable]
public class TypeInfo
{
    public string name;
}

// Contiene los sprites del Pokemon, de los cuales usaremos solo el frontal
[Serializable]
public class Sprites
{
    public string front_default;
}

