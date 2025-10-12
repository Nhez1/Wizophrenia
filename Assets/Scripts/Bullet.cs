using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float lifeTime;

    protected virtual IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield break;
    }
}
