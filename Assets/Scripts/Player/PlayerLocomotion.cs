using UnityEngine;

namespace Player
{
    public class PlayerLocomotion
    {
        /*
         * The goal is Handle all movement related actions for the player
         */
        #region Variables
        private Rigidbody rb;
        PlayerConfiguration playerconfig; 
        public bool rightDirection=true;
        //interact
        //public DialogueTrigger interactingObject;
        
        #endregion
        public PlayerLocomotion(Rigidbody rb, PlayerConfiguration playerconfig)
        {
            this.rb = rb;
            this.playerconfig = playerconfig;
        }

        public void GroundedVelocityMovement(float velocityInput)
        {
        }
        


        public void ZeroVelocity()
        {
            rb.velocity= Vector3.zero;
        }
        //Dialogue
        /*public void StartDialogue()
        {
            interactingObject.TriggerDialogue();
            if (interactingObject.GetComponent<SpawnMoveablePlatform>() != null)
            {
                interactingObject.GetComponent<SpawnMoveablePlatform>().Spawn();
            }
        }

        public void DialogueUpdate(bool newDialogue)
        {
            if (newDialogue)
            {
                DialogueManager.instance.DisplayNextDialogueLine();
            }
        }
        */
    }
}