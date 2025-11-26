using System;
using UnityEngine;

public enum PlantState
{
    Seed,
    Sapling,
    Bush,
    Grown
}

public class Plant : MonoBehaviour, IInteractable
{
    [Header(" Plant Characteristics ")]
    //[SerializeField] private PlantState _currentState;
    //[SerializeField] private int _growTime = 5;
    [SerializeField] private HerbSO _yield;
    [SerializeField] private Sprite _brokenSprite;
    private bool _canHarvest;

    private Player _player;

    //private Animator _anim;
    //private Coroutine _growCycleCoroutine;

    [field: SerializeField]
    public string InteractMessage { get; set; }
    public bool IsActive => _canHarvest;

    [SerializeField] private AudioClip _harvestClip;

    void Start()
    {
        _canHarvest = true;
        _player = FindObjectOfType<Player>();
        //_anim = GetComponent<Animator>();
    }

    //public void Grow()
    //{
    //    _currentState++;
    //    _anim.SetInteger("plantState", (int)_currentState);
    //    CheckPlantState();
    //}

    //private void CheckPlantState()
    //{
    //    if (_currentState == PlantState.Grown) //Si la planta est� 'Crecida', que pare de crecer.
    //    {
    //        InteractMessage = "Harvest";
    //        _canHarvest = true;
    //        return;
    //    }
    //    else //Si no, que crezca.
    //    {
    //        InteractMessage = "Can't harvest yet!";
    //        _growCycleCoroutine = StartCoroutine(Photosynthesis());
    //    }
    //}

    //IEnumerator Photosynthesis()
    //{
    //    yield return new WaitForSeconds(_growTime);
    //    Grow();

    //    _growCycleCoroutine = null;
    //}

    public void Interact()
    {
        if (_canHarvest)
        {
            if (_player) _player.Inventory.AddItem(_yield);
            _yield = null;
            AudioSource.PlayClipAtPoint(_harvestClip, transform.position);
            if (_brokenSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = _brokenSprite;
            }
            else gameObject.SetActive(false);

            _canHarvest = false;
        }
    }

    public void EnableInteraction() => _canHarvest = true;
    public void DisableInteraction() => _canHarvest = false;

    #region OnHover (not implemented)
    public void OnHoverEnter()
    {
        throw new NotImplementedException();
    }

    public void OnHoverStay()
    {
        throw new NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new NotImplementedException();
    }
    #endregion

    //void SetDefault()
    //{
    //    _currentState = PlantState.Seed;
    //    _canHarvest = false;
    //    InteractMessage = "Can't harvest yet!";
    //}
}
// Marker