using System.Collections.Generic;
using UnityEngine;

// DISCLAIMER: MADE BY AI //
public class GrassChunkCombiner : MonoBehaviour
{
    [Header("Chunk Settings")]
    public float chunkSize = 32f;
    public Material grassMaterial; // Use a cutout shader (e.g. "Universal Render Pipeline/Lit" with Alpha Clipping)

    [ContextMenu("Combine Grass")]
    public void CombineGrass()
    {
        var sprites = FindObjectsOfType<SpriteRenderer>();
        Dictionary<Vector2Int, List<SpriteRenderer>> chunks = new();

        // Group by chunk position
        foreach (var sr in sprites)
        {
            if (!sr.enabled || sr.sprite == null) continue;

            Vector3 pos = sr.transform.position;
            Vector2Int key = new(
                Mathf.FloorToInt(pos.x / chunkSize),
                Mathf.FloorToInt(pos.z / chunkSize)
            );
            if (!chunks.ContainsKey(key))
                chunks[key] = new List<SpriteRenderer>();
            chunks[key].Add(sr);
        }

        int created = 0;
        foreach (var kvp in chunks)
        {
            List<CombineInstance> combines = new();
            foreach (var sr in kvp.Value)
            {
                var sprite = sr.sprite;
                if (sprite == null) continue;

                // Convert sprite to quad mesh
                Mesh mesh = new();
                Vector3[] verts = new Vector3[4];
                Vector2[] uvs = new Vector2[4];
                int[] tris = { 0, 1, 2, 2, 3, 0 };

                for (int i = 0; i < 4; i++)
                {
                    verts[i] = sprite.vertices[i];
                    uvs[i] = sprite.uv[i];
                }

                mesh.vertices = verts;
                mesh.uv = uvs;
                mesh.triangles = tris;

                // Apply transform
                CombineInstance ci = new();
                ci.mesh = mesh;
                ci.transform = sr.transform.localToWorldMatrix;
                combines.Add(ci);
            }

            if (combines.Count == 0) continue;

            Mesh combinedMesh = new();
            combinedMesh.CombineMeshes(combines.ToArray(), true, true, false);

            GameObject chunk = new("GrassChunk_" + created++);
            chunk.transform.position = Vector3.zero;

            var mf = chunk.AddComponent<MeshFilter>();
            mf.sharedMesh = combinedMesh;

            var mr = chunk.AddComponent<MeshRenderer>();
            mr.sharedMaterial = grassMaterial;

            chunk.isStatic = true;
        }

        // Delete originals
        foreach (var sr in sprites)
            DestroyImmediate(sr.gameObject);

        Debug.Log($"Combined into {created} chunks.");
    }
}
