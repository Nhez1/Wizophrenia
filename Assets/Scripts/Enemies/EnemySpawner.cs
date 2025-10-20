using System.Collections;
using UnityEngine;

// Por Jere
public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs y referencias")]
    public GameObject shadowHandPrefab;   
    public GameObject stalkerPrefab;
    public Player playerRef;     

    [Header("Configuracion de spawn")]
    public float spawnDistance = 5f;   
    public float spawnInterval = 3f;   

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnHand();
            //SpawnStalker();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnHand()
    {
        if (shadowHandPrefab == null || playerRef == null) return;

        Vector3 dir = Random.onUnitSphere;
        dir.y = Mathf.Abs(dir.y); // evita spawnear bajo el escenario
        Vector3 spawnPos = playerRef.transform.position + dir.normalized * spawnDistance;
        spawnPos.y = 0.5f;

        GameObject g = Instantiate(shadowHandPrefab, spawnPos, Quaternion.identity);
        ShadowHand shadowHand = g.GetComponent<ShadowHand>();

        if (shadowHand != null) shadowHand.player = playerRef;
    }

    void SpawnStalker()
    {
        if (stalkerPrefab == null || playerRef == null) return;

        Vector3 dir = Random.onUnitSphere;
        dir.y = Mathf.Abs(dir.y);
        Vector3 spawnPos = playerRef.transform.position + dir.normalized * spawnDistance;

        GameObject s = Instantiate(stalkerPrefab, spawnPos, Quaternion.identity);
        _ = s.GetComponent<EyeDeer>();

        Debug.Log("Stalker spawneado en " + spawnPos);
    }
}
