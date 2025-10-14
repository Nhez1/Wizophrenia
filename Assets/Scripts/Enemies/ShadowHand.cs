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

    public Player player;  //Aca colocar la mano desde el inspector (usar firepoint de ser necesario)
    private Vector3 _targetPos;
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
        if (player == null) return;
        _targetPos = player.transform.position;
        // Se iguala la Y del targetPos a 0 para que se mantenga pegado al piso.
        _targetPos.y = .1f;

        // Mover directo hacia la mano
        if (isFollowing) Move();

        // Revisar distancia
        CheckDistance();
    }

    void OnFlameOff() => isFollowing = false;
    void OnFlameOn() => isFollowing = true;

    void Move() => transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

    void CheckDistance()
    {
        float dist = Vector3.Distance(transform.position, _targetPos);
        if (isFollowing && dist <= stopDistance) StealFlame();
    }

    void StealFlame()
    {
        ForceFlameOff?.Invoke();
        player.Life.Damage(_dmg);
        Destroy(gameObject);
    }
}
