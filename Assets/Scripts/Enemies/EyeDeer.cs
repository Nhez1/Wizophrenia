using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EyeDeer : MonoBehaviour
{
    [Header("Stats")]
    public float maxHP = 100f;
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _stopDistance = 5f;

    private Animator _anim;
    private Transform _player;
    private EnemyProximityAnimator _proximityBehaviour;

    [Header("Effects")]
    [SerializeField] private Volume _vignette;
    private VignetteEffect _visualEffect;
    private bool _isDraining = false;
    private bool _isHidden = false;

    [Header("Respawn Settings")]
    [SerializeField] private List<GameObject> respawnNodes = new List<GameObject>();
    [SerializeField] private float respawnDelay = 20f;
    [SerializeField] private float minDistanceFromPlayer = 15f;

    void Start()
    {
        if (Application.isPlaying)
            _vignette.profile.TryGet(out _visualEffect);

        _anim = GetComponent<Animator>();

        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;

        _proximityBehaviour = new(_anim, _player, transform);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if (!LookedAt() && distance > _stopDistance && !_isHidden)
        {
            Move();
            _proximityBehaviour.OnUpdate();
        }

        if (distance <= _stopDistance && !_isHidden)
            DrainSanity();
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

    #region DrainSanityBehaviour
    void DrainSanity()
    {
        if (_isDraining) return;
        _isDraining = true;

        EnableVisualEffect(true);
        StartCoroutine(DrainSanityOverTime(null));
    }

    IEnumerator DrainSanityOverTime(Sanity playerSanity)
    {
        while (Vector3.Distance(transform.position, _player.position) <= _stopDistance)
        {
            yield return new WaitForSeconds(1f);
        }

        _isDraining = false;
        EnableVisualEffect(false);
    }

    void EnableVisualEffect(bool on)
    {
        if (_visualEffect != null)
            _visualEffect.intensity.value = on ? 1f : 0f;
    }
    #endregion

    public void GetBlindedByFlame()
    {
        _proximityBehaviour.StopBehaviour();
        _anim.speed = 1;
        _anim.Play("Hide");
        EnableVisualEffect(false);
        _isHidden = true;

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        GameObject chosenNode = GetRandomRespawnNode();

        if (chosenNode != null)
        {
            transform.position = chosenNode.transform.position;
        }

        _anim.Play("Idle");
        _isHidden = false;
    }

    GameObject GetRandomRespawnNode()
    {
        if (respawnNodes.Count == 0) return null;

        List<GameObject> validNodes = new List<GameObject>();

        foreach (var node in respawnNodes)
        {
            if (node == null) continue;
            float distance = Vector3.Distance(node.transform.position, _player.position);

            Vector3 dirToNode = (node.transform.position - _player.position).normalized;
            float dot = Vector3.Dot(Camera.main.transform.forward, dirToNode);

            if (distance > minDistanceFromPlayer && dot < 0.2f)
                validNodes.Add(node);
        }

        if (validNodes.Count == 0)
            return respawnNodes[Random.Range(0, respawnNodes.Count)];

        return validNodes[Random.Range(0, validNodes.Count)];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ExpansiveWave>())
        {
            GetBlindedByFlame();
        }
    }
}
