using UnityEngine;

public class EyeDeer : MonoBehaviour
{
    private Transform _player; //esta en private porque va a reconocer al player a traves del tag
    public float maxHP;
    public float speed = 1.5f;
    public float stopDistance = 5f;

    private EnemyProximityAnimator _proximityBehaviour;

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
            if (distance > stopDistance) Move();
        }

        _proximityBehaviour.OnUpdate();
    }

    void Move()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;
    }

    bool LookedAt()
    {
        Vector3 toEnemy = (transform.position - _player.position).normalized;
        float dot = Vector3.Dot(Camera.main.transform.forward, toEnemy); //ve si el player lo esta viendo

        //si el jugador lo mira se queda quieto, similar al disappearing spirit
        if (dot > 0.7f) return true;
        else return false;
    }
}
