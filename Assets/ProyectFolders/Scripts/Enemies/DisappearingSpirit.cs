using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Creado por Jere, corregido por Fecu

public class DisappearingSpirit : MonoBehaviour
{
    [Tooltip("Offset de qu� tan directamente tiene que mirar el jugador al enemigo para que desaparezca")]
    public float lookOffset = 0.8f;
    private Transform camPos;

    public bool willMove = true;

    void Update()
    {
        camPos = Camera.main.transform;

        // Ver si lo est�s mirando
        LookingToSpirit();
    }

    void LookingToSpirit()
    {
        Vector3 dirToSpirit = (transform.position - camPos.transform.position).normalized;
        float dot = Vector3.Dot(camPos.transform.forward, dirToSpirit);

        if (dot > 0.8f) //cerca de el centro
        {
            gameObject.SetActive(false);
        }
    }
}
//Marker
