using UnityEngine;

public class Billboard : MonoBehaviour
{
    // This script is for 2D objects to be rendered properly inside a 3D space.
    [SerializeField] private bool _lockXRotation;
    [SerializeField] private bool _lockYRotation;
    [SerializeField] private bool _lockZRotation;

    [SerializeField] private Vector3 _rotationOffset = new(0f, 0f, 0f);

    private void LateUpdate()
    {
        var cam = Camera.main;
        if (!cam) return;

        // Get direction toward camera
        Vector3 dir = transform.position - cam.transform.position;
        dir.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        Vector3 euler = targetRot.eulerAngles + _rotationOffset;
        Vector3 current = transform.eulerAngles;

        if (_lockXRotation) euler.x = current.x;
        if (_lockYRotation) euler.y = current.y;
        if (_lockZRotation) euler.z = current.z;

        transform.rotation = Quaternion.Euler(euler);
    }
}