using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
public abstract class Factory : MonoBehaviour
{
    public abstract IProduct GetProduct(Vector3 position);

}

}

