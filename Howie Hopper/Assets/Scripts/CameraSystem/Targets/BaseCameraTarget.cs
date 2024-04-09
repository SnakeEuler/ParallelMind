using UnityEngine;
namespace ParallelMinds {
public abstract class BaseCameraTarget: MonoBehaviour, ICameraTarget {
  public abstract Vector3 GetTargetPosition ();
  //The influence that a target has on the camera
  public abstract void SetTargetInfluence ();

  public virtual Vector3 Postion {
    get {
      var position = transform.position;
      return new Vector3 (position.x, position.y, position.z);
    }
    set { transform.position = new Vector3 (value.x, value.y, value.z); }
  }
}
}
