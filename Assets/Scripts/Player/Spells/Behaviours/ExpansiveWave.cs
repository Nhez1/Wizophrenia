using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansiveWave : MonoBehaviour
{
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided ");
        if (collision.gameObject.CompareTag("GhostEnemy"))
        {
            if (collision.TryGetComponent<IDamageable>(out var opp)) DealDamage(opp.Life);
        }
    }

    void DealDamage(Life enemy) => enemy.TakeDamage(100f);
}
//Marker