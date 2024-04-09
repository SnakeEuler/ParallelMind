using System;
using UnityEngine;
using static UnityEngine.Vector3;

namespace ParallelMinds {
public class PlayerMovementController: MonoBehaviour {
  public Rigidbody rb;
  public PlayerData playerData;
  public JumpParameters jumpParameters;
  public PlayerInputHandler playerInputHandler;
  public ParallelStateMachine fsm;
  public SurfaceChecker surfaceChecker;

  public bool grounded;
  public Transform orientation;

  [SerializeField]
  private string currentStateName;
  private float currentSpeed;

  // Initializes the Awake function,
  // fwhich calls InitializeStateMachine and sets the Rigidbody constraints to FreezeRotationX and FreezeRotationZ.
  private void Awake () {
    InitializeStateMachine ();
    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
  }
  // Update method that updates the finite state machine and sets the currentStateName
  // variable to the name of the current state if it exists.
  private void Update () {
    fsm.Update ();
    currentStateName = fsm.GetCurrentState ()?.stateName.ToString ();
  }

  // A description of the entire function, its parameters, and its return types.
  private void FixedUpdate () {
    fsm.FixedUpdate ();
    var desiredUpDirection = surfaceChecker.groundNormal;
    if (rb.velocity.magnitude > 0.1f) {
      Lerp (desiredUpDirection, rb.velocity.normalized, playerData.tiltFactor);
    }
    // Smoothly rotate towards the desired orientation
    var targetRotation = Quaternion.LookRotation (transform.forward, desiredUpDirection);
    transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation,
    Time.deltaTime * playerData.rotationSpeed);

    SurfaceCheckResult groundCheck = surfaceChecker.CheckSurfaceRay (transform.position,
    Vector3.down, playerData.checkDepth, playerData.groundLayer);
    grounded = groundCheck.isHit;
  }
  // Initialize the state machine by adding states and changing the initial state.
  private void InitializeStateMachine () {
    fsm = gameObject.AddComponent<ParallelStateMachine> ();

    var idleState = new IdleState (this);
    var walkState = new WalkState (this);
    var jumpState = new JumpState (this);

    fsm.AddState (idleState.stateName, idleState);
    fsm.AddState (walkState.stateName, walkState);
    fsm.AddState (jumpState.stateName, jumpState);

    fsm.ChangeState (idleState);
  }
  // Apply drag based on whether the player is grounded or in the air.
  private void ApplyDrag () {
    float drag = grounded? playerData.groundDrag : playerData.airDrag;
    rb.drag = drag;
  }
}
}
