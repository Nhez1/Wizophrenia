using System;
using System.Collections;
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
    public PlantState currentState;
    public int growTime = 5;
    private bool _canHarvest;

    public Player player;
    public HerbSO yield;

    private Animator _anim;
    private Coroutine growCycleCoroutine;

    [field: SerializeField]
    public string InteractMessage { get; set; }
    public bool IsActive => gameObject.activeSelf;

   public AudioClip harvestClip;

    void Start()
    {
        SetDefault();
        _anim = GetComponent<Animator>();

        growCycleCoroutine = StartCoroutine(Photosynthesis());
    }

    public void Grow()
    {
        currentState++;
        _anim.SetInteger("plantState", (int)currentState);
        CheckPlantState();
    }

    private void CheckPlantState()
    {
        if (currentState == PlantState.Grown) //Si la planta est� 'Crecida', que pare de crecer.
        {
            InteractMessage = "Harvest";
            _canHarvest = true;
            return;
        }
        else //Si no, que crezca.
        {
            InteractMessage = "Can't harvest yet!";
            growCycleCoroutine = StartCoroutine(Photosynthesis());
        }
    }

    IEnumerator Photosynthesis()
    {
        yield return new WaitForSeconds(growTime);
        Grow();

        growCycleCoroutine = null;
    }

    public void Interact()
    {
        if (_canHarvest)
        {
            player.Inventory.AddItem(yield);
            AudioSource.PlayClipAtPoint(harvestClip, transform.position);
            gameObject.SetActive(false);
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

    void SetDefault()
    {
        currentState = PlantState.Seed;
        _canHarvest = false;
        InteractMessage = "Can't harvest yet!";
    }
}
// Marker