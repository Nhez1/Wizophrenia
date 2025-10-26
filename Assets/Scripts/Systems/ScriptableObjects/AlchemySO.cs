using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemySO : ScriptableObject
{
    public PotionSO potionResult;

    public void Mix(HerbSO firstHerb, HerbSO secondHerb)
    {
        potionResult.healthModifier = firstHerb.healthModifier + secondHerb.healthModifier;
        potionResult.manaModifier = firstHerb.manaModifier + secondHerb.manaModifier;
        potionResult.sanityModifier = firstHerb.sanityModifier + secondHerb.sanityModifier;
    }
}