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
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _stopDistance = 1.5f;
    [SerializeField] private bool _isFollowing;

    public Life Life => _life;

    // 🎧 Nuevo: sonido al robar el fuego
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stealSound;

    private void Start()
    {
        _life = new(false, maxHP, gameObject);

        // Si no hay AudioSource asignado, agregar uno al vuelo
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; // 3D sound
        }
    }

    void Update()
    {
        if (player == null) return;
        _targetPos = player.transform.position;
        _targetPos.y = .1f;

        if (_isFollowing) Move();
        CheckDistance();
    }

    public void OnFlameOff() => _isFollowing = false;
    public void OnFlameOn() => _isFollowing = true;

    void Move() => transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);

    void CheckDistance()
    {
        float dist = Vector3.Distance(transform.position, _targetPos);
        if (_isFollowing && dist <= _stopDistance) StealFlame();
    }

    void StealFlame()
    {
        // 🔥 Reproduce el sonido antes de destruirse
        if (stealSound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(stealSound, transform.position);
        }

        ForceFlameOff?.Invoke();
        player.Life.Damage(_dmg);
        Destroy(gameObject);
    }
}
