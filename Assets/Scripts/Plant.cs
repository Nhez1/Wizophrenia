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
    public State currentState;
    public int growTime = 5;
    private Coroutine growCycleCoroutine;
    private Animator _animator;

    void Start()
    {
        currentState = State.Seed;
        _animator = GetComponent<Animator>();

        growCycleCoroutine = StartCoroutine(Photosynthesis());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) Grow();

        
    }

    public void Grow()
    {
        if ((int)currentState >= Enum.GetValues(typeof(State)).Length - 1) return;
        else
        {
            currentState++;
            _animator.SetInteger("plantState", (int)currentState);
            Debug.Log("Current plant state is " + currentState);

            growCycleCoroutine = StartCoroutine(Photosynthesis());
        }
    }

    IEnumerator Photosynthesis()
    {
        yield return new WaitForSeconds(growTime);
        Grow();

        growCycleCoroutine = null;
    }

    // (Seed) -> Sapling -> Bush -> Plant

}
