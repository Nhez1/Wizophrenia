using UnityEngine;

//TP2 Gomez Villarruel Jeremias

public class SparksEffect : Bullet
{
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterLifeTime());
    }

    protected override void OnDespawn()
    {
        SparksFactory.Instance.ReturnSparks(this);
    }
}
