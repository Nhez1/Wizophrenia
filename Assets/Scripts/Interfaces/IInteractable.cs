public interface IInteractable

//TP2 Gomez Villarruel Jeremias
{
    string InteractMessage { get; }
    bool IsActive { get; }
    void Interact();

    void OnHoverEnter();
    void OnHoverStay();
    void OnHoverExit();
}
