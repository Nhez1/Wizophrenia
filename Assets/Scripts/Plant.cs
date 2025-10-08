using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State
{
    Seed,
    Sapling,
    Bush,
    Grown
}

public class Plant : Interactable
{
    [Header(" Plant Characteristics ")]
    public State currentState;
    public int growTime;

    public Player player;
    public HerbSO yield;

    private Animator _animator;
    private Coroutine growCycleCoroutine;

    void Start()
    {
        CanInteract = false;
        currentState = State.Seed;
        _animator = GetComponent<Animator>();

        growCycleCoroutine = StartCoroutine(Photosynthesis());
    }

    public void Grow()
    {
        if (currentState == State.Grown) // Si el estado actual de la planta es mayor o igual al estado final (-1 porque cuenta al 0), que pare de crecer.
        {
            CanInteract = true;
            return;
        }
        else // Si no, que crezca.
        {
            currentState++;
            CheckPlantState();

            growCycleCoroutine = StartCoroutine(Photosynthesis());
        }
    }

    public override void Interact()
    {
        player.Inventory.AddItem(yield);
        base.Interact();
    }

    private void CheckPlantState()
    {
        _animator.SetInteger("plantState", (int)currentState);
    }

    IEnumerator Photosynthesis()
    {
        yield return new WaitForSeconds(growTime);
        Grow();

        growCycleCoroutine = null;
    }
}
// Marker