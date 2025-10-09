using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
public class Bullet : MonoBehaviour, IProduct
{
    [SerializeField] string _productName;
    string IProduct.productName { get => _productName;}

    [SerializeField] Color _usingColor;

    public void Initialize()
    {
        GetComponent<Renderer>().material.color = _usingColor;
    }

}

}

