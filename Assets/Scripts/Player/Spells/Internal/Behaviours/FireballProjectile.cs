using System.Collections;
using UnityEngine;

public class FireballProjectile : Bullet
{
    public float fireballSpeed = 5f;
    Vector3 spawnPos;

    [Tooltip("The amount of force it will apply on to the object")]
    public float knockBackForce;
    [Tooltip("For how much time the enemy will be knocked back")]
    public float knockBackTime;
    [field: SerializeField]
    public float Dmg { get; private set; }

    void Start()
    {
        spawnPos = transform.position;
        StartCoroutine(ReturnToPoolAfterLifeTime());
    }


    void Update()
    {
        Move();
    }

    void Move() => transform.Translate(fireballSpeed * Time.deltaTime * Vector3.forward);

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

    protected override IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        OnImpact();
    }

    private void OnImpact()
    {
        //Returns FireBall to item pool
        FireBallFactory.Instance.ReturnFireBall(this);
        //Spawns Impact particles
        var sparks = SparksFactory.Instance.GetSparks();
        sparks.transform.position = transform.position;
    }

    void DealDamage(Life enemy) => enemy.Damage(Dmg);
}