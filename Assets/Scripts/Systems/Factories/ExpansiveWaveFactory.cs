using UnityEngine;

public class ExpansiveWaveFactory : MonoBehaviour
{
    public static ExpansiveWaveFactory Instance { get; private set; }

    [SerializeField] private int _instancesAmount;
    [SerializeField] private ExpansiveWave _expansiveWavePrefab;
    ObjectPool<ExpansiveWave> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<ExpansiveWave>(CreateObject, TurnOn, TurnOff, _instancesAmount, transform);
    }

    ExpansiveWave CreateObject()
    {
        var result = Instantiate(_expansiveWavePrefab);
        result.transform.parent = transform;
        return result;
    }

    void TurnOn(ExpansiveWave f) => f.gameObject.SetActive(true);
    void TurnOff(ExpansiveWave f) => f.gameObject.SetActive(false);

    public ExpansiveWave GetExpansiveWave()
    {
        return _pool.GetObject();
    }

    public void ReturnExpansiveWave(ExpansiveWave f) => _pool.ReturnObjectToPool(f);
}