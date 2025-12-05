using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDecorator : MonoBehaviour
{
    public float SpeedMultiplier { get; private set; } = 1f;

    public void SetSlow(float multiplier)
    {
        SpeedMultiplier = multiplier;
    }

    public void ClearSlow()
    {
        SpeedMultiplier = 1f;
    }

}
