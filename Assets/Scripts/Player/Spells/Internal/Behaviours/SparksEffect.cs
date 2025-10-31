using System.Collections;
using UnityEngine;

public class SparksEffect : Bullet
{
    private float _timer;

    private void OnEnable()
    {
        _timer = 0f;
        ReturnAfterLifeTime();
    }

    private void ReturnAfterLifeTime()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime) SparksFactory.Instance.ReturnSparks(this);
    }
}
