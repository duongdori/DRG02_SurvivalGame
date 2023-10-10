using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player.Manager;

namespace Scripts.Player.AnimationEvent
{
    public class AnimationEventTrigger : MonoBehaviour
    {

        public void AnimationFinishTrigger()
        {
            PlayerManager.Instance.player.AnimationFinishTrigger();
        }

        public void AnimationTrigger()
        {
            PlayerManager.Instance.player.AnimationTrigger();
        }
    
        public void EnableDamageCollider()
        {
            PlayerManager.Instance.weaponSlotManager.weaponDamageCollider.EnableDamageCollider();
        }
    
        public void DisableDamageCollider()
        {
            PlayerManager.Instance.weaponSlotManager.weaponDamageCollider.DisableDamageCollider();
        }
    }
}

