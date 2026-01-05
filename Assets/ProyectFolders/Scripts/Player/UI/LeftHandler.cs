using UnityEngine;
using System;

public class LeftHandler : MonoBehaviour
{
    public static event Action OnLotusGrab;
    public static event Action OnLotusLeave;
    [SerializeField] private GameEvent _onLotusGrab;
    [SerializeField] private GameEvent _onLotusLeave;

    [SerializeField] private Animator _anim;
    [SerializeField] private CanvasGroup _self;
    [SerializeField] private ItemSlot _handSlot;
    private IConsumable _heldConsumable;
    private LeftHandAnimator _view;
    private Player _player;
    public bool IsHoldingConsumable => _heldConsumable != null;

    private void Start()
    {
        if (_self == null) _self = GetComponent<CanvasGroup>();
        if (_anim == null) _anim = GetComponent<Animator>();
        if (_player == null) _player = FindObjectOfType<Player>();

        ClearHandSlot(null, null);

        _view = new(_anim);
    }

    public void OccupyHandSlot(object sender, object data)
    {
        _self.alpha = 1f;

        if (data is ItemSO item)
        {
            if (item.Type == ItemType.Herb) _view.HoldHerb();
            if (item.Type == ItemType.BadHerb) _view.HoldBadHerb();
            if (item.Type == ItemType.Potion) _view.HoldPotion();
            if(item.Type == ItemType.LotusFlower)
            {
                OnLotusGrab?.Invoke();
                _view.HoldLotus();
            }


            _heldConsumable = item as IConsumable;
        }
    }

    public void ClearHandSlot(object sender, object data)
    {
        _heldConsumable = null;
        _self.alpha = 0f;

        if (data is ItemSO item && item.Type == ItemType.LotusFlower) OnLotusLeave?.Invoke();
    }

    public void UseItem()
    {
        if (!IsHoldingConsumable) return;

        _heldConsumable.Consume(new(_player.Life, _player.Mana, _player.Sanity));
        _heldConsumable = null;

        Destroy(_handSlot.UIItem.gameObject);
        ClearHandSlot(null, null);
    }
}

public class LeftHandAnimator
{
    private Animator _a;

    public LeftHandAnimator(Animator a)
    {
        _a = a;
    }

    public void HoldHerb() => _a.Play("HoldHerb");
    public void HoldBadHerb() => _a.Play("HoldHerbBad");
    public void HoldPotion() => _a.Play("HoldPotion");
    public void HoldLotus() => _a.Play("HoldLotus");
}
