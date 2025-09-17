using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform parentTransform;
    InputController _input;
    Player player;
    float xRotation = 45f;

    public InputController InputControl => InputControl;


    private void Start()
    {
        parentTransform = transform.parent;
        player = GetComponentInParent<Player>();

        _input = player.InputControl;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        parentTransform.Rotate(Vector3.up * _input.MouseX);

        xRotation -= _input.MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void Option()
    {

    }
}
