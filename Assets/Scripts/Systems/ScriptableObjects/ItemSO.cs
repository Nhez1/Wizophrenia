using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject, IItem
{
    [SerializeField] private string _itemName;
    public readonly float amount;
    public Sprite icon;
    public ItemType type;

    public string Name => _itemName;
    public float Amount => amount;
    public string Description => throw new System.NotImplementedException();
}

public enum ItemType
{
    Herb,
    Potion
}