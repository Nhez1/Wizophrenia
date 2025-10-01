using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFlyingEnemy : MonoBehaviour, IDamageable, IKnockbackable
{
    public float maxHP;
    [SerializeField] private Life _life;
    Transform player; //esta en private porque va a reconocer al player a traves del tag
    public float speed = 1.5f;
    public float stopDistance = 5f;
    public Life Life => _life;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        _life = new(false, maxHP, gameObject);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);  //si no lo mira, se mueve
       
        if (distance > stopDistance) Move();

        transform.LookAt(player); //siempre mira al player
    }

    void Move()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;
    }

    public void Knockback(Vector3 source, float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(source, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector3 source, float force, float duration)
    {
        Vector3 knockDir = (transform.position - source).normalized;
        float time = 0f;

        while (time < duration)
        {
            transform.position += (force / duration) * Time.deltaTime * knockDir;
            time += Time.deltaTime;
            yield return null;
        }
    }
}
