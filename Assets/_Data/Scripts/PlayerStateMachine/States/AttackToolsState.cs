using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.Player.Manager;
using Scripts.PlayerStateMachine;

public class AttackToolsState : State
{
    public AttackToolsState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        string animName = PlayerManager.Instance.weaponSlotManager.GetItemOnRightHand().itemName;
        player.animator.SetTrigger(animName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.StandingState);
        }
    }
}
