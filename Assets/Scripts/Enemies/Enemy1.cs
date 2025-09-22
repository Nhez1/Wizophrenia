using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisappearingSpirit : MonoBehaviour
{
    private float _speed = 2;

    [Tooltip("Offset de qué tan directamente tiene que mirar el jugador al enemigo para que desaparezca")]
    public float lookOffset = 0.8f;
    private float wanderTimer;
    private float wanderCooldown = 2f;
    public Transform playerPos; // se asigna en el inspector
    Camera playerCam;

    public bool willMove = true;

    void Start()
    {
        playerCam = Camera.main;
        StartCoroutine(DeambulateTimer());
    }

    void Update()
    {
        playerPos = playerCam.transform;

        // Movimiento errante
        wanderTimer += Time.deltaTime;
        if (wanderTimer > wanderCooldown)
        {
            Vector3 target = playerPos.position + Random.insideUnitSphere * 3f;
            target.y = playerPos.position.y + 1f;
            wanderTimer = 0f;
        }

        // Ver si lo estás mirando
        LookingToSpirit();
    }

    void LookingToSpirit()
    {
        Vector3 dirToSpirit = (transform.position - Camera.main.transform.position).normalized;
        float dot = Vector3.Dot(playerCam.transform.forward, dirToSpirit);

        if (dot > 0.8f) //cerca de el centro
        {
            Debug.Log("Looked");
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
