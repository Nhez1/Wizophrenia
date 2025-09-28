using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.AI;

public class DisappearingSpirit : MonoBehaviour
{
    private float _speed = 2;
    [Tooltip("Offset de qué tan directamente tiene que mirar el jugador al enemigo para que desaparezca")]
    public float lookOffset = 0.8f;
    private float wanderTimer;
    private float wanderCooldown = 2f;
    private Camera camera2;

    public bool willMove = true;

    void Start()
    {
        camera2 = Camera.main;
        StartCoroutine(DeambulateTimer());
    }

    void Update()
    {
        // Movimiento errante
        wanderTimer += Time.deltaTime;
        if (wanderTimer > wanderCooldown)
        {
            Vector3 target = camera2.transform.position + Random.insideUnitSphere * 3f;
            target.y = camera2.transform.position.y + 1f;
            wanderTimer = 0f;
        }

        // Ver si lo estás mirando
        LookingToSpirit();
    }

    void LookingToSpirit()
    {
        Vector3 dirToSpirit = (transform.position - camera2.transform.position).normalized;
        float dot = Vector3.Dot(camera2.transform.forward, dirToSpirit);

        if (dot > 0.8f) //cerca de el centro
        {
            gameObject.SetActive(false);
        }
    }

    private void Deambulate()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        transform.position += _speed * Time.fixedDeltaTime * randomDir;
    }

    public IEnumerator DeambulateTimer()
    {
        if (willMove)
        {
            Deambulate();
            yield return new WaitForSeconds(2);
        }
    }
}
//Marker
