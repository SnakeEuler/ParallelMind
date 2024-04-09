using UnityEngine;

public class SimpleFollowCamera: MonoBehaviour {
  public Transform target;      // Assign the target GameObject in the Inspector
  public float followSpeed = 2f;// Adjust this value to control how fast the camera follows

  void LateUpdate () {
    if (target != null) {
      // Smoothly follow the target's position with an offset
      Vector3 desiredPosition
          = target.position + new Vector3 (0, 0, -10f);// Adjust offset as needed
      transform.position
          = Vector3.Lerp (transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
  }
}
