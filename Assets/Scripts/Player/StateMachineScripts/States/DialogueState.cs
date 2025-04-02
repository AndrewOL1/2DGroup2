using UnityEngine;

namespace Player.StateMachineScripts.States
{
    public class DialogueState : BaseState
    {
        public DialogueState(PlayerController player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            /*
            if(player.Bird)
                animator.CrossFade(BirdIdleHash,crossFadeDuration);
            else    
                animator.CrossFade(IdleHash,crossFadeDuration);
            player.PlayerLocomotion.ZeroVelocity();
            if (player.triggerDialogue == true)
            {
                player.triggerDialogue = false;
            }
            else
                player.PlayerLocomotion.StartDialogue();
            player.InputProcessor.SetDialogue();
            // maybe a delay for the animation
            */
        }

        public override void Update()
        {
            if (player.InputProcessor.NextDialogue)
            {
                //player.PlayerLocomotion.DialogueUpdate(player.InputProcessor.NextDialogue);
                player.InputProcessor.NextDialogue = false;
            }
        }

        public override void FixedUpdate()
        {
            
        }

        public override void OnExit()
        {
            player.InputProcessor.SetGameplay();
        }
    }
}