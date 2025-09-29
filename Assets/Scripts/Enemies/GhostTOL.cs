using UnityEngine;
using System;

// Por Jere

public class GhostTOL : MonoBehaviour  //Ghost Turn Off Lights
{
    public static event Action ForceFlameOff;

    [Header("Movement")]
    public Transform target;  //Aca colocar la mano desde el inspector (usar firepoint de ser necesario)
    public float speed = 1.5f;
    public float stopDistance = 0.3f;

    void Update()
    {
        if (target == null) return;

        // Mover directo hacia la mano
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Revisar distancia
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= stopDistance)
        {
            ForceFlameOff?.Invoke();
            Destroy(gameObject);
        }
    }
}
