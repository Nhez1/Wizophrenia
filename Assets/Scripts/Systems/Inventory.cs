using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory<T> where T : ItemSO
{
    private Dictionary<string, T> _items = new();

    public void AddItem(T item, string itemName) => _items.Add(itemName, item);

    public void RemoveItem(string item)
    {
        if (_items.ContainsKey(item)) _items.Remove(item);
    }

    public Dictionary<string, T> GetAllItems()
    {
        return new Dictionary<string, T>(_items); // Retorna una copia para evitar manipulación directa
    }
}
