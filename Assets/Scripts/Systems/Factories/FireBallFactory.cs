using UnityEngine;

public class FireBallFactory : MonoBehaviour
{
    public static FireBallFactory Instance { get; private set; }

    [SerializeField] private FireballProjectile _fireBallPrefab;
    ObjectPool<FireballProjectile> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<FireballProjectile>(CreateObject, TurnOn, TurnOff, 5, transform);
    }

    FireballProjectile CreateObject()
    {
        var result = Instantiate(_fireBallPrefab);
        return result;
    }

    void TurnOn(FireballProjectile f) => f.gameObject.SetActive(true);
    void TurnOff(FireballProjectile f) => f.gameObject.SetActive(false);

    public FireballProjectile GetFireBall()
    {
        return _pool.GetObject();
    }

    public void ReturnFireBall(FireballProjectile f) => _pool.ReturnObjectToPool(f);
}