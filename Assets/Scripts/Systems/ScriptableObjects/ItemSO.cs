using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject, IItem
{
    [field: SerializeField]
    public string Name { get; set; }

    public readonly float amount;
    public Sprite icon;
    public ItemType type;

    public float Amount => amount;
    public string Description => throw new System.NotImplementedException();
}

public enum ItemType
{
    Herb,
    Potion
}