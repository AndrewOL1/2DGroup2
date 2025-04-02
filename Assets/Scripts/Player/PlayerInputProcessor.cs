using UnityEngine;

namespace Player
{
    public class PlayerInputProcessor
    {
        /*
         * Handle InputReaders fucntion to Player Controller
         */
        Input.InputReader _input;
        public Vector2 MousePos;
        public bool IsJumping;
        public float JumpTime;
        public bool IsInteracting;
        public bool NextDialogue=false;

        public PlayerInputProcessor(Input.InputReader input)
        {
            this._input = input;
            _input.LookEvent += HandleMove;
            _input.PauseEvent += HandlePause;
            _input.InteractEvent += HandleInteract;
            //_input.InteractCanceledEvent += HandleCancelledInteract;
            _input.ResumeEvent += HandleContinue;
        }


        #region Handlers
        private void HandleContinue()
        {
            Debug.Log("Continue");
            NextDialogue=true;
        }
        private void HandleInteract()
        {
            IsInteracting = true;
        }
        private void HandleCancelledInteract()//not sure if i will need this
        {
            IsInteracting = false;
        }
        private void HandlePause()
        {
            throw new System.NotImplementedException();//later
        }

        private void HandleMove(Vector2 mosPosition)
        {
            MousePos = mosPosition;
        }
        #endregion
        #region Active Control Scheme
        public void SetGameplay()=>_input.SetGameplay();
        public void SetPaused()=> _input.SetPause();

        public void SetDialogue()
        {
           // _input.SetDialogue();
        }

        #endregion
    }
}