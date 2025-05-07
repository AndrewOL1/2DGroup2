using Player.StateMachineScripts.States;
using UnityEngine;

namespace Player.StateMachineScripts
{
    public class StateCollection
    {
        public IdleState IdleState { get; private set; }
        public TestState TestState { get; private set; }

        public StateCollection(PlayerController playerController, Animator animator,PlayerConfiguration playerConfiguration)
        { 
            IdleState = new IdleState(playerController, animator);
            TestState = new TestState(playerController, animator);
        }
    }
}