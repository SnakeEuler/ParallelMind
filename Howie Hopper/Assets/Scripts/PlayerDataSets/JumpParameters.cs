using UnityEngine;
namespace ParallelMinds {
[CreateAssetMenu (fileName = "JumpParameters", menuName = "ParallelMinds/JumpParameters")]
public class JumpParameters: ScriptableObject {
  [Header ("Height and Duration")]
  public float maxJumpHeight = 4f;
  public float maxJumpTime = 1f;

  [Header ("Jump Force")]
  public float jumpForce = 10f;
  public float jumpCutMultiplier = 0.5f;
  public float cutJumpDuration;

  [Header ("In-Air Control")]
  public float airControlMultiplier = .5f;

  [Header ("Gravity")]
  public float gravityScale = 1f;

  [Header ("Coyote Time")]
  public float coyoteTime = 0.2f;

  [Header ("Input Buffer")]
  public float inputBufferTime = 0.1f;

  [Header ("Multiple Jumps")]
  public int maxJumps = 1;

  [Header ("Jump Curves")]
  //This curve dictates the strength of the jump force applied over time during the upward phase of the jump.
  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]
  public AnimationCurve ascentCurve;
  //This curve controls how gravity affects the character during the falling phase of the jump.
  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 2, 5)]
  public AnimationCurve descentCurve;


  [Header ("In-Air Control")]
  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]// Customize these bounds as needed
  public AnimationCurve xSpeedUpCurve;

  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]
  public AnimationCurve xTurnBackCurve;

  [SerializeField, Space (5)]
  [BoundCurve (0, 0, 1, 1, 5)]
  public AnimationCurve xSlowDownCurve;

}
}
