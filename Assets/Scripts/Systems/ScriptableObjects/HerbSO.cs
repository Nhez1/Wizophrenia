using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Herb")]
public class HerbSO : ItemSO
{
    public float healthModifier;
    public float manaModifier;
    public float sanityModifier;
}
