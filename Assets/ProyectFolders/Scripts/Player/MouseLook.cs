using UnityEngine;
using System;

public class MouseLook : MonoBehaviour
{
    public static event Action OnCameraLock;
    public static event Action OnCameraUnlock;

    private Transform _parentTransform;
    private InputController _input;
    private Player _player;
    private float _xRotation = 45f;

    [field: SerializeField]
    public bool CameraLocked { get; private set; }

    private void Awake()
    {
        
        _parentTransform = transform.parent;
        _player = GetComponentInParent<Player>();
        
    }

    private void Start()
    {
        UnlockCamera();
        _input = _player.InputControl;

    }

    private void Update()
    {
        if (!CameraLocked) MoveCamera();
    }

    private void MoveCamera()
    {
        _parentTransform.Rotate(Vector3.up * _input.MouseX);

        _xRotation -= _input.MouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    /// <summary>
    /// Camera locked = camera doesn't move, custom cursor is unlocked to move around.
    /// </summary>
    public void LockCamera()
    {
        CameraLocked = true;
        OnCameraLock?.Invoke();
    }

    /// <summary>
    /// Unlock the camera, default playing mode.
    /// </summary>
    public void UnlockCamera()
    {
        CameraLocked = false;
        OnCameraUnlock?.Invoke();
    }
}

