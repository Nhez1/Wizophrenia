using UnityEngine;

public class ExpansiveWaveFactory : MonoBehaviour
{
    public static ExpansiveWaveFactory Instance { get; private set; }

    [SerializeField] private ExpansiveWave _expansiveWavePrefab;
    Pool<ExpansiveWave> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new Pool<ExpansiveWave>(CreateObject, TurnOn, TurnOff, 5, transform);
    }

    ExpansiveWave CreateObject()
    {
        var result = Instantiate(_expansiveWavePrefab);
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