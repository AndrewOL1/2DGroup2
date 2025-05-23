using UnityEngine;

namespace Player.StateMachineScripts.States
{
    /*
     * The Base of all States
     */
    public abstract class BaseState : IState
    {
        protected readonly PlayerController player;
        protected readonly Animator animator;
        
        protected static readonly int LocomotionHash = Animator.StringToHash("Walking");
        protected static readonly int JumpHash = Animator.StringToHash("Cat_Jumping");
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int BirdLocomotionHash = Animator.StringToHash("Walking_Bird");
        protected static readonly int BirdJumpHash = Animator.StringToHash("Jumping_Bird");
        protected static readonly int BirdIdleHash = Animator.StringToHash("Idle_Bird");

        protected const float crossFadeDuration = 0.1f;

        protected BaseState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }
        
        
        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void OnExit()
        {
            //noop
        }
    }
}