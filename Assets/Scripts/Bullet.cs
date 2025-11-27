using System.Collections;
using UnityEngine;

//TP2 Gomez Villarruel Jeremias

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float lifeTime;

    private Coroutine _lifeCoroutine;

    private void OnEnable()
    {
        _lifeCoroutine = StartCoroutine(ReturnToPoolAfterLifeTime());
    }

    protected virtual IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        OnDespawn();
    }

    protected abstract void OnDespawn();
}
