using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireballSpeed = 5f;
    public float fireballLifeTime = 8f;
    public GameObject ImpactEffect;


    void Start()
    {
        Destroy (gameObject, fireballLifeTime);
    }


    void Update()
    {
        transform.Translate(Vector3.forward * fireballSpeed * Time.deltaTime);
    }

    void OnTriggerEnter (Collider other)
    {
        if (ImpactEffect != null)
        {
            Instantiate ( ImpactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
