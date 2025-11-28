using UnityEngine;

public class ShadowHandFactory : MonoBehaviour
{
    public static ShadowHandFactory Instance { get; private set; }

    [SerializeField] private int _instancesAmount;
    [SerializeField] private ShadowHand _shadowHandPrefab;
    ObjectPool<ShadowHand> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<ShadowHand>(CreateObject, TurnOn, TurnOff, _instancesAmount, transform);
    }

    ShadowHand CreateObject()
    {
        var result = Instantiate(_shadowHandPrefab);
        result.transform.parent = transform;
        return result;
    }

    void TurnOn(ShadowHand f) => f.gameObject.SetActive(true);
    void TurnOff(ShadowHand f) => f.gameObject.SetActive(false);

    public ShadowHand GetShadowHand()
    {
        return _pool.GetObject();
    }

    public void ReturnShadowHand(ShadowHand f)
    {
        _pool.ReturnObjectToPool(f);
    }
}

