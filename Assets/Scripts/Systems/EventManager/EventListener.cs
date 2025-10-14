using UnityEngine;
using UnityEngine.Events;

//creo nuestro propio tipo de game event para poder mandar mas parametros
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

public class EventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}