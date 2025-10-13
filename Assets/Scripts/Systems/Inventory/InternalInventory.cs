using System.Collections.Generic;
using System;

public class InternalInventory<T> where T : ItemSO
{
    public static event Action<T> OnItemAdded;
    private List<T> _items = new();

    public void AddItem(T item)
    {
        _items.Add(item);
        OnItemAdded?.Invoke(item);
    }

    public void RemoveItem(T item)
    {
        if (_items.Contains(item)) _items.Remove(item);
    }

    public List<T> GetAllItems()
    {
        return new List<T>(_items); // Retorna una copia para evitar manipulación directa
    }
}
