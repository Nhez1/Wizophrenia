using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum Prefab
{
    BallOfFire,
    ImpactEffect
}

[Serializable]
public class PrefabEntry
{
    public Prefab type;
    public GameObject prefab;
}

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private List<PrefabEntry> _prefabList = new(); //Se inicializa la lista de prefabs. Hay una lista y un diccionario de las mismas cosas porque el diccionario no se puede serializar.
    private static Dictionary<Prefab, GameObject> _prefabs = new(); //Se inicializa el diccionario

    private void Awake()
    {
        foreach (var entry in _prefabList) _prefabs[entry.type] = entry.prefab; //Acá se copia el contenido de _prefabList a _prefabs

    }

    public static GameObject GetPrefab(Prefab type)
    {
        if (_prefabs.TryGetValue(type, out var prefab)) return prefab;

        Debug.LogError($"Prefab {type} not found!");
        return null;
    }
}
