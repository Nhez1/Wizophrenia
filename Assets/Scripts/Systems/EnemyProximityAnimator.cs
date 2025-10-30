using UnityEngine;

// DISCLAIMER: MADE BY AI //
[System.Serializable]
public class EnemyProximityAnimator
{
    private bool _isStop = false;
    private Animator _anim;
    private Transform _player;
    private Transform _enemy;
    private string _stateName = "Approach";
    [Tooltip("The maximum distance. This will be where the animation will START")]
    [SerializeField] private float _maxAnimDistance = 20f; // Beyond this, animation = start
    [Tooltip("The minimum distance. This will be where the animation will END")]
    [SerializeField] private float _minAnimDistance = 5f; // This close = end of animation

    public EnemyProximityAnimator(Animator a, Transform p, Transform e)
    {
        _anim = a;
        _player = p;
        _enemy = e;
    }

    public void OnUpdate()
    {
        if (!_isStop)
        {
            float dist = Vector3.Distance(_player.position, _enemy.position);
            float t = Mathf.InverseLerp(_minAnimDistance, _maxAnimDistance, dist);
            t = Mathf.Clamp01(1f - t);

            _anim.Play(_stateName, 0, t);
            _anim.speed = 0;
        }
    }

    public void StopBehaviour() => _isStop = true;
}
