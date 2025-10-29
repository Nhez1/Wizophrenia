using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EyeDeer : MonoBehaviour
{
    [Header("Stats")]
    public float maxHP = 100f;

    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _stopDistance = 5f;

    private Transform _player;
    private EnemyProximityAnimator _proximityBehaviour;

    [Header("Effects")]
    [SerializeField] private Volume _vignette;

    private bool _isDying = false;

    void Start()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;

        _proximityBehaviour = new EnemyProximityAnimator(GetComponent<Animator>(), _player, transform);
    }

    void Update()
    {
        if (_isDying) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (!LookedAt() && distance > _stopDistance)
            Move();

        _proximityBehaviour.OnUpdate();

        if (Input.GetKeyDown(KeyCode.U))
            Debug.Log("Turn on Vignette");
    }

    void Move()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += _speed * Time.deltaTime * direction;
    }

    bool LookedAt()
    {
        Vector3 toEnemy = (transform.position - _player.position).normalized;
        float dot = Vector3.Dot(Camera.main.transform.forward, toEnemy);

        return dot > 0.2f;
    }

    void DrainSanity()
    {
        var s = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Sanity;
    }

    IEnumerator DrainSanityOverTime(Sanity playerSanity)
    {
        while (Vector3.Distance(transform.position, _player.position) <= _stopDistance)
        {
            playerSanity.Reduce(2);
            yield return new WaitForSeconds(1f);
        }
    }

    // --- Este método lo llama el ExpansiveWave ---
    public void HitByExpansiveWave()
    {
        if (!_isDying)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        _isDying = true;
        _speed = 0;

        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Die");

        // Espera la duración de la animación
        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }

    // --- Opcional: para debug, ver colisiones con la onda ---
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ExpansiveWave>(out var wave))
        {
            HitByExpansiveWave();
        }
    }
}
