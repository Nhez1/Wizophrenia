using UnityEngine;

public abstract class ItemSO : ScriptableObject, IItem
{
    [field: SerializeField]
    public string Name { get; set; }

    public Sprite icon;
    public ItemType type;
    private float _amount;

    public float Amount => _amount;
    public string Description => throw new System.NotImplementedException();
}

public enum ItemType
{
    Herb,
    Potion
}