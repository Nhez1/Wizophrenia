using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<EventListener> listeners = new();

    //transmitir el evento, los listeners lo van a recivir como una señal de radio
    public void Raise(object sender, params object[] data)
    {
        for (int i = 0; i < listeners.Count; i++)
            listeners[i].OnEventRaised(this, sender, data);
    }


    //cosas para gestionar los listeners
    public void RegisterListener(EventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void UnregisterListener(EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
