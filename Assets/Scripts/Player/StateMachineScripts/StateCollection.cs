using Player.StateMachineScripts.States;
using UnityEngine;

namespace Player.StateMachineScripts
{
    public class StateCollection
    {
        public IdleState IdleState { get; private set; }
        
        public DialogueState DialogueState { get; private set; }

        public StateCollection(PlayerController playerController, Animator animator,PlayerConfiguration playerConfiguration)
        { 
            IdleState = new IdleState(playerController, animator);

            DialogueState = new DialogueState(playerController, animator);
        }
    }
}