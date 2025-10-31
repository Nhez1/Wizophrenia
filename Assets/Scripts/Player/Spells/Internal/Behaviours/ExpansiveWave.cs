using System.Collections;
using UnityEngine;

public class ExpansiveWave : Bullet
{
    private float _timer;

    private void OnEnable()
    {
        _timer = 0f;
        ReturnAfterLifeTime();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("GhostEnemy"))
        {
            if (collision.TryGetComponent<IDamageable>(out var opp)) DealDamage(opp.Life);
        }
    }

    void DealDamage(Life enemy) => enemy.Damage(100f);

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
}
//Marker