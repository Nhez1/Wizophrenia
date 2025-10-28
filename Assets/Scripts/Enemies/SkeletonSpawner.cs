using System.Collections;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject skeletonPrefab;
    public int maxSkeletons = 5;
    public float spawnInterval = 5f;
    public float spawnRadius = 20f;
    public float groundOffset = 0.5f;

    private readonly System.Collections.Generic.List<GameObject> spawnedSkeletons = new();

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            //si ya hay muchos esqueletos, espera
            spawnedSkeletons.RemoveAll(s => s == null);
            if (spawnedSkeletons.Count >= maxSkeletons) continue;

            //genera una posicion aleatoria en un círculo
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = Terrain.activeTerrain 
                ? Terrain.activeTerrain.SampleHeight(randomPos) + groundOffset
                : transform.position.y + groundOffset;

            GameObject newSkeleton = Instantiate(skeletonPrefab, randomPos, Quaternion.identity);
            spawnedSkeletons.Add(newSkeleton);

            Debug.Log($"Spawned Skeleton at {randomPos}");
        }
    }
}
