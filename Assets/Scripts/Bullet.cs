using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float lifeTime;

    protected virtual IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield break;
    }
}
