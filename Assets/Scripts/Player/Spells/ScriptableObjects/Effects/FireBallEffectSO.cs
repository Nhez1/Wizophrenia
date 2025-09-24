using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEffectSO : EffectSO
{
    public override void OnCast(GameObject prefab, Transform s)
    {
        Instantiate(prefab);
    }
}
