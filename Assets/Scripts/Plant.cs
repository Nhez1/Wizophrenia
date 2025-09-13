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

public class Plant : MonoBehaviour, IHoverable
{
    public State currentState;
    public int growTime = 5;
    private Coroutine growCycleCoroutine;
    private Animator _animator;
    private bool _pickupAble = false;

    void Start()
    {
        currentState = State.Seed;
        _animator = GetComponent<Animator>();

        growCycleCoroutine = StartCoroutine(Photosynthesis());
    }

    public void Grow()
    {
        if ((int)currentState >= Enum.GetValues(typeof(State)).Length - 1) // Si el estado actual de la planta es mayor o igual al estado final, que pare de crecer.
        {
            _pickupAble = true;
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
        Debug.Log("Current plant state is " + currentState);
    }

    IEnumerator Photosynthesis()
    {
        yield return new WaitForSeconds(growTime);
        Grow();

        growCycleCoroutine = null;
    }

    public void OnHover()
    {
        throw new NotImplementedException();
    }

    // (Seed) -> Sapling -> Bush -> Plant

}
