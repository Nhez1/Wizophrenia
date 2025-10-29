using System.Collections;
using UnityEngine;

public class ExpansiveWave : Bullet
{
    void Start()
    {
        StartCoroutine(ReturnToPoolAfterLifeTime());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("GhostEnemy"))
        {
            if (collision.TryGetComponent<IDamageable>(out var opp)) DealDamage(opp.Life);
        }
    }

    void DealDamage(Life enemy) => enemy.Damage(100f);

    protected override IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        OnImpact();
    }

    private void OnImpact()
    {
        //Returns FireBall to item pool
        ExpansiveWaveFactory.Instance.ReturnExpansiveWave(this);
    }
}
//Marker