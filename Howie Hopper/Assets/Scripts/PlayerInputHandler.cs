using UnityEngine;
using EasyButtons;
namespace ParallelMinds {
public class PlayerInputHandler: MonoBehaviour {
  [SerializeField]
  private InputChannel inputChannel;
  [SerializeField]
  private float horizontalInput;
  [SerializeField]
  private float verticalInput;
  [SerializeField]
  private Vector2 moveDirection;

  // Toggles between 2D and 3D mode for the button.
  [Button]
  public void turnOn2D () {
    is2D = !is2D;
    switch (is2D) {
      case true:
        inputChannel.inputActions.Player.Walk.Disable ();
        inputChannel.inputActions.Player.Walk2D.Enable ();
        break;
      case false:
        inputChannel.inputActions.Player.Walk.Enable ();
        inputChannel.inputActions.Player.Walk2D.Disable ();
        break;
    }

  }
  public bool is2D;

  public bool jumpInputPressed;

  // A method that subscribes to input events when the object is enabled.
  private void OnEnable () { SubscribeToInputEvents (); }

  // A method that is called when the object is disabled. It unsubscribes from input events.
  private void OnDisable () { UnsubscribeFromInputEvents (); }

  // Subscribes to various input events such as walk started,
  // walk performed, walk canceled, jump started, and jump canceled.
  private void SubscribeToInputEvents () {
    inputChannel.AddWalkStartedListener (OnMoveInput);
    inputChannel.AddWalkPerformedListener (OnMoveInput);
    inputChannel.AddWalkCanceledListener (OnMoveInput);
    inputChannel.AddJumpStartedListener (OnJumpStarted);
    inputChannel.AddJumpCanceledListener (OnJumpCanceled);
  }

  // Unsubscribes from all input events to clean up listeners.
  private void UnsubscribeFromInputEvents () {
    inputChannel.RemoveWalkStartedListener (OnMoveInput);
    inputChannel.RemoveWalkPerformedListener (OnMoveInput);
    inputChannel.RemoveWalkCanceledListener (OnMoveInput);
    inputChannel.RemoveJumpStartedListener (OnJumpStarted);
    inputChannel.RemoveJumpCanceledListener (OnJumpCanceled);
  }

  // A function that sets the jumpInputPressed variable to true when the jump action is started.
  private void OnJumpStarted (bool input) { jumpInputPressed = true; }

  // A method that handles when the jump action is canceled.
  private void OnJumpCanceled (bool input) { jumpInputPressed = false; }

  // A method that handles the input for movement, taking a Vector2 input parameters
  private void OnMoveInput (Vector2 input) {
    var inputDirection = new Vector3 (input.x, 0.0f, input.y);
    horizontalInput = inputDirection.x;
    verticalInput = inputDirection.z;
    moveDirection = new Vector2 (horizontalInput, verticalInput).normalized;
  }
  public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
  public float VerticalInput { get => verticalInput; set => verticalInput = value; }
  public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
}
}
