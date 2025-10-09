using UnityEngine;

public class HandsView
{
    private Animator _anim;

    public HandsView(Animator a)
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
