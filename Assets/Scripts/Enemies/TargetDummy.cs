using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Por Jere, simplemente para el escenario de prueba

public class TargetDummy : MonoBehaviour
{

void OnTriggerEnter (Collider other)
{
    if (other.CompareTag("Fireball"))
    {
        Destroy (gameObject);
        Destroy (other.gameObject);
    }
}
}
