using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private SceneController _sceneManager;
    [SerializeField] private string _targetScene;
    [SerializeField] private string _interactMessage = "Enter Cottage";
    public string InteractMessage => _interactMessage;

    public bool IsActive => gameObject.activeSelf;

    public void Interact()
    {
        //_sceneManager.OldSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        _sceneManager.ChangeSceneAsyncByName(_targetScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
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
