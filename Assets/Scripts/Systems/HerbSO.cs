using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Herb")]
public class HerbSO : ItemSO
{
    public List<float> healthModifiers = new();
    public List<float> manaModifiers = new();
    public List<float> sanityModifiers = new();
}
