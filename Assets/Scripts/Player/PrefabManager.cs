using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PrefabType
{
    BallOfFire,
    ImpactEffect
}

[Serializable]
public class PrefabEntry
{
    public PrefabType type;
    public GameObject prefab;
}

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private List<PrefabEntry> _prefabList;
    private static Dictionary<PrefabType, GameObject> _prefabs;

    private void Awake()
    {
        _prefabList = new(); //Se inicializa la lista de prefabs. Hay una lista y un diccionario de las mismas cosas porque el diccionario no se puede serializar.
        _prefabs = new(); //Se inicializa el diccionario
        foreach (var entry in _prefabList) _prefabs[entry.type] = entry.prefab; //Acá se copia el contenido de _prefabList a _prefabs

    }

    public static GameObject GetPrefab(PrefabType type)
    {
        if (_prefabs.TryGetValue(type, out var prefab)) return prefab;

        Debug.LogError($"Prefab {type} not found!");
        return null;
    }
}
