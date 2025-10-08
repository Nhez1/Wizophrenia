using System.Collections;
using UnityEngine;

// Por Jere

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs y referencias")]
    public GameObject ghostPrefab;   
    public GameObject stalkerPrefab;
    public Transform playerHand;     

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
            SpawnGhost();
            SpawnStalker();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnGhost()
    {
        if (ghostPrefab == null || playerHand == null) return;

        Vector3 dir = Random.onUnitSphere;
        dir.y = Mathf.Abs(dir.y); // evita spawnear bajo el escenario
        Vector3 spawnPos = playerHand.position + dir.normalized * spawnDistance;

        GameObject g = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
        ShadowHand ghostScript = g.GetComponent<ShadowHand>();

        if (ghostScript != null)
        {
            ghostScript.target = playerHand;
        }

        Debug.Log("Fantasma spawneado en " + spawnPos);
    }

    void SpawnStalker()
    {
        if (stalkerPrefab == null || playerHand == null) return;

        Vector3 dir = Random.onUnitSphere;
        dir.y = Mathf.Abs(dir.y);
        Vector3 spawnPos = playerHand.position + dir.normalized * spawnDistance;

        GameObject s = Instantiate(stalkerPrefab, spawnPos, Quaternion.identity);
        _ = s.GetComponent<Stalker>();


        Debug.Log("Stalker spawneado en " + spawnPos);
    }
}
