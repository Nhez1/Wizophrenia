public interface IInteractable
{
    string InteractMessage { get; }
    bool IsActive { get; }
    void Interact();

    void OnHoverEnter();
    void OnHoverStay();
    void OnHoverExit();
}
