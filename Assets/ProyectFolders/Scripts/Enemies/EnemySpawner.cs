using System.Collections;
using UnityEngine;

// Por Jere
public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs y referencias")]
    [SerializeField] private GameObject _shadowHandPrefab;
    [SerializeField] private Player _playerRef;

    [Header("Configuracion de spawn")]
    [SerializeField] private float _spawnDistance = 20f;   
    [SerializeField] private float _spawnInterval = 60f;   

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnHand();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    void SpawnHand()
    {
        if (_shadowHandPrefab == null || _playerRef == null) return;

        Vector3 dir = Random.insideUnitCircle;
        Vector3 spawnPos = _playerRef.transform.position + dir.normalized * _spawnDistance;
        spawnPos.y = -0.1f;

        GameObject g = Instantiate(_shadowHandPrefab, spawnPos, _shadowHandPrefab.transform.rotation);

        if(g.TryGetComponent<ShadowHand>(out var shadowHand)) shadowHand.player = _playerRef;
    }

    //void SpawnStalker()
    //{
    //    if (stalkerPrefab == null || playerRef == null) return;

    //    Vector3 dir = Random.onUnitSphere;
    //    dir.y = Mathf.Abs(dir.y);
    //    Vector3 spawnPos = playerRef.transform.position + dir.normalized * spawnDistance;

    //    GameObject s = Instantiate(stalkerPrefab, spawnPos, Quaternion.identity);
    //    _ = s.GetComponent<EyeDeer>();

    //    Debug.Log("Stalker spawneado en " + spawnPos);
    //}
}
