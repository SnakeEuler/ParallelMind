using System;
using UnityEngine;
namespace ParallelRelay {
[CreateAssetMenu (fileName = "New Event", menuName = "Events/ParallelRelay")]
public class ScriptableEvent<T>: ScriptableObject where T : new () {
  private ParallelRelay<T> parallelRelay = new ParallelRelay<T> ();

  public void Invoke (T value) { parallelRelay.Dispatch (value); }

  public void Register (Action<T> eventListener) { parallelRelay.AddListener (eventListener); }

  public void Unregister (Action<T> eventListener) { parallelRelay.RemoveListener (eventListener); }
}
}
