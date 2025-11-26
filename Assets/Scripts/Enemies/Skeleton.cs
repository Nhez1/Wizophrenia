using System.Collections;
using UnityEngine;

public class Skeleton : MonoBehaviour, IDamageable, IKnockbackable
{
    [Header(" Passive Stats ")]
    [SerializeField] private float _maxHP = 75f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Life _life;
    private bool _canMove = true;
    public Life Life => _life;

    [Header(" Combat Stats ")]
    [SerializeField] private float _detectArea = 8f;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackCooldown = 1f;
    private bool _canAttack = true;

    private Animator _anim;

    private void Awake()
    {
        _life = new(false, _maxHP, gameObject);
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (DetectPlayer(out var tgt) && _canMove)
        {
            var disToPlayer = Vector3.Distance(transform.position, tgt.transform.position);

            if(disToPlayer > _attackRange) FollowPlayer(tgt);
            else if (disToPlayer <= _attackRange && _canAttack) Attack(tgt.Life);
        }
        else _anim.SetBool("IsFollowing", false);
    }

    void Attack(Life target)
    {
        target.Damage(_damage);
        StartCoroutine(Cooldown());
    }

    public bool DetectPlayer(out Player target)
    {
        Collider[] detectionArea = Physics.OverlapSphere(transform.position, _detectArea);
        foreach (var detected in detectionArea)
        {
            // Esto es una especie de doble return, devuelve Verdadero de que encontró un target y el Player.cs del Target.
            if (detected.TryGetComponent(out target)) return true;
        }

        target = null;
        return false;
    }
    private void FollowPlayer(Player target)
    {
        _anim.SetBool("IsFollowing", true);
        Vector3 dir = (target.transform.position - transform.position).normalized;

        transform.position += _speed * Time.deltaTime * dir;
    }

    public void Knockback(Vector3 source, float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(source, force, duration));
    }
    IEnumerator KnockbackRoutine(Vector3 source, float force, float duration)
    {
        Vector3 knockDir = (transform.position - source).normalized;
        knockDir.y = transform.position.y;
        // Acá seteo la Y de knockDir a la actual porque por alguna razón en la Y se pasa cualquier cosa y sin esto el enemigo clippea por el piso y se va a la mierda
        float time = 0f;

        while (time < duration)
        {
            _canMove = false;
            transform.position += force / duration * Time.deltaTime * knockDir;
            time += Time.deltaTime;
            yield return null;
        }

        _canMove = true;
    }

    IEnumerator Cooldown()
    {
        _canAttack = false;

        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _detectArea);
    }
}