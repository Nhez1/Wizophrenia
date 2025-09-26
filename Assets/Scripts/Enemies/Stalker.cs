using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{
    Transform player; //esta en private porque va a reconocer al player a traves del tag
    public float speed = 1.5f;
    public float stopDistance = 1f;

    private Renderer rend;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 toEnemy = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, toEnemy);                      //ve si el player lo esta viendo

        if (dot > 0.7f)  //si el jugador lo mira se queda quieto, similar al disappearing spirit
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);  //si no lo mira, se mueve
        if (distance > stopDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        transform.LookAt(player); //siempre mira al player
    }
}
