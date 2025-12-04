using UnityEngine;

public abstract class MementoEntity : MonoBehaviour
{
    protected MementoState _memento;

    protected virtual void Awake()
    {
        _memento = new();
    }

    protected abstract void SaveStates();
    protected abstract void LoadStates(object[] oldState);

    public void TryLoadStates()
    {
        if (_memento.GetMemoriesAmount == 0) return;

        var oldState = _memento.LoadState();

        LoadStates(oldState);
    }

    public void ForceSave() => SaveStates();
}
