using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ParallelMinds {
[RequireComponent (typeof (Camera))]
public class CameraManager: MonoBehaviour {
  [SerializeField]
  private Camera mainCamera;
  [SerializeField]
  private List<ICameraBehavior> activeBehaviors;
  [SerializeField]
  private List<ICameraTarget> activeTargets;

  public void AddBehavior (ICameraBehavior behavior) { activeBehaviors.Add (behavior); }

  public void RemoveBehavior (ICameraBehavior behavior) { activeBehaviors.Remove (behavior); }

  public void AddTarget (ICameraTarget target) { activeTargets.Add (target); }

  public void RemoveTarget (ICameraTarget target) { activeTargets.Remove (target); }

  private void Update () {
    Vector3[] targetPositions = activeTargets.Select (t => t.GetTargetPosition ()).ToArray ();
    activeBehaviors.Sort ((a, b) => a.Priority.CompareTo (b.Priority));
    Vector3 finalPosition = CalculateBlendedPosition (targetPositions, activeBehaviors);
    mainCamera.transform.position = finalPosition;

    ParallelCamera framingCamera = GetComponent<ParallelCamera>(); // Assuming it's on the same object
    if (framingCamera != null)
    {
      // Adjust target positions using framingCamera.IsTargetInFrameOfAction(), etc.
      for (int i = 0; i < targetPositions.Length; i++)
      {
        if (!framingCamera.IsTargetInNeutralFrame(targetPositions[i]))
        {
          // Calculate an offset
          Vector3 offset = framingCamera.CalculateFramingOffset(targetPositions[i]);
          targetPositions[i] += offset;
        }
      }
    }
  }
  private Vector3 CalculateBlendedPosition (Vector3[] targets, List<ICameraBehavior> behaviors) {
    if (targets.Length == 0) return Vector3.zero;

    Vector3 weightedSum = Vector3.zero;
    float totalWeight = 0;

    for (int i = 0;i < targets.Length;i ++) {
      weightedSum += targets[i] * behaviors[i].Weight;
      totalWeight += behaviors[i].Weight;
    }

    return weightedSum / totalWeight;
  }
}
}
