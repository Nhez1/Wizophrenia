using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private GameEvent _onSceneChange;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private string _targetScene;
    [SerializeField] private string _interactMessage = "Enter Cottage";
    public string InteractMessage => _interactMessage;

    public bool IsActive => gameObject.activeSelf;

    public void Interact()
    {
        _onSceneChange.Raise(this, _targetScene);
    }

    public void OnHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverStay()
    {
        throw new System.NotImplementedException();
    }
}
