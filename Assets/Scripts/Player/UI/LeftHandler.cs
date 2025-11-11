using UnityEngine;

public class LeftHandler : MonoBehaviour
{
    public static LeftHandler Instance;

    [SerializeField] private Animator _anim;
    [SerializeField] private CanvasGroup _self;
    [SerializeField] private ItemSlot _handSlot;
    private LeftHandAnimator _view;

    private void Start()
    {
        Instance = this;
        _self = GetComponent<CanvasGroup>();
        if (_anim == null) _anim = GetComponent<Animator>();

        _view = new(_anim);

        InputController.OnConsumableUse += UseItem;
    }

    public void OccupyHandSlot(object sender, object data)
    {
        if (data is ItemSO item)
        {
            if (item.Type == ItemType.Herb) _self.alpha = 1f;
        }
    }

    public void ClearHandSlot()
    {
        _self.alpha = 0f;
    }

    public void UseItem()
    {
        if (_handSlot.UIItem == null) return;
        // var usable = UIItem.Item as usable
    }
}

public class LeftHandAnimator
{
    private Animator _a;

    public LeftHandAnimator(Animator a)
    {
        _a = a;
    }
}
