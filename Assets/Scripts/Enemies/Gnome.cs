using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Gnome : MonoBehaviour, IDamageable, IKnockbackable
{
    private Life _life;

    [Header("Stats")]
    public float maxHP = 50f;
    public float damage = 15f;
    public float visionRange = 5f;       // Distancia para detectar al jugador
    public float activationDelay = 3f;   // Tiempo que debe verte antes de moverse
    public float moveSpeed = 3f;         // Velocidad de acercamiento

    private Transform playerTransform;
    private Player playerScript;
    private NavMeshAgent agent;

    private bool isChasing = false;
    private bool hasKicked = false;

    public Life Life => _life;

    void Start()
    {
        _life = new(false, maxHP);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            playerScript = playerObj.GetComponent<Player>();
        }

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.isStopped = true;   // comienza quieto
        agent.stoppingDistance = 0.5f;

        // Configurar Rigidbody y Collider para triggers
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        // Asegurar que el gnomo esté sobre NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogWarning("El gnomo no está sobre un NavMesh válido");
        }
    }

    void Update()
    {
        if (playerTransform == null || agent == null || !agent.isOnNavMesh) return;

        if (!isChasing && !hasKicked)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= visionRange)
            {
                StartCoroutine(ActivateAfterDelay());
            }
        }

        if (isChasing && !hasKicked)
        {
            // Moverse hacia el jugador
            agent.SetDestination(playerTransform.position);
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        if (agent.isOnNavMesh)
        {
            isChasing = true;
            agent.isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasKicked) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.Life.TakeDamage(damage);
                Debug.Log("¡Patada del gnomo! Vida del jugador: " + player.Life.HP);
            }
            // Hacer daño

            hasKicked = true;
            agent.isStopped = true;

            // Desaparecer en humo
            StartCoroutine(VanishEffect());
        }
    }

    private IEnumerator VanishEffect()
    {
        // Aquí podés agregar partículas de humo
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    public void Knockback(Vector3 source, float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(source, force, duration));
    }

    IEnumerator KnockbackRoutine(Vector3 source, float force, float duration)
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