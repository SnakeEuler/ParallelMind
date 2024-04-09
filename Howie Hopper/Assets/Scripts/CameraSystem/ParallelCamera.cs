using System.Collections.Generic;
using UnityEngine;
namespace ParallelMinds {
public class ParallelCamera: MonoBehaviour {
  //this camera
  private Camera parallelCamera;

  private List<ICameraTarget> targets = new List<ICameraTarget> ();
  private List<ICameraBehavior> behaviors = new List<ICameraBehavior> ();

  public void AddBehavior (ICameraBehavior behavior) { }
  public void RemoveBehavior (ICameraBehavior behavior) { }
  public void AddTarget (ICameraTarget target) { }
  public void RemoveTarget (ICameraTarget target) { }

  //A primary function of the Parallel Camera is to  track the action within a scene and focus on
  //it within a frame, aka the Frame of Action.
  public Rect frameOfAction = new Rect (5f, 5f, 10f, 10f);
  //A default padding area at the screen boundaries
  public float framePadding = 10f;
  //The area inside the frameOfAction that acts as a neutral zone. Actions or player movement
  //within this frame do not cause he camera to move allowing for a stable, consistent viewing of
  //the game world
  public Rect neutralFrame = new Rect (0f, 0f, 5f, 5f);
  //The calculated area of difference that remains inside the frameOfAction, but outside
  //the neutralFrame is active and the player's actions and movement within the space
  //force camera movement

  public Camera ActiveCamera { get { return parallelCamera; } }

  public Vector2 CameraPosition {
    get { return new Vector2 (transform.position.x, transform.position.y); }
  }

  public bool IsTargetInFrameOfAction (Vector3 targetPosition) {
    Vector3 screenPoint = ActiveCamera.WorldToScreenPoint (targetPosition);

    return screenPoint.x > frameOfAction.xMin && screenPoint.x < frameOfAction.xMax
        && screenPoint.y > frameOfAction.yMin && screenPoint.y < frameOfAction.yMax;
  }
  public bool IsTargetInNeutralFrame (Vector3 targetPosition) {
    Vector3 screenPoint = ActiveCamera.WorldToScreenPoint (targetPosition);

    return screenPoint.x > neutralFrame.xMin && screenPoint.x < neutralFrame.xMax
        && screenPoint.y > neutralFrame.yMin && screenPoint.y < neutralFrame.yMax;
  }
  public Vector3 CalculateFramingOffset (Vector3 targetPosition) {
    Vector3 screenPoint = ActiveCamera.WorldToScreenPoint (targetPosition);
    Vector3 offset = Vector3.zero;

    // Example: If target is too far left
    if (screenPoint.x < frameOfAction.xMin) {
      offset.x = frameOfAction.xMin - screenPoint.x;
    }

    // Similar logic for other directions (right, top, bottom)

    // Convert back to world space offset
    return ActiveCamera.ScreenToWorldPoint (offset) - targetPosition;
  }
}
}
