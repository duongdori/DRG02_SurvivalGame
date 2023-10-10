using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    private PlayerInputAction playerInputAction;
    public bool JumpInput { get; private set; } = false;
    public bool SprintInput { get; private set; } = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        playerInputAction = new PlayerInputAction();

        playerInputAction.Player.Jump.performed += JumpOnPerformed;
        playerInputAction.Player.Sprint.performed += SprintOnPerformed;
        playerInputAction.Player.Sprint.canceled += SprintOnCanceled;
    }

    private void SprintOnCanceled(InputAction.CallbackContext context)
    {
        SprintInput = false;
    }

    private void SprintOnPerformed(InputAction.CallbackContext context)
    {
        SprintInput = true;
    }

    private void JumpOnPerformed(InputAction.CallbackContext context)
    {
        JumpInput = context.performed;
    }

    public void SetJumpInput(bool value)
    {
        JumpInput = value;
    }
    public void SetSprintInput(bool value)
    {
        SprintInput = value;
    }

    private void OnEnable()
    {
        playerInputAction.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Player.Disable();
    }

    public Vector2 GetMoveInput()
    {
        return playerInputAction.Player.Move.ReadValue<Vector2>();
    }
}
