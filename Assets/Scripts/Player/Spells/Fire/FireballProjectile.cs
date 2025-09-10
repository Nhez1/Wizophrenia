using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
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
        transform.Translate(fireballSpeed * Time.deltaTime * Vector3.forward);
    }

    void OnTriggerEnter (Collider other)
    {
        if (ImpactEffect != null)
        {
            Instantiate (ImpactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
