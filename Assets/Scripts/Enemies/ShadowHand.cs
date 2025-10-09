using UnityEngine;
using System;

// Por Jere

public class ShadowHand : MonoBehaviour, IDamageable
{
    //Ghost Turn Off Lights
    public static event Action ForceFlameOff;

    public float maxHP = 50f;
    [SerializeField] private Life _life;
    [SerializeField] private float _dmg = 10f;

    public Transform target;  //Aca colocar la mano desde el inspector (usar firepoint de ser necesario)
    public float speed = 1.5f;
    public float stopDistance = 0.3f;
    bool isFollowing;

    public Life Life => _life;

    private void OnEnable()
    {
        FlameEffectSO.OnFlameSwitchOff += OnFlameOff;
        FlameEffectSO.OnFlameSwitchOn += OnFlameOn;
    }

    private void OnDisable()
    {
        FlameEffectSO.OnFlameSwitchOff -= OnFlameOff;
        FlameEffectSO.OnFlameSwitchOn -= OnFlameOn;
    }

    private void Start()
    {
        _life = new(false, maxHP, gameObject);
    }

    void Update()
    {
        if (target == null) return;

        // Mover directo hacia la mano
        if(isFollowing) Move();

        // Revisar distancia
        CheckDistance();
    }

    void OnFlameOff() => isFollowing = false;
    void OnFlameOn() => isFollowing = true;

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void CheckDistance()
    {
        float dist = Vector3.Distance(transform.position, target.position);
        if (isFollowing && dist <= stopDistance)
        {
            ForceFlameOff?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isFollowing && other.gameObject.TryGetComponent<IDamageable>(out var player))
            {
                player.Life.Damage(_dmg);
            }
        }
    }
}
