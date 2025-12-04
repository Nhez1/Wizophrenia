using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Herb")]
public class HerbSO : ItemSO
{
    [field: SerializeField]
    public string HerbName { get; }

    public float healthModifier;
    public float manaModifier;
    public float sanityModifier;
}
