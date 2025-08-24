using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform parentTransform;
    InputController _controller;
    Player player;

    public InputController InputControl => InputControl;

    private void Start()
    {
        parentTransform = transform.parent;
        player = GetComponentInParent<Player>();

        _controller = player.InputControl;
        
    }

    private void Update()
    {
        parentTransform.Rotate(Vector3.up * _controller._mouseX);
    }
}
