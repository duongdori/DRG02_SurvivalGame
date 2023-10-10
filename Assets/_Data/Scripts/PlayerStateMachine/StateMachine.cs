using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerStateMachine
{
    public class StateMachine
    {
        private State currentState;
        public State CurrentState => currentState;
    
        public void Initialize(State startingState)
        {
            currentState = startingState;
            currentState.Enter();
        }
        public void ChangeState(State newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}

