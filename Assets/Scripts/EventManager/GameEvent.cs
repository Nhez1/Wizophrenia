using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<EventListener> listeners = new List<EventListener>();

    //transmitir el evento, los listeners lo van a recivir como una señal de radio
    public void Raise(Component sender, object data) //se le suele decir raise, al parecer, pero siento que "broadcast" seria mas correcto lol
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }

    //cosas para gestionar los listeners

    public void RegisterListener(EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void UnregisterListener(EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

}
