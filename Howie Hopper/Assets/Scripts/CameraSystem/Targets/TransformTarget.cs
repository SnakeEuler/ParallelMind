using UnityEngine;
namespace ParallelMinds {
public class TransformTarget: BaseCameraTarget {
  //A optional offset that can be added to the target transform
  [SerializeField]
  private Vector3 offset;
  public Vector3 Offset { get { return offset; } set { offset = value; } }

  public override Vector3 GetTargetPosition () { return Postion + offset; }
  public override void SetTargetInfluence () { return; }
}
}
