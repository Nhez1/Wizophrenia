using System;
using System.Collections;
using TMPro;
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
    [SerializeField] private Sprite _brokenSprite;
    private bool _canHarvest;
    public bool isLotus;

    public Player player;
    [SerializeField] private HerbSO _yield;

    public PauseSystem pause;
    public GameObject winCanvas;

    //private Animator _anim;
    //private Coroutine _growCycleCoroutine;

    [field: SerializeField]
    public string InteractMessage { get; set; }
    public bool IsActive => _canHarvest;

    [SerializeField] private AudioClip _harvestClip;

    void Start()
    {
        _canHarvest = true;
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
            player.Inventory.AddItem(_yield);
            _yield = null;
            AudioSource.PlayClipAtPoint(_harvestClip, transform.position);
            if (_brokenSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = _brokenSprite;
            }
            else gameObject.SetActive(false);

            if (isLotus)
            {
                pause.Pause();
                UICursor.Instance.ActivateCursor();
                winCanvas.SetActive(true);
            }
            _canHarvest = false;
        }
    }

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

    //void SetDefault()
    //{
    //    _currentState = PlantState.Seed;
    //    _canHarvest = false;
    //    InteractMessage = "Can't harvest yet!";
    //}
}
// Marker