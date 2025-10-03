using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("Velocidad del ciclo, más alto = más rápido")]
    public float cycleSpeed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.right, cycleSpeed * Time.deltaTime);
    }
}
