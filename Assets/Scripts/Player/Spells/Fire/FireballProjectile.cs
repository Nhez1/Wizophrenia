using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float fireballSpeed = 5f;
    public float fireballLifeTime = 8f;
    public float impactEffectLifeTime = 2f;
    public GameObject ImpactEffect;


    void Start()
    {
        Destroy(gameObject, fireballLifeTime);
    }


    void Update()
    {
        transform.Translate(fireballSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy() => Destroy(Instantiate(ImpactEffect, transform.position, Quaternion.identity), impactEffectLifeTime);
}
//Marker