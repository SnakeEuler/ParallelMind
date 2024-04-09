using UnityEngine;

namespace ParallelMinds {
public class JumpState: State {

  //Member variables
  private readonly PlayerMovementController playerMovementController;
  private readonly JumpParameters jumpParameters;
  private readonly Rigidbody rb;

  //State Tracking Variables
  private float jumpStartTime;
  public bool jumpInputHeld;
  private bool coyoteTimeActive;
  private float coyoteTimeCounter;
  private float cutJumpTime;

  //Input Handling Variables
  private float jumpButtonReleaseTimer;
  private const float MinReleaseDuration = 0.1f;

  // Phase enum for jump state
  private enum JumpPhase { SpeedUp, SlowDown, TurnBack, Null }
  private JumpPhase phase;

  //Air Control Variables
  private float xCurveTime;
  private bool jumpInputReleased;

  //Constructor
  public JumpState (PlayerMovementController controller): base (StateName.Jump, controller.fsm) {
    playerMovementController = controller;
    jumpParameters = controller.jumpParameters;
    rb = controller.rb;

    // Transitions
    transitions[StateName.Walk] = ()
        => playerMovementController.grounded
        && playerMovementController.playerInputHandler.MoveDirection != Vector2.zero;
    transitions[StateName.Idle] = ()
        => playerMovementController.grounded
        && playerMovementController.playerInputHandler.MoveDirection == Vector2.zero;
  }

  public override void EnterState () {
    base.EnterState ();
    ResetJumpState ();
  }
  //Helper Functions
  private void ResetJumpState () {
    jumpStartTime = Time.time;
    coyoteTimeActive = true;
    coyoteTimeCounter = 0f;
    cutJumpTime = 0f;
    jumpInputReleased = false;
    jumpInputHeld = true;
    xCurveTime = 0f;
    phase = JumpPhase.Null;
    ApplyInitialJumpForce ();
  }
  public override void UpdateState () {
    HandleInput ();
    HandleStateChange ();
  }

  public override void FixedUpdateState () {
    ApplyGravityAdjustments ();
    ApplyAirControl ();
    HandleCoyoteTime ();
  }

  private void ApplyInitialJumpForce () {
    float timeRatio = Mathf.Clamp01 ((Time.time - jumpStartTime) / jumpParameters.maxJumpTime);
    float curveMultiplier = jumpParameters.ascentCurve.Evaluate (timeRatio);
    float jumpStrength = jumpParameters.jumpForce * curveMultiplier;

    rb.AddForce (Vector3.up * jumpStrength, ForceMode.Impulse);
  }

  private void ApplyGravityAdjustments () {
    float heightRatio = Mathf.Clamp01 (fsm.transform.position.y / jumpParameters.maxJumpHeight);
    float curveMultiplier = jumpParameters.descentCurve.Evaluate (heightRatio);
    float gravityMultiplier = jumpParameters.gravityScale * curveMultiplier;

    rb.AddForce (Physics.gravity * gravityMultiplier * Time.fixedDeltaTime, ForceMode.Acceleration);
    // Jump cut logic:
    if (cutJumpTime != 0f) {
      float timeSinceCut = Time.time - cutJumpTime;
      float timeRatio = timeSinceCut / jumpParameters.cutJumpDuration;

      if (timeRatio <= 1f) {
        gravityMultiplier *= jumpParameters.jumpCutMultiplier;
      } else {
        // Reset cut if the duration has passed
        cutJumpTime = 0f;
      }
    }
  }

  private void ApplyAirControl () {
    if (!playerMovementController.grounded) {
      Vector3 currentVelocity = rb.velocity;
      Vector3 horizontalMovementDirection
          = playerMovementController.playerInputHandler.MoveDirection;

      // Phase determination
      if (horizontalMovementDirection != Vector3.zero) {
        if (phase == JumpPhase.Null
          || Mathf.Sign (Vector3.Dot (currentVelocity, horizontalMovementDirection)) <= 0) {
          phase = JumpPhase.SpeedUp;
          xCurveTime = 0f;
        } else if (phase == JumpPhase.SpeedUp
          && Mathf.Sign (Vector3.Dot (currentVelocity, horizontalMovementDirection)) > 0) {
          phase = JumpPhase.TurnBack;
          // Use your curve-related code to get time from TurnBack Curve
          xCurveTime = CorePhysics.SetCurveTimeByValue (jumpParameters.xTurnBackCurve,
          Mathf.Abs (currentVelocity.x) / playerMovementController.playerData.maxSpeed,
          jumpParameters.maxJumpTime);
        }
      } else {
        phase = JumpPhase.SlowDown;
        xCurveTime = 0f;
      }
      // Calculate target velocity based on phase
      // Smoothing when existing velocity is present
      if (phase == JumpPhase.SpeedUp && rb.velocity.magnitude > 0.1f) {
        float currentSpeedRatio
            = rb.velocity.magnitude / playerMovementController.playerData.maxSpeed;
        xCurveTime = CorePhysics.SetCurveTimeByValue (jumpParameters.xSpeedUpCurve,
        currentSpeedRatio, jumpParameters.maxJumpTime);
      }
      float targetSpeed = 0f;
      switch (phase) {
        case JumpPhase.SpeedUp:
          xCurveTime += Time.fixedDeltaTime;
          targetSpeed
              = jumpParameters.xSpeedUpCurve.Evaluate (xCurveTime / jumpParameters.maxJumpTime)
              * playerMovementController.playerData.maxSpeed;
          break;
        case JumpPhase.TurnBack:
          xCurveTime += Time.fixedDeltaTime;
          targetSpeed
              = jumpParameters.xTurnBackCurve.Evaluate (xCurveTime / jumpParameters.maxJumpTime)
              * playerMovementController.playerData.maxSpeed;
          break;
        case JumpPhase.SlowDown:
          xCurveTime += Time.fixedDeltaTime;
          targetSpeed
              = jumpParameters.xSlowDownCurve.Evaluate (xCurveTime / jumpParameters.maxJumpTime)
              * playerMovementController.playerData.maxSpeed;
          break;
      }
      Vector3 targetVelocity = horizontalMovementDirection * targetSpeed;

      // Air control based on jump parameters
      float airControlMultiplier = jumpParameters.airControlMultiplier;

      rb.velocity = Vector3.Lerp (currentVelocity, targetVelocity,
      airControlMultiplier * Time.fixedDeltaTime);
    }
  }


  private void HandleInput () {
    bool jumpButtonPressedThisFrame = playerMovementController.playerInputHandler.jumpInputPressed;
    bool jumpButtonReleasedThisFrame = !jumpButtonPressedThisFrame;

    if (jumpButtonPressedThisFrame) {
      if (jumpInputReleased && coyoteTimeActive) {
        ResetJumpState ();
      }
    } else {
      jumpButtonReleaseTimer += Time.deltaTime;
      if (jumpButtonReleaseTimer >= MinReleaseDuration) {
        jumpInputReleased = true;
        jumpButtonReleaseTimer = 0f;// Reset timer
      }
    }
  }

  private void HandleCoyoteTime () {
    coyoteTimeCounter += Time.deltaTime;
    coyoteTimeActive = coyoteTimeCounter <= jumpParameters.coyoteTime;
  }

  public void HandleStateChange () {
    foreach (var transition in transitions) {
      if (transition.Value () && jumpInputReleased) {
        fsm.ChangeState (transition.Key);
        break;
      }
    }
  }
}
}
