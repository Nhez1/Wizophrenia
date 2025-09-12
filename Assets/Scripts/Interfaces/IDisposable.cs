public interface IDisposable
{
    void Dispose();
}


public interface ICommand
{
    void Do();
    void UnDo();
}