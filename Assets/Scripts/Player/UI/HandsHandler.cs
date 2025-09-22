using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsHandler : MonoBehaviour
{
    public Animator animator;

    public void FlameSwitch(bool isActive) => animator.SetBool("isActive", isActive);

    private void OnEnable()
    {
        FlameSpell.OnFlameSwitch += FlameSwitch;
    }

    private void OnDisable()
    {
        FlameSpell.OnFlameSwitch -= FlameSwitch;
    }
}
