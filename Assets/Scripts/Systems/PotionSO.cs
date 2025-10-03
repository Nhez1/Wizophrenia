using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Potion")]
public class PotionSO : ItemSO
{
    public float healthModifier;
    public float manaModifiers;
    public float sanityModifiers;

    public void Consume(PotionContext potionContext)
    {

    }
}

public class PotionContext
{
    public Life life;
    public Mana mana;
    //Acá también se va a pedir el script de Sanidad, cuando esté hecho.

    public PotionContext(Life L, Mana M)
    {
        life = L;
        mana = M;
    }
}
