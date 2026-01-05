using System;
using UnityEngine;
using UnityEngine.Events;

//creo nuestro propio tipo de game event para poder mandar mas parametros
[Serializable]
public class CustomGameEvent : UnityEvent<object, object[]> { }

public class EventListener : MonoBehaviour
{
    [SerializeField] EventCouple[] _events;

    private void OnEnable()
    {
        foreach (var eventCouple in _events) eventCouple.gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        foreach (var eventCouple in _events) eventCouple.gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(GameEvent raisedEvent, object sender, object[] data)
    {
        for (int i = 0; i < _events.Length; i++)
        {
            if (_events[i].gameEvent == raisedEvent)
            {
                _events[i].response.Invoke(sender, data);
                return; // one response per event
            }
        }
    }

}

[Serializable]
public class EventCouple
{
    public GameEvent gameEvent;
    public CustomGameEvent response;
}