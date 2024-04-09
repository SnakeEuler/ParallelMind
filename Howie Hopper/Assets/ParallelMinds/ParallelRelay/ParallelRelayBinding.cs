using System;
using UnityEngine;
namespace ParallelRelay {
public class ParallelRelayBinding<TDelegate>: IParallelRelayBinding where TDelegate : class {
  [SerializeField]
  private bool IsEnabled { get; set; }
  protected ILinkBase<TDelegate> ParallelRelay { get; private set; }
  protected TDelegate Listener { get; private set; }

  //Constructors
  private ParallelRelayBinding () { }
  public ParallelRelayBinding (
  ILinkBase<TDelegate> parallelRelay, TDelegate listener, bool allowDuplicates,
  bool isListening): this () {
    ParallelRelay = parallelRelay;
    Listener = listener;
    this.AllowDuplicates = allowDuplicates;
    Enabled = isListening;
  }

  public bool Enabled { get; set; }
  public bool AllowDuplicates { get; set; }

  public uint PersistentListenerCount => ParallelRelay.PersistentListenerCount;



  public bool Enable (bool enable) {
    if (enable) {
      if (!Enabled) {
        if (ParallelRelay.AddListener (Listener, AllowDuplicates)) {
          Enabled = true;
          return true;
        }
      }
    } else {
      if (Enabled) {
        ParallelRelay.RemoveListener (Listener);
        Enabled = false;
        return true;
      }
    }
    return false;
  }

  // Toggles the state of the function. No parameters or return type.
  public void Toggle () { Enable (!Enabled); }
}
}
