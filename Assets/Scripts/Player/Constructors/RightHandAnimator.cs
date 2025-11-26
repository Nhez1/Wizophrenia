using UnityEngine;

public class RightHandAnimator
{
    private Animator _anim;

    public RightHandAnimator(Animator a)
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

    public void CastFire()
    {
        _anim.ResetTrigger("castFireBall");
        _anim.SetTrigger("castFireBall");
    }
}
