using System;
using UnityEngine;
namespace ParallelMinds {
[AddComponentMenu ("ParallelCamera/Behaviours/Smooth Follow")]
[RequireComponent (typeof (Camera))]
public class SmoothFollow: BaseCameraBehavior {
  public override void OnDeactivate () { throw new System.NotImplementedException (); }

  //the principal camera
  private Camera followCamera;
  [SerializeField]
  private TransformTarget target;

  [SerializeField]
  private Vector3 lerpSpeed = new Vector3 (0.5f, 0.5f, 0.5f);
  [SerializeField]
  private Vector3 cameraChangeThreshold = new Vector3 (0.1f, 0.1f, 0.1f);

  private Vector3 lastPoistion;
  private Vector3 lastPositionAbs;

  public Camera FollowCamera {
    get {
      if (followCamera == null) {
        followCamera = GetComponent<Camera> ();
      }
      return followCamera;
    }
  }

  public Vector3 LerpSpeed { get { return lerpSpeed; } set { lerpSpeed = value; } }

  public override void OnActivate () { }


  private void Start () { followCamera = GetComponent<Camera> (); }


  private void LateUpdate () {
    Vector3 targetPosition = target.GetTargetPosition ();
    Vector3 desiredPosition = targetPosition;// You might offset this based on framing

    // Check if change is significant enough (using thresholds)
    if (Vector3.Distance (followCamera.transform.position, desiredPosition)
      > cameraChangeThreshold.magnitude) {
      followCamera.transform.position = Vector3.Lerp (followCamera.transform.position,
      desiredPosition, lerpSpeed.x * Time.deltaTime);
    }
  }
}
}
