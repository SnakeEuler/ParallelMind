using UnityEngine;

namespace ParallelMinds {
public interface ICameraBehavior {
  public void UpdateBehavior (float deltaTime);
  public void OnActivate ();
  public void OnDeactivate ();
  public CameraBehaviorPriority Priority { get; set; }
  float Weight { get; set; }
}
}
