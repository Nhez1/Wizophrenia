using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour, ISpell
{
    bool isActive;
    
    public float ManaCost { get; }

    IEnumerator PassiveManaSpend()
    {
        yield return null;
        if (isActive)
        {
            yield return new WaitForSeconds(1);
            //mana -= 1;

        }
    }
}
