using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.PlayerStateMachine;

public class JumpState : State
{
    private bool grounded;
    private float gravityValue;
    private float jumpHeight;
    private float playerSpeed;
    private Vector3 airVelocity;
    public JumpState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        grounded = false;
        gravityValue = player.gravityValue;
        jumpHeight = player.jumpHeight;
        playerSpeed = player.moveSpeed;
        gravityVelocity.y = 0;
        
        player.animator.SetFloat("MoveSpeed", 0f);
        Jump();
        InputManager.Instance.SetJumpInput(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;

        if (grounded)
        {
            stateMachine.ChangeState(player.LandingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!grounded)
        {
            velocity = player.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            Vector3 cameraForward = player.cameraTransform.forward;
            Vector3 cameraRight = player.cameraTransform.right;
            
            velocity = cameraForward * velocity.z + cameraRight * velocity.x;
            velocity.y = 0f;
            velocity.Normalize();
            
            airVelocity = cameraForward * airVelocity.z + cameraRight * airVelocity.x;
            airVelocity.y = 0f;
            airVelocity.Normalize();
            
            player.controller.Move(gravityVelocity * Time.deltaTime +
                                   (airVelocity * player.airControl + velocity * (1 - player.airControl)) * (playerSpeed * Time.deltaTime));
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = player.IsGrounded();
    }
    
    private void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}
