using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryPool
{
    public class Pool<T> where T : MonoBehaviour
    {
        Func<T> _factoryMethod;

        Action<T> _turnOnCallBack;

        Action<T> _turnOffCallBack;

        List<T> _currentStock;

        public Pool(Func<T> factoryMethod, Action<T> turnOnCallBack, Action<T> turnOffCallBack, int initialAmount)
        {
            _factoryMethod = factoryMethod;
            _turnOnCallBack = turnOnCallBack;
            _turnOffCallBack = turnOffCallBack;

            _currentStock = new List<T>();

            for (int i = 0; i < initialAmount; i++)
            {
                var createdObject = _factoryMethod();
                _turnOffCallBack(createdObject);
                _currentStock.Add(createdObject);
            }
        }
        public T GetObject()
        {
            T objectToReturn;

            if (_currentStock.Count != 0)
            {
                objectToReturn = _currentStock[0];
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

}

