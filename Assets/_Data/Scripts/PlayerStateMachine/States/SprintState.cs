using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.PlayerStateMachine;

public class SprintState : GroundedState
{
    public SprintState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        playerSpeed = player.sprintSpeed;
        gravityValue = player.gravityValue;
        
        Debug.Log("Sprint");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;

        if (input.sqrMagnitude != 0)
        {
            player.animator.SetFloat("MoveSpeed", input.magnitude + 0.5f, player.speedDampTime, Time.deltaTime);
        }
        else
        {
            player.animator.SetFloat("MoveSpeed", input.magnitude, player.speedDampTime, Time.deltaTime);
        }

        if (!sprintInput)
        {
            stateMachine.ChangeState(player.StandingState);
        }
        
        if (jumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
