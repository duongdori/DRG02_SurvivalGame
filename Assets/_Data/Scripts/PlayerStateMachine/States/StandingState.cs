using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

using Scripts.Player;
using Scripts.Player.Manager;
using Scripts.PlayerStateMachine;

public class StandingState : GroundedState
{
    public StandingState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        jumpInput = false;
        sprintInput = false;
        
        input = Vector2.zero;
        
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        playerSpeed = player.moveSpeed;
        gravityValue = player.gravityValue;
        
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isExitingState) return;
        
        if (PlayerManager.Instance.weaponSlotManager.HasItemOnRightHand())
        {
            player.animator.runtimeAnimatorController = player.overrideController;
        }
        else
        {
            player.animator.runtimeAnimatorController = player.originalController;
        }
        
        player.animator.SetFloat("MoveSpeed", input.magnitude, player.speedDampTime, Time.deltaTime);

        if (sprintInput)
        {
            stateMachine.ChangeState(player.SprintState);
        }
        
        if (jumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() 
                                        && PlayerManager.Instance.weaponSlotManager.HasItemOnRightHand())
        {
            InventoryItemData itemData = PlayerManager.Instance.weaponSlotManager.GetItemOnRightHand();
            
            if (itemData.itemType == ItemType.Tools)
            {
                stateMachine.ChangeState(player.AttackToolsState);
            }
            else if (itemData.itemType == ItemType.Weapon)
            {
                stateMachine.ChangeState(player.AttackWeaponState);
            }
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
