using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class IceWalker : MonoBehaviour, IDamageable, IKnockbackable
{
    public static event Action<bool> OnIceArea;

    public float maxHP;
    public Life _life;

    [Header("Aura Settings")]
    public float auraRadius = 5f;
    public float damagePerSecond = 3f;

    [Header("Movement Settings")]
    public float moveRadius = 5f;
    public float moveSpeed = 3f;

    private bool playerInside = false;
    private Player player;

    private NavMeshAgent agent;
    private Vector3 startPos;

    public Life Life => _life;

    void Start()
    {
        _life = new(false, maxHP);

        // Configurar aura
        SphereCollider aura = gameObject.AddComponent<SphereCollider>();
        aura.isTrigger = true;
        aura.radius = auraRadius;

        // Guardar posición inicial
        startPos = transform.position;

        // Configurar NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.stoppingDistance = 0.1f;

            if (agent.isOnNavMesh)
                SetNewDestination();
        }
        else
        {
            Debug.LogWarning("IceWalker requiere un NavMeshAgent en el GameObject.");
        }
    }

    void Update()
    {
        // Deambulación
        if (agent != null && agent.isOnNavMesh)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                SetNewDestination();
        }

        // Aplicar daño al jugador mientras está en el aura
        if (playerInside && player != null)
        {
            player.Life.Damage(damagePerSecond * Time.deltaTime);
        }
    }

    void SetNewDestination()
    {
        if (agent == null || !agent.isOnNavMesh) return;

        Vector2 randomCircle = Random.insideUnitCircle * moveRadius;
        Vector3 target = startPos + new Vector3(randomCircle.x, 0, randomCircle.y);
        agent.SetDestination(target);
    }

    void OnTriggerEnter(Collider other)
{
    Player p = other.GetComponent<Player>();
    if (p != null)
    {
        playerInside = true;
        player = p;
        OnIceArea?.Invoke(true);

        // Mostrar mensaje temporal en pantalla
        DialogueData data = new DialogueData();
        data.lines = new string[] { "Te estás congelando..." };
        DialogueManager.Instance.StartDialogue(data);
    }
}

    void OnTriggerExit(Collider other)
{
    Player p = other.GetComponent<Player>();
    if (p != null && p == player)
    {
        playerInside = false;
        player = null;

        // Desactiva el texto
        DialogueManager.Instance.ClearDialogueText();

        // Oculta también el panel o fondo helado
        DialogueManager.Instance.dialoguePanel.SetActive(false);

        OnIceArea?.Invoke(false);
    }
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
