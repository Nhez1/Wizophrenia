using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SkeletonEnemy : MonoBehaviour, IDamageable, IKnockbackable
{
    [Header("Stats")]
    public float maxHP = 30f;
    public float speed = 2f;
    public float stopDistance = 2f;
    public float gravity = -9.8f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;

    private float lastAttackTime;
    [SerializeField] private float _detectArea = 8f;

    [Header("References")]
    [SerializeField] private Life _life;
    private Transform player;
    private CharacterController controller;
    private Animator _anim;

    private Vector3 knockbackVelocity;
    private float knockbackTime;

    public Life Life => _life;

    void Start()
    {
        _anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        _life = new Life(false, maxHP, gameObject);
    }

    void Update()
    {
        if (player == null || _life.HP <= 0) return;

        var disToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (disToPlayer <= _detectArea)
        {
            HandleMovement();
            HandleKnockback();
            TryAttack();
            _anim.SetBool("IsFollowing", true);
        }

            _anim.SetBool("IsFollowing", false);
    }

    void HandleMovement()
    {
        if (knockbackTime > 0f) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 moveDir = (player.position - transform.position).normalized;
        moveDir.y = 0f;
        transform.rotation = Quaternion.LookRotation(moveDir);

        if (distance > stopDistance)
            controller.Move(speed * Time.deltaTime * moveDir);

        controller.Move(gravity * Time.deltaTime * Vector3.up);
    }

    void TryAttack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log($"{name} ataca al jugador e inflige {attackDamage} daño");

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.Life.Damage(attackDamage);
            Debug.Log($" Player recibió {attackDamage} daño (vida restante: {playerScript.Life.HP})");
        }
        else
        {
            Debug.LogWarning(" No se encontró el componente Player en el objeto del jugador.");
        }
    }


    public void Damage(float amount)
    {
        _life.Damage(amount);
        Debug.Log($"{name} recibió {amount} daño. Vida restante: {_life.HP}");
    }

    public void Knockback(Vector3 source, float force, float duration)
    {
        Vector3 dir = (transform.position - source).normalized;
        dir.y = 0.2f;
        knockbackVelocity = dir * force;
        knockbackTime = duration;
    }

    void HandleKnockback()
    {
        if (knockbackTime > 0f)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackTime -= Time.deltaTime;
        }
    }
}
