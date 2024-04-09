using System;
using ParallelMinds;
using UnityEngine;

namespace ParallelMinds {
public class WalkState: State {
  private PlayerMovementController pmc;
  private PlayerInputHandler inputHandler;
  private PlayerData playerData;
  private Rigidbody rb;

  private MovementPhase phase;
  private float curveTime;

  // Constructor for WalkState class with parameter controller of type PlayerMovementController.
  public WalkState (PlayerMovementController controller): base (StateName.Walk, controller.fsm) {
    pmc = controller;
    inputHandler = controller.playerInputHandler;
    playerData = controller.playerData;
    rb = controller.rb;


    transitions[StateName.Idle] = () => inputHandler.MoveDirection == Vector2.zero;
    transitions[StateName.Jump] = () => pmc.playerInputHandler.jumpInputPressed;
  }

  public override void EnterState () {
    curveTime = 0f;
    phase = MovementPhase.Accelerating;
  }

  public override void UpdateState () {
    HandleStateChange ();
    if (pmc.surfaceChecker.grounded) {
      ApplyWalkingMovement ();
    }
  }

  public void HandleStateChange () {
    // Handle transitions
    foreach (var transition in transitions) {
      if (transition.Value ()) {
        fsm.ChangeState (transition.Key);
        break;
      }
    }
  }
  public override void FixedUpdateState () {
    curveTime += Time.fixedDeltaTime;
    UpdatePhaseAndVelocity ();
  }

  private void ApplyWalkingMovement () {
    var isMoving = inputHandler.MoveDirection != Vector2.zero;

    // Convert the 2D movement direction to a 3D vector
    var inputDirection
        = new Vector3 (inputHandler.MoveDirection.x, 0, inputHandler.MoveDirection.y);

    // Transform the input direction by the player's orientation
    var orientedDirection = pmc.orientation.TransformDirection (inputDirection).normalized;

    var speedFromCurve = GetCurrentSpeedFromCurve ();
    var desiredVelocity = isMoving? orientedDirection * speedFromCurve : Vector3.zero;

    // Apply the desired velocity, preserving the existing y-axis velocity
    rb.velocity = new Vector3 (desiredVelocity.x, rb.velocity.y, desiredVelocity.z);
  }


  // Update the phase of movement and adjust velocity accordingly
  private void UpdatePhaseAndVelocity () {
    if (IsChangingDirection ()) {
      phase = MovementPhase.Reversing;
      curveTime = 0f;
    }

    switch (phase) {
      case MovementPhase.Accelerating:
        if (curveTime > playerData.accelerationTime) {
          phase = MovementPhase.Decelerating;
          curveTime = 0f;
        }
        break;
      case MovementPhase.Decelerating:
        break;
      case MovementPhase.Reversing:
        if (curveTime > playerData.reverseTime) {
          phase = MovementPhase.Accelerating;
          curveTime = 0f;
        }
        break;
      default:
        throw new ArgumentOutOfRangeException ();
    }
  }

  // Check if the player is changing direction
  private bool IsChangingDirection () {
    float currentDirection = Mathf.Sign (rb.velocity.x);
    float inputDirection = Mathf.Sign (inputHandler.MoveDirection.x);
    return Math.Abs (currentDirection - inputDirection) > 0.1
        && rb.velocity.magnitude > playerData.directionChangeThreshold;
  }

  // Calculate the current speed from the appropriate curve based on the current MovementPhase
  private float GetCurrentSpeedFromCurve () {
    return phase switch {
      MovementPhase.Accelerating => playerData.accelerationCurve.Evaluate (curveTime
      / playerData.accelerationTime) * playerData.maxSpeed,
      MovementPhase.Decelerating => playerData.decelerationCurve.Evaluate (curveTime
      / playerData.decelerationTime) * playerData.maxSpeed,
      MovementPhase.Reversing => playerData.reverseCurve.Evaluate (curveTime
      / playerData.reverseTime) * playerData.maxSpeed,
      _ => 0f,
    };
  }
}
}
