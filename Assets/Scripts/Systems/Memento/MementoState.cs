using System.Collections.Generic;

public class MementoState
{
    private Stack<object[]> _statesStack;

    public MementoState()
    {
        _statesStack = new Stack<object[]>();
    }

    public int GetMemoriesAmount => _statesStack.Count;

    public void SaveMemory(params object[] newState)
    {
        _statesStack.Push(newState);
    }

    public object[] LoadState()
    {
        var lastState = _statesStack.Pop();
        return lastState;
    }
}
