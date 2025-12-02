using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MementoEntity : MonoBehaviour
{
    protected MementoState _memento;

    private void Awake()
    {
        _memento = new();
    }

    private void LateUpdate()
    {
        SaveStates();
    }

    protected abstract void SaveStates();

    public void TryLoadStates()
    {
        if (_memento.GetMemoriesAmount == 0) return;

        var oldState = _memento.LoadState();

        LoadStates(oldState);
    }

    protected abstract void LoadStates(object[] oldState);
}
