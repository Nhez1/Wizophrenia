using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    float manaCost;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpendMana()
    {
        yield return null;
        if (isActive)
        {
            yield return new WaitForSeconds(1);
            //mana -= 1;

        }
    }
}
