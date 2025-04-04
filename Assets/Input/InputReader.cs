using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
    public class InputReader : ScriptableObject,PlayerControls.IPausedActions,PlayerControls.IGameplayActions
    {
        private PlayerControls _inputs;

        public void OnEnable()
        {
            if (_inputs == null)
            {
                _inputs = new PlayerControls();
                _inputs.Gameplay.SetCallbacks(this);
                _inputs.Paused.SetCallbacks(this);
                SetGameplay();
            }
        }
        # region InputStates
        public void SetGameplay()
        {
            _inputs.Gameplay.Enable();
            _inputs.Paused.Disable();
        }
        public void SetPause()
        {
            _inputs.Gameplay.Disable();
            _inputs.Paused.Enable();
        }
        # endregion
        # region PublicEvents

        public event Action PauseEvent;
        public event Action ResumeEvent;

        public event Action<Vector2> LookEvent;
        public event Action InteractEvent;
        public event Action InteractEventCancelled;
        # endregion
        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;
            ResumeEvent?.Invoke();
            SetGameplay();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;
            InteractEvent?.Invoke();
        }
        public void OnInteractCancelled(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Canceled) return;
            InteractEvent?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;
            PauseEvent?.Invoke();
            SetPause();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            LookEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
