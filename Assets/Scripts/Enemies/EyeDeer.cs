using System.Collections;
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

    void Start()
    {
        if(Application.isPlaying) _vignette.profile.TryGet(out _visualEffect);
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
        
        if (distance <= _stopDistance && !_isHidden) DrainSanity();
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
        //var s = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Sanity;
        StartCoroutine(DrainSanityOverTime(null));
    }

    IEnumerator DrainSanityOverTime(Sanity playerSanity)
    {
        while (Vector3.Distance(transform.position, _player.position) <= _stopDistance)
        {
            //playerSanity.Reduce(2);
            yield return new WaitForSeconds(1f);
        }

        _isDraining = false;
        EnableVisualEffect(false);
    }

    void EnableVisualEffect(bool on)
    {
        if (_visualEffect != null) _visualEffect.intensity.value = on ? 1f : 0f;
    }
    #endregion

    public void GetBlindedByFlame()
    {
        _proximityBehaviour.StopBehaviour();
        _anim.speed = 1;
        _anim.Play("Hide");
        EnableVisualEffect(false);
        _isHidden = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ExpansiveWave>())
        {
            GetBlindedByFlame();
        }
    }
}
