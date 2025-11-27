using System.Collections;
using UnityEngine;

//TP2 Gomez Villarruel Jeremias

public class ExpansiveWave : Bullet
{
    private float _timer;

    private void OnEnable() => _timer = 0f;

    private void Update() => ReturnAfterLifeTime();

    private void ReturnAfterLifeTime()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime) OnImpact();
    }

    private void OnImpact()
    {
        //Returns FireBall to item pool
        ExpansiveWaveFactory.Instance.ReturnExpansiveWave(this);
    }

    protected override void OnDespawn()
{
    ExpansiveWaveFactory.Instance.ReturnExpansiveWave(this);
}

}