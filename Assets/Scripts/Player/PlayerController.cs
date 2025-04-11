using System;
using System.Collections;
using Player.StateMachineScripts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using IState = Player.StateMachineScripts.IState;
using StateMachine = Player.StateMachineScripts.StateMachine;

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
        public static PlayerController Instance;
       # region Variables
       StateCollection _states;
       public PlayerInputProcessor InputProcessor;
       StateMachine _stateMachine;
       public PlayerLocomotion PlayerLocomotion;
       public bool triggerDialogue;
       [SerializeField] private Input.InputReader inputReader;
       public PlayerConfiguration playerData;
       [SerializeField] private Animator animator;
       //[SerializeField] private PlayerCollision playerCollision;
       private SpriteRenderer _spriteRenderer;
       [SerializeField] private int itemId;
       public bool delayB;
       #endregion

       private void Awake()
       {
           if (Instance == null)
           {
               Instance = this;
               DontDestroyOnLoad(this.gameObject);
           }
           else if (Instance != this) Destroy(this.gameObject);

           _stateMachine = new StateMachine();
           _states = new StateCollection(this,animator,playerData);
           InputProcessor = new PlayerInputProcessor(inputReader);
           PlayerLocomotion = new PlayerLocomotion(playerData);
           //playerCollision.SetPlayerLocomotion(PlayerLocomotion);
           //define transitions
           //At(_states.LocomotionState,_states.JumpState, new FuncPredicate(()=> InputProcessor.IsJumping&&playerCollision.coyoteTimer>=0));
           At(_states.IdleState,_states.DialogueState, new FuncPredicate(()=> InputProcessor.IsJumping));//don't know if needed
           At(_states.IdleState,_states.LetterState, new FuncPredicate(()=> itemId==1));
           At(_states.IdleState,_states.KeyState, new FuncPredicate(()=> itemId==2));
           At(_states.IdleState,_states.CogState, new FuncPredicate(()=> itemId==3));
           At(_states.KeyState,_states.LetterState, new FuncPredicate(()=> itemId==1));
           At(_states.CogState,_states.LetterState, new FuncPredicate(()=> itemId==1));
           At(_states.LetterState,_states.KeyState, new FuncPredicate(()=> itemId==2));
           At(_states.CogState,_states.KeyState, new FuncPredicate(()=> itemId==2));
           At(_states.LetterState,_states.CogState, new FuncPredicate(()=> itemId==3));
           At(_states.KeyState,_states.CogState, new FuncPredicate(()=> itemId==3));
           At(_states.LetterState,_states.IdleState, new FuncPredicate(()=> itemId==0));
           At(_states.KeyState,_states.IdleState, new FuncPredicate(()=> itemId==0));
           At(_states.CogState,_states.IdleState, new FuncPredicate(()=> itemId==0));
           
           
           //inital state
           _stateMachine.SetState(_states.IdleState);
           _spriteRenderer=animator.GetComponent<SpriteRenderer>();
       }
       void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
       void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

       private void Start()
       {
           SetActiveItem(0);
       }

       private void Update()
       {
           _stateMachine.Update();
       }
       private void FixedUpdate()
       {
           _stateMachine.FixedUpdate();
       }

       public void SetActiveItem(int id)
       {
           if (id == itemId)
           {
               itemId = 0;
               playerData.id = 0;
           }
           else
           {
               itemId = id;
               playerData.id = id;
           }
       }
    }
}
