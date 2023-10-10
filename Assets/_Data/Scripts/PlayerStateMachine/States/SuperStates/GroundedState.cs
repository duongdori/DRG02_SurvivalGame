using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.PlayerStateMachine;

public class GroundedState : State
{
    protected float gravityValue;
    protected Vector3 currentVelocity;
    protected float playerSpeed;
    protected Vector3 cVelocity;
    
    public GroundedState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        velocity = new Vector3(input.x, 0f, input.y);
        velocity = player.cameraTransform.forward * velocity.z + player.cameraTransform.right * velocity.x;
        velocity.y = 0f;
        velocity.Normalize();
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        gravityVelocity.y += gravityValue * Time.deltaTime;
        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
        
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity,ref cVelocity, player.velocityDampTime);
        player.controller.Move(currentVelocity * (Time.deltaTime * playerSpeed) + gravityVelocity * Time.deltaTime);
  
        if (velocity.sqrMagnitude > 0)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(velocity),player.rotationDampTime);
        }
    }
    
    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f;
        player.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            player.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
