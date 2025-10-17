using System.Collections;
using UnityEngine;

public class SparksEffect : Bullet
{
    void Start()
    {
        lifeTime = 2f;

        StartCoroutine(ReturnToPoolAfterLifeTime());
    }

    protected override IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        SparksFactory.Instance.ReturnSparks(this);
    }
}
