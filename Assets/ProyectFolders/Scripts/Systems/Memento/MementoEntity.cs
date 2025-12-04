using UnityEngine;

public abstract class MementoEntity : MonoBehaviour
{
    protected MementoState _memento;
    protected object _scriptType;

    protected virtual void Awake()
    {
        _memento = new();
        _scriptType = GetType();
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

    public void SaveToGlobalCache()
    {
        if (_memento.GetMemoriesAmount == 0)
            SaveStates();

        GlobalMementoCache.Save(GetType(),_memento.LoadState());
    }

    public void RestoreFromGlobalCache()
    {
        if (!GlobalMementoCache.TryLoad(GetType(), out var state))
            return;

        _memento.SaveMemory(state);
        GlobalMementoCache.Clear(GetType());
        TryLoadStates();
    }
}
