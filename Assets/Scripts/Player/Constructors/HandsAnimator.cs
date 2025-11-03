using UnityEngine;

public class HandsAnimator
{
    private Animator _anim;

    public HandsAnimator(Animator a)
    {
        _anim = a;
    }

    public void ShadowHandOff()
    {
        // Cancel any current transitions first
        _anim.ResetTrigger("shadowHandTrigger");
        _anim.SetTrigger("shadowHandTrigger");
    }

    public void SwitchFlameSpell(bool isActive) => _anim.SetBool("isActive", isActive);
}
