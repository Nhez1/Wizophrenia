using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EyeDeer : MonoBehaviour
{
    public float maxHP;
    private Transform _player; //esta en private porque va a reconocer al player a traves del tag
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _stopDistance = 5f;

    private EnemyProximityAnimator _proximityBehaviour;

    [SerializeField] private Volume _vignette;

    void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        _proximityBehaviour = new(GetComponent<Animator>(), _player, transform);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        //si no lo mira, se mueve

        if (LookedAt()) return;
        else
        {
            if (distance > _stopDistance) Move();
        }

        _proximityBehaviour.OnUpdate();

        if (Input.GetKeyDown(KeyCode.U)) Debug.Log("Turn on Vignette");
    }

    void Move()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += _speed * Time.deltaTime * direction;
    }

    bool LookedAt()
    {
        Vector3 toEnemy = (transform.position - _player.position).normalized;
        float dot = Vector3.Dot(Camera.main.transform.forward, toEnemy); //ve si el player lo esta viendo

        //si el jugador lo mira se queda quieto, similar al disappearing spirit
        if (dot > .2f) return true;
        else return false;
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
            yield return new WaitForSeconds(1);
        }
    }
}
