using UnityEngine;

public abstract class ItemSO : ScriptableObject, IItem
{
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public Sprite Icon { get; set; }
    [field: SerializeField]
    public ItemType Type { get; set; }
    public float Amount { get; private set; }
    public string Description => throw new System.NotImplementedException();
}

public enum ItemType
{
    Herb,
    BadHerb,
    LotusFlower,
    Potion,
    EdibleFlower
}