using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksEffect : Bullet
{
    void Start()
    {
        lifeTime = 2f;
    }

    protected override IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        SparksFactory.Instance.ReturnSparks(this);
    }
}
