using System.Collections;
using UnityEngine;

public class FireballProjectile : Bullet
{
    [Header("Fireball Settings")]
    [SerializeField] private float _fireballSpeed = 5f;
    [SerializeField] private float _knockBackForce = 5f;
    [SerializeField] private float _knockBackTime = .2f;
    private float _timer;
    [field: SerializeField] public float Dmg { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioClip _launchSound;
    [SerializeField] private AudioClip _impactSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _timer = 0f;

        // Agregamos o usamos un AudioSource local
        if (!TryGetComponent(out _audioSource)) _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1f; // 3D
        _audioSource.volume = 0.8f;

        // Sonido de lanzamiento
        if (_launchSound != null)
            _audioSource.PlayOneShot(_launchSound);
    }

    void Update()
    {
        Move();
        ReturnAfterLifeTime();
    }

    void Move() => transform.Translate(_fireballSpeed * Time.deltaTime * Vector3.forward);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
            {
                DealDamage(enemy.Life);
                if (collision.gameObject.TryGetComponent<IKnockbackable>(out var knockbackable))
                    knockbackable.Knockback(transform.position, _knockBackForce, _knockBackTime);
            }
        }

        // Sonido de impacto y retorno inmediato al pool
        OnImpact();
    }

    private void ReturnAfterLifeTime()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime) OnImpact();
    }

    private void OnImpact()
    {
        // Evita múltiples impactos
        if (!gameObject.activeSelf) return;

        // Reproduce sonido de impacto
        if (_impactSound != null)
            AudioSource.PlayClipAtPoint(_impactSound, transform.position, 0.8f);

        // Partículas de impacto
        var sparks = SparksFactory.Instance.GetSparks();
        sparks.transform.position = transform.position;

        // Devuelve al pool inmediatamente
        FireBallFactory.Instance.ReturnFireBall(this);
    }

    void DealDamage(Life target) => target.Damage(Dmg);
}
