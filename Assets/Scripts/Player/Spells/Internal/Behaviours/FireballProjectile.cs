using System.Collections;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float fireballSpeed = 5f;
    public float fireballLifeTime = 8f;
    public float impactEffectLifeTime = 2f;
    public GameObject ImpactEffect;
    Vector3 spawnPos;

    [Tooltip("The amount of force it will apply on to the object")]
    public float knockBackForce;
    [Tooltip("For how much time the enemy will be knocked back")]
    public float knockBackTime;

    void Start()
    {
        spawnPos = transform.position;
        StartCoroutine(ReturnToPoolAfterLifeTime());
    }


    void Update()
    {
        transform.Translate(fireballSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
            {
                DealDamage(enemy.Life);
                if (collision.gameObject.TryGetComponent<IKnockbackable>(out var knockbackable)) knockbackable.Knockback(spawnPos, knockBackForce, knockBackTime);
            }
        }
        else if (collision.gameObject.CompareTag("Player")) return;

        OnImpact();
    }

    private IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(fireballLifeTime);
        OnImpact();
    }

    private void OnImpact()
    {
        //Returns FireBall to item pool
        FireBallFactory.Instance.ReturnFireBall(this); 
        //Spawns Impact particles
        Destroy(Instantiate(ImpactEffect, transform.position, Quaternion.identity), impactEffectLifeTime); 
    }

    void DealDamage(Life enemy) => enemy.Damage(50f);
}
//Marker