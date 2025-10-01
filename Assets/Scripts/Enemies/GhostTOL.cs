using UnityEngine;
using System;

// Por Jere

public class GhostTOL : MonoBehaviour, IDamageable
{
    //Ghost Turn Off Lights
    public static event Action ForceFlameOff;

    public float maxHP = 50f;
    [SerializeField] private Life _life;
    [SerializeField] private float _dmg = 10f;

    public Transform target;  //Aca colocar la mano desde el inspector (usar firepoint de ser necesario)
    public float speed = 1.5f;
    public float stopDistance = 0.3f;

    public Life Life => _life;

    private void Start()
    {
        _life = new(false, maxHP, gameObject);
    }

    void Update()
    {
        if (target == null) return;

        // Mover directo hacia la mano
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Revisar distancia
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= stopDistance)
        {
            ForceFlameOff?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collision");
            if(other.gameObject.TryGetComponent<IDamageable>(out var player))
            {
                player.Life.TakeDamage(_dmg);
            }
        }
    }
}
