using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.PlayerStateMachine;

public class State
{
    protected StateMachine stateMachine;
    protected Player player;
    
    #region Animation Variables
    
    protected bool isAnimationFinished;
    private string _animBoolName;
    
    #endregion
    
    #region Other Variables
    
    protected bool isExitingState;
    protected float startTime;
    
    #endregion

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;
    protected bool jumpInput;
    protected bool sprintInput;
    protected bool isGrounded;
    public State(StateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        _animBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        player.animator.SetBool(_animBoolName, true);
        Debug.Log(_animBoolName);
    }
    
    public virtual void Exit()
    {
        isExitingState = true;
        player.animator.SetBool(_animBoolName, false);
    }
    
    public virtual void LogicUpdate()
    {
        input = InputManager.Instance.GetMoveInput();
        jumpInput = InputManager.Instance.JumpInput;
        sprintInput = InputManager.Instance.SprintInput;

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    protected virtual void DoChecks()
    {
        isGrounded = player.IsGrounded();
    }
    public virtual void AnimationTrigger()
    {
        
    }
    
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
