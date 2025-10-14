using UnityEngine;

public class Billboard : MonoBehaviour
{
    // This script is for 2D objects to be rendered properly inside a 3D space.
    [SerializeField] private float xRotation = 0f;
    [SerializeField] private float yRotation = 180f;
    [SerializeField] private float zRotation = 0f;

    private void LateUpdate()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        // Rotate only on Y axis.
        cameraPos.y = transform.position.y;
        // Make the sprite face the camera.
        transform.LookAt(cameraPos);
        // Rotate 180 on Y because of how SpriteRenderer works.
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}