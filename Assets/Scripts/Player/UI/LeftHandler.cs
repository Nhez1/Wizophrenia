using UnityEngine;

public class LeftHandler : MonoBehaviour
{
    public static LeftHandler Instance;

    [SerializeField] private Animator _anim;
    [SerializeField] private CanvasGroup _self;
    [SerializeField] private ItemSlot _handSlot;
    private IConsumable _heldConsumable;
    private LeftHandAnimator _view;
    private Player _player;
    public bool IsHoldingConsumable => _heldConsumable != null;

    private void Start()
    {
        Instance = this;
        if (_self == null) _self = GetComponent<CanvasGroup>();
        if (_anim == null) _anim = GetComponent<Animator>();
        if (_player == null) _player = FindObjectOfType<Player>();

        ClearHandSlot();

        _view = new(_anim);

        InputController.OnConsumableUse += UseItem;
    }

    public void OccupyHandSlot(object sender, object data)
    {
        _self.alpha = 1f;

        if (data is ItemSO item)
        {
            if (item.Type == ItemType.Herb) _view.HoldHerb();
            if (item.Type == ItemType.BadHerb) _view.HoldBadHerb();
            if (item.Type == ItemType.Potion) _view.HoldPotion();


            _heldConsumable = item as IConsumable;
        }
    }

    public void ClearHandSlot()
    {
        _heldConsumable = null;
        _self.alpha = 0f;
    }

    public void UseItem()
    {
        if (!IsHoldingConsumable) return;

        _heldConsumable.Consume(new(_player.Life, _player.Mana, _player.Sanity));
        _heldConsumable = null;

        Destroy(_handSlot.UIItem.gameObject);
        ClearHandSlot();
    }

    private void OnDisable()
    {
        InputController.OnConsumableUse -= UseItem;
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
}
