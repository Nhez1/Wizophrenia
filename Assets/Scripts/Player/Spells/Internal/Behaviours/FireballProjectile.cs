using System.Collections;
using UnityEngine;

public class FireballProjectile : Bullet
{
    [Header("Fireball Settings")]
    public float fireballSpeed = 5f;
    public float knockBackForce;
    public float knockBackTime;
    [field: SerializeField] public float Dmg { get; private set; }

    [Header("Audio")]
    public AudioClip launchSound;
    public AudioClip impactSound;

    Vector3 _spawnPos;
    AudioSource _audioSource;

    void Start()
    {
        _spawnPos = transform.position;

        // Agregamos o usamos un AudioSource local
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1f; // 3D
        _audioSource.volume = 0.8f;

        // Sonido de lanzamiento
        if (launchSound != null)
            _audioSource.PlayOneShot(launchSound);

        StartCoroutine(ReturnToPoolAfterLifeTime());
    }

    void Update()
    {
        Move();
    }

    void Move() => transform.Translate(fireballSpeed * Time.deltaTime * Vector3.forward);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
            {
                DealDamage(enemy.Life);
                if (collision.gameObject.TryGetComponent<IKnockbackable>(out var knockbackable))
                    knockbackable.Knockback(_spawnPos, knockBackForce, knockBackTime);
            }
        }

        // Sonido de impacto y retorno inmediato al pool
        OnImpact();
    }

    protected override IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        OnImpact();
    }

    private void OnImpact()
    {
        // Evita múltiples impactos
        if (!gameObject.activeSelf) return;

        // Reproduce sonido de impacto
        if (impactSound != null)
            AudioSource.PlayClipAtPoint(impactSound, transform.position, 0.8f);

        // Partículas de impacto
        var sparks = SparksFactory.Instance.GetSparks();
        sparks.transform.position = transform.position;

        // Devuelve al pool inmediatamente
        FireBallFactory.Instance.ReturnFireBall(this);
    }

    void DealDamage(Life enemy) => enemy.Damage(Dmg);
}
