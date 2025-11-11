using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Alchemy")]
public class AlchemySO : ScriptableObject
{
    public PotionSO Mix(HerbSO firstHerb, HerbSO secondHerb, HerbSO thirdHerb)
    {
        PotionSO potion = CreateInstance<PotionSO>();
        potion.Type = ItemType.Potion;

        potion.healthModifier = firstHerb.healthModifier + secondHerb.healthModifier + thirdHerb.healthModifier;
        potion.manaModifier = firstHerb.manaModifier + secondHerb.manaModifier + thirdHerb.manaModifier;
        potion.sanityModifier = firstHerb.sanityModifier + secondHerb.sanityModifier + thirdHerb.sanityModifier;

        return potion;
    }
}