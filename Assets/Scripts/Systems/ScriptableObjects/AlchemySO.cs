using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemySO : ScriptableObject
{
    public PotionSO potionResult;

    public HerbSO firstHerb;
    public HerbSO secondHerb;

    public void Mix()
    {
        potionResult.healthModifier = firstHerb.healthModifier + secondHerb.healthModifier;
        potionResult.manaModifier = firstHerb.manaModifier + secondHerb.manaModifier;
    }


}