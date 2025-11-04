using UnityEngine;

public class SparksFactory : MonoBehaviour
{
    public static SparksFactory Instance { get; private set; }

    [SerializeField] private int _instancesAmount;
    [SerializeField] private SparksEffect _sparksPrefab;
    ObjectPool<SparksEffect> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<SparksEffect>(CreateObject, TurnOn, TurnOff, _instancesAmount, transform);
    }

    SparksEffect CreateObject()
    {
        var result = Instantiate(_sparksPrefab);
        result.transform.parent = transform;
        return result;
    }

    void TurnOn(SparksEffect f) => f.gameObject.SetActive(true);
    void TurnOff(SparksEffect f) => f.gameObject.SetActive(false);

    public SparksEffect GetSparks()
    {
        return _pool.GetObject();
    }

    public void ReturnSparks(SparksEffect f)
    {
        _pool.ReturnObjectToPool(f);
    }
}
