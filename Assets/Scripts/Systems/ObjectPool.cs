using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    Func<T> _factoryMethod; //How to create object
    Action<T> _turnOnCallBack; //Turn on gameObject
    Action<T> _turnOffCallBack; //Turn off gameObject
    List<T> _currentStock; //Object pool stock

    public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallBack, Action<T> turnOffCallBack, int initialAmount, Transform parent = null)
    {
        _factoryMethod = factoryMethod;
        _turnOnCallBack = turnOnCallBack;
        _turnOffCallBack = turnOffCallBack;

        _currentStock = new List<T>();

        for (int i = 0; i < initialAmount; i++)
        {
            var createdObject = _factoryMethod();
            if (parent != null) createdObject.transform.parent = parent;
            _turnOffCallBack(createdObject);
            _currentStock.Add(createdObject);
        }
    }
    public T GetObject()
    {
        T objectToReturn;

        if (_currentStock.Count != 0)
        {
            objectToReturn = _currentStock[0]; //Se ingresa al primer objeto ya que al ser todos iguales, no es relevante cuál agarrar
            _currentStock.RemoveAt(0);
        }
        else
        {
            objectToReturn = _factoryMethod();
        }

        _turnOnCallBack(objectToReturn);
        return objectToReturn;
    }

    public void ReturnObjectToPool(T obj)
    {
        _turnOffCallBack(obj);
        _currentStock.Add(obj);
    }
}


