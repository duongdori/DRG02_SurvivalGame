using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player;
using Scripts.PlayerStateMachine;

public class LandingState : State
{
    private float timePassed;
    private float landingTime;
    public LandingState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0f;
        landingTime = 0.1f;
    }
    
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        if (timePassed > landingTime)
        {
            stateMachine.ChangeState(player.StandingState);
        }
        timePassed += Time.deltaTime;
    }
}
