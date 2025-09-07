using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsHandler : MonoBehaviour
{
    public Animator animator;

    public void FlameOff() => animator.SetBool("isActive", false);
    public void FlameOn() => animator.SetBool("isActive", true);

    private void OnEnable()
    {
        FlameSpell.OnFlameOn += FlameOn;
        FlameSpell.OnFlameOff += FlameOff;
    }

    private void OnDisable()
    {
        FlameSpell.OnFlameOn -= FlameOn;
        FlameSpell.OnFlameOff -= FlameOff;
    }
}
