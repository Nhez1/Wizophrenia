using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Mana
{
    public static event Action<float> OnManaChanged;

    [Tooltip("Maximum MP")]
    [SerializeField] private float _maxMP = 100;
    [Tooltip("Mana Points")]
    [SerializeField] private float _mp;

    private Coroutine _drainRoutine;
    private bool _activateDrain = false;
    private bool _isDraining = false;
    private MonoBehaviour coroutineStarter;
    public float MaxMP { get { return _maxMP; } private set { } }
    public float MP { get { return _mp; } private set => _mp = Mathf.Clamp(value, 0f, _maxMP); }

    public Mana(MonoBehaviour mb)
    {
        MP = MaxMP;
        coroutineStarter = mb;
    }

    public void Spend(float amount)
    {
        MP -= amount;
        UpdateMana(MP);
    }

    /// <summary>
    /// Restore a specified amount of Mana to the player.
    /// </summary>
    public void Restore(float amount)
    {
        if (_mp < _maxMP) MP += amount;
        UpdateMana(MP);
    }

    /// <summary>
    /// Reduce the player's maximum MP by a specified amount.
    /// </summary>
    /// <param name="amount">How much maxMP you want to take away from the player.</param>
    public void ReduceMaxMP(float amount)
    {
        if (amount >= _maxMP) _maxMP = 0;
        else _maxMP -= amount;
    }

    public void SetMP(float amount) => MP = amount;
    public void SetMaxMP(float amount) => MaxMP = amount;

    public void Drain(float amountPerSec)
    {
        _activateDrain = !_activateDrain;

        if (_activateDrain)
        {
            if (_drainRoutine == null && MP > amountPerSec) _drainRoutine = coroutineStarter.StartCoroutine(DrainCoroutine(amountPerSec));
        }
        else
        {
            if (_drainRoutine != null)
            {
                coroutineStarter.StopCoroutine(_drainRoutine);
                _drainRoutine = null;
            }
            _isDraining = false;
        }
    }

    IEnumerator DrainCoroutine(float amount)
    {
        if (_isDraining) yield break;
        _isDraining = true;

        while (_activateDrain)
        {
            Spend(amount);
            yield return new WaitForSeconds(1f);

            if (MP <= amount)
            {
                _activateDrain = false;
                _isDraining = false;
            }
        }

        _isDraining = false;
    }

    private void UpdateMana(float mP) => OnManaChanged?.Invoke(mP);

}

