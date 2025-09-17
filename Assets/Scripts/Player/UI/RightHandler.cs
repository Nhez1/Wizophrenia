using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandler : MonoBehaviour
{
    public Animator animator;

    public void FlameSwitch(bool isActive, float x) => animator.SetBool("isActive", isActive);

    private void OnEnable()
    {
        FlameSpell.OnFlameSwitch += FlameSwitch;
    }

    private void OnDisable()
    {
        FlameSpell.OnFlameSwitch -= FlameSwitch;
    }
}
