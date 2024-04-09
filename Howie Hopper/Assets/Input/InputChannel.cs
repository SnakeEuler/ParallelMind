using System;
using ParallelRelay;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu (fileName = "InputChannel", menuName = "ParallelMinds/Input Channel")]
public class InputChannel: ScriptableObject, InputActions.IPlayerActions {
  public InputActions inputActions;

  private ParallelRelay<Vector2> walkStarted = new ParallelRelay<Vector2> ();
  private ParallelRelay<Vector2> walkPerformed = new ParallelRelay<Vector2> ();
  private ParallelRelay<Vector2> walkCanceled = new ParallelRelay<Vector2> ();

  private ParallelRelay<bool> jumpStarted = new ParallelRelay<bool> ();
  private ParallelRelay<bool> jumpCanceled = new ParallelRelay<bool> ();

  // A method that handles the callback for the OnWalk action,
  // dispatching events based on the phase of the input action.
  public void OnWalk (InputAction.CallbackContext context) {
    switch (context.phase) {
      case InputActionPhase.Started:
        walkStarted.Dispatch (context.ReadValue<Vector2> ());
        break;
      case InputActionPhase.Performed:
        walkPerformed.Dispatch (context.ReadValue<Vector2> ());
        break;
      case InputActionPhase.Canceled:
        walkCanceled.Dispatch (context.ReadValue<Vector2> ());
        break;
      case InputActionPhase.Disabled: break;
      case InputActionPhase.Waiting: break;
      default: throw new ArgumentOutOfRangeException ();
    }
  }
  // A method that handles the callback from a 2D walk input action.
  // It dispatches events based on the phase of the input action.
  public void OnWalk2D (InputAction.CallbackContext context) {
    switch (context.phase) {
      case InputActionPhase.Started:
        walkStarted.Dispatch (context.ReadValue<Vector2> ());
        break;
      case InputActionPhase.Performed:
        walkPerformed.Dispatch (context.ReadValue<Vector2> ());
        break;
      case InputActionPhase.Canceled:
        walkCanceled.Dispatch (context.ReadValue<Vector2> ());
        break;
      case InputActionPhase.Disabled: break;
      case InputActionPhase.Waiting: break;
      default: throw new ArgumentOutOfRangeException ();
    }
  }
  // A method that handles the jump action when triggered by the player input.
  public void OnJump (InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      jumpStarted.Dispatch (true);
    }
    if (context.phase == InputActionPhase.Canceled) {
      jumpCanceled.Dispatch (true);
    }
  }

  private void OnEnable () {
    if (inputActions == null) {
      inputActions = new InputActions ();
      inputActions.Player.SetCallbacks (this);
    }
    inputActions.Player.Enable ();
  }

  private void OnDisable () { inputActions.Player.Disable (); }


  //WALK EVENTS
  #region Walk Events

  public void AddWalkStartedListener (Action<Vector2> listener) {
    walkStarted.AddListener (listener);
  }

  public void AddWalkPerformedListener (Action<Vector2> listener) {
    walkPerformed.AddListener (listener);
  }

  public void AddWalkCanceledListener (Action<Vector2> listener) {
    walkCanceled.AddListener (listener);
  }

  public void RemoveWalkStartedListener (Action<Vector2> listener) {
    walkStarted.RemoveListener (listener);
  }

  public void RemoveWalkPerformedListener (Action<Vector2> listener) {
    walkPerformed.RemoveListener (listener);
  }

  public void RemoveWalkCanceledListener (Action<Vector2> listener) {
    walkCanceled.RemoveListener (listener);
  }

  #endregion

  //JUMP EVENTS
  #region Jump Events

  public void AddJumpStartedListener (Action<bool> listener) { jumpStarted.AddListener (listener); }
  public void AddJumpCanceledListener (Action<bool> listener) {
    jumpCanceled.AddListener (listener);
  }
  public void RemoveJumpStartedListener (Action<bool> listener) {
    jumpStarted.RemoveListener (listener);
  }
  public void RemoveJumpCanceledListener (Action<bool> listener) {
    jumpCanceled.RemoveListener (listener);
  }

  #endregion
}
