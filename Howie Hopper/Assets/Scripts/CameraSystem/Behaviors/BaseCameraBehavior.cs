using UnityEngine;
namespace ParallelMinds {
public abstract class BaseCameraBehavior: MonoBehaviour, ICameraBehavior {
  public ICameraTarget target;

  public void UpdateBehavior (float deltaTime) { }
  public abstract void OnActivate ();
  public abstract void OnDeactivate ();
  public CameraBehaviorPriority Priority { get; set; }
  public float Weight { get; set; }
}
}
