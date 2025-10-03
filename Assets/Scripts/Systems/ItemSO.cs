using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType type;
}

public enum ItemType
{
    Plant
}