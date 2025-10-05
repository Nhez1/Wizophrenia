using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Herb")]
public class HerbSO : ItemSO
{
    [field: SerializeField]
    public string HerbName { get; }

    public float healthModifier;
    public float manaModifier;
    public float sanityModifier;
}
