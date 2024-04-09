using UnityEngine;

namespace ParallelMinds {
public interface ISurfaceChecker {
  bool IsGrounded { get; }
  Vector3 GroundNormal { get; }
  bool CheckGround (Vector3 raycastOrigin);
}
}
