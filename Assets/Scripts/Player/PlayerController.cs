using System;
using System.Collections;
using Player.StateMachineScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    /*
     * This Player controller is using a state machine to handle conditions. The States are held and made in the StateCollection.
     * The input is handled in PlayerInputProcessor and InputReader(This removes unneeded data from the default new InputSystem)
     * The PlayerConfinuration/playerData hold all player values
     * The PlayerCollision handle the collision of the player
     *
     * Init
     * State machine and the state collection
     * MUST DEFINE STATE TRANSITIONS IN AWAKE
     * At(from,to,condition)
     * Any(to,condition)
     */
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
       # region Variables
       StateCollection _states;
       public PlayerInputProcessor InputProcessor;
       StateMachine _stateMachine;
       public PlayerLocomotion PlayerLocomotion;
       public bool triggerDialogue;

       [SerializeField] private Rigidbody rb;
       [SerializeField] private Input.InputReader inputReader;
       public PlayerConfiguration playerData;
       [SerializeField] private Animator animator;
       //[SerializeField] private PlayerCollision playerCollision;
       private SpriteRenderer _spriteRenderer;

       public bool delayB;
       #endregion

       private void Awake()
       {
           if (_instance == null)
           {
               _instance = this;
               DontDestroyOnLoad(this.gameObject);
           }
           else if (_instance != this) Destroy(this.gameObject);

           _stateMachine = new StateMachine();
           _states = new StateCollection(this,animator,playerData);
           InputProcessor = new PlayerInputProcessor(inputReader);
           PlayerLocomotion = new PlayerLocomotion(rb, playerData);
           //playerCollision.SetPlayerLocomotion(PlayerLocomotion);
           //define transitions
           //At(_states.LocomotionState,_states.JumpState, new FuncPredicate(()=> InputProcessor.IsJumping&&playerCollision.coyoteTimer>=0));
           
           
           //inital state
           _stateMachine.SetState(_states.IdleState);
           _spriteRenderer=animator.GetComponent<SpriteRenderer>();
       }
       void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
       void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

       private void Update()
       {
           _stateMachine.Update();
           if(!PlayerLocomotion.rightDirection)
               _spriteRenderer.flipX = true;
           else
               _spriteRenderer.flipX = false;
       }
       private void FixedUpdate()
       {
           _stateMachine.FixedUpdate();
       }
    }
}
