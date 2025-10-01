using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour, IDamageable, IKnockbackable
{
    public float maxHP;
    [SerializeField] private Life _life;
    Transform player; //esta en private porque va a reconocer al player a traves del tag
    public float speed = 1.5f;
    public float stopDistance = 1f;

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

        if (LookedAt()) return;
        else
        {
            if (distance > stopDistance) Move();
        }

        transform.LookAt(player); //siempre mira al player
    }

    void Move()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;
    }

    bool LookedAt()
    {
        Vector3 toEnemy = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, toEnemy); //ve si el player lo esta viendo

        //si el jugador lo mira se queda quieto, similar al disappearing spirit
        if (dot > 0.7f) return true;
        else return false;
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
