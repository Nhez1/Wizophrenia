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

public class Plant : MonoBehaviour
{
    [Header(" Plant Characteristics ")]
    public State currentState;
    public int growTime;
    [SerializeField] private bool _canHarvest = false;

    private Interactable _interactor;
    private Animator _animator;
    private Coroutine growCycleCoroutine;

    void Start()
    {
        currentState = State.Seed;
        _interactor = GetComponent<Interactable>();
        _animator = GetComponent<Animator>();

        growCycleCoroutine = StartCoroutine(Photosynthesis());
    }

    private void Update() => _interactor.CanInteract = _canHarvest;

    public void Grow()
    {
        if ((int)currentState >= Enum.GetValues(typeof(State)).Length - 1) // Si el estado actual de la planta es mayor o igual al estado final (-1 porque cuenta al 0), que pare de crecer.
        {
            _canHarvest = true;
            return;
        }
        else // Si no, que crezca.
        {
            currentState++;
            CheckPlantState();

            growCycleCoroutine = StartCoroutine(Photosynthesis());
        }
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