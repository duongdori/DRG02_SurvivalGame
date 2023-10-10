using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.PlayerStateMachine;

public class AttackWeaponState : State
{
    private int numberOfAttacks = 2;
    private int currentAttackCounter = 0;
    private float attackCounterResetCooldown = 0.3f;
    private float targetTime;
    public AttackWeaponState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (startTime >= targetTime)
        {
            ResetAttackCounter();
        }
        
        player.animator.SetInteger("AttackCombo", currentAttackCounter);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;

        if (isAnimationFinished)
        {
            currentAttackCounter++;
            stateMachine.ChangeState(player.StandingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        if (currentAttackCounter >= numberOfAttacks)
        {
            ResetAttackCounter();
        }

        targetTime = Time.time + attackCounterResetCooldown;
    }

    private void ResetAttackCounter()
    {
        currentAttackCounter = 0;
    }
    
}
