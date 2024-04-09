using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ParallelRelay {
public class EventListener<T>: MonoBehaviour where T : new () {
  [SerializeField]
  private List<ScriptableEvent<T>> scriptableEvents;// List of ScriptableEvents
  [SerializeField]
  protected UnityEvent<T> callbacks;

  protected virtual void OnEnable () {
    foreach (var scriptableEvent in scriptableEvents) {
      // ReSharper disable once Unity.NoNullPropagation
      scriptableEvent?.Register (OnSignalReceived);
    }
  }

  protected virtual void OnDisable () {
    foreach (var scriptableEvent in scriptableEvents) {
      // ReSharper disable once Unity.NoNullPropagation
      scriptableEvent?.Unregister (OnSignalReceived);
    }
  }

  public virtual void OnSignalReceived (T signal) { callbacks.Invoke (signal); }
}
}
