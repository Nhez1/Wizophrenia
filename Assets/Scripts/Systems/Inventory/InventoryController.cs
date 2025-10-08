using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController<T> where T : ItemSO
{
    private List<T> _items = new();

    public void AddItem(T item) => _items.Add(item);

    public void RemoveItem(T item)
    {
        if (_items.Contains(item)) _items.Remove(item);
    }

    public List<T> GetAllItems()
    {
        return new List<T>(_items); // Retorna una copia para evitar manipulación directa
    }
}
