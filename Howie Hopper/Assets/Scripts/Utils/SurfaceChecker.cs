using UnityEngine;

namespace ParallelMinds {
public enum CastShape { Sphere, Box, Ray }

public struct SurfaceCheckResult {
  public bool isHit;
  public RaycastHit hitInfo;
  public Vector3 point;
  public Vector3 normal;
  public float distance;
}

public class SurfaceChecker: MonoBehaviour {
  [SerializeField]
  private PlayerData playerData;
  public bool grounded;
  public Vector3 groundNormal = Vector3.up;
  public LayerMask[] surfaceCheckLayer;

  public SurfaceCheckResult CheckSurfaceRay (
  Vector3 raycastOrigin, Vector3 direction, float distance, LayerMask layerMask) {
    if (Physics.Raycast (raycastOrigin, direction, out RaycastHit hitInfo, distance, layerMask)) {
      return new SurfaceCheckResult {
        isHit = true,
        hitInfo = hitInfo,
        point = hitInfo.point,
        normal = hitInfo.normal,
        distance = hitInfo.distance
      };
    } else {
      return new SurfaceCheckResult { isHit = false };// Or a default empty result
    }
  }


  public SurfaceCheckResult CheckSurfaceSphere (
  Vector3 origin, float radius, Vector3 direction, float distance, LayerMask layerMask) {
    if (Physics.SphereCast (origin, radius, direction, out RaycastHit hitInfo, distance,
    layerMask)) {
      return new SurfaceCheckResult {
        isHit = true,
        hitInfo = hitInfo,
        point = hitInfo.point,
        normal = hitInfo.normal,
        distance = hitInfo.distance
      };
    } else {
      return new SurfaceCheckResult { isHit = false };// Or a default empty result
    }
  }
  // CheckGround function checks for ground using raycast and returns a bool value.
  // Parameters:
  // - raycastOrigin: The origin point of the raycast.
  // Returns:
  // - A boolean indicating whether the object is grounded.
}
}
