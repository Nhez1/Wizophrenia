using UnityEngine;

public class RightHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _self;
    [SerializeField] private Animator _anim;
    private RightHandAnimator _view;

    private void Start()
    {
        if (_anim == null) _anim = GetComponent<Animator>();

        _view = new(_anim);
    }

    public void FlameOff(object sender, object data)
    {
        string mode = data as string;

        if (mode is "TurnOff") HUDFlameOff();
        else if (mode is "ForceOff") _view.ShadowHandOff();
    }
    public void FlameSwitchOn() => _view.SwitchFlameSpell(true);
    public void HUDFlameOff() => _view.SwitchFlameSpell(false);
    public void CastFireBall() => _view.CastFire();

    void HideHand() => _self.alpha = 0f;
    void ShowHand() => _self.alpha = 1f;

    private void OnEnable()
    {
        LeftHandler.OnLotusGrab += HideHand;
        LeftHandler.OnLotusLeave += ShowHand;
    }

    private void OnDisable()
    {
        LeftHandler.OnLotusGrab -= HideHand;
        LeftHandler.OnLotusLeave -= ShowHand;
    }
}