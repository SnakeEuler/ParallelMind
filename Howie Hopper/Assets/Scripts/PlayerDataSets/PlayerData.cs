using UnityEngine;

namespace ParallelMinds {
[CreateAssetMenu (fileName = "PlayerData", menuName = "ParallelMinds/PlayerData")]
public class PlayerData: ScriptableObject {
  // Existing InputChannel setup ...

  [Header ("Movement Parameters")]
  public float maxSpeed;
  [Range (0f, 1f)]
  public float accelerationTime;
  [Range (0f, 1f)]
  public float decelerationTime;
  [Range (0f, 1f)]
  public float reverseTime;

  public AnimationCurve accelerationCurve;
  public AnimationCurve decelerationCurve;
  public AnimationCurve reverseCurve;
  public double directionChangeThreshold;

  [Header ("Ground Check Parameters")]
  public float playerHeight;
  public float checkDepth;
  public LayerMask groundLayer;
  public float groundCheckOffset;

  [Header ("Physics Parameters")]
  public float groundDrag = 3;
  public float airDrag = 0;
  public float dragAdjustmentRate = 5;

  [Header ("Camera")]
  public float baseTiltAngle = 15f;// degrees

  [Header ("Tilt Settings")]
  public float tiltFactor = 0.1f;
  public float rotationSpeed = 5f;

  // [Header ("Path Movement (Experimental)")]
  // public float defaultPathSpeed = 5f;
  // [SerializeField]


  // public float checkDistance;
  // public LayerMask groundLayerMask;
}
}
