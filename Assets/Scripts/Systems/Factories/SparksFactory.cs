using UnityEngine;

public class SparksFactory : MonoBehaviour
{
    public static SparksFactory Instance { get; private set; }

    [SerializeField] private SparksEffect _expansiveWavePrefab;
    ObjectPool<SparksEffect> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<SparksEffect>(CreateObject, TurnOn, TurnOff, 5, transform);
    }

    SparksEffect CreateObject()
    {
        var result = Instantiate(_expansiveWavePrefab);
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
