using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOld : MyMonobehaviour
{
    
    public CharacterController controller;
    public Animator animator;
    private Transform cameraMain;

    protected Vector2 moveInput;
    private Vector3 moveDirection;
    protected float yDirection;
    private float gravityValue = -20f;
    
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    
    #region Ground Check Variables

    [Header("Ground Check Variables")]
    
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundCheckLayerMask;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        cameraMain = Camera.main.transform;
    }

    private void Update()
    {
        moveInput = InputManager.Instance.GetMoveInput();
        
        HandleMovement();
        HandleAnimation();
        HandleRotation();
        HandleJump();
        HandleGravity();
        
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void HandleMovement()
    {
        moveDirection = cameraMain.transform.forward * moveInput.y + cameraMain.transform.right * moveInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();
        moveDirection *= moveSpeed;

    }
    private void HandleAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    private void HandleGravity()
    {
        if (IsGrounded() && yDirection < 0.01f)
        {
            yDirection = -0.5f;
        }
        else
        {
            yDirection += gravityValue * Time.deltaTime;
        }
        
        moveDirection.y = yDirection;
    }
    private void HandleJump()
    {
        if (InputManager.Instance.JumpInput && IsGrounded())
        {
            yDirection += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            InputManager.Instance.SetJumpInput(false);
            animator.SetTrigger("Jumping");
        }
    }

    private void HandleRotation()
    {
        if(moveInput == Vector2.zero) return;
         Vector3 direction = cameraMain.transform.forward * moveInput.y + cameraMain.transform.right * moveInput.x;
         direction.y = 0f;
         direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundCheckLayerMask);
    }
    private void OnDrawGizmos()
    {
        if(groundCheckPosition == null) return;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }
}
