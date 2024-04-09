using System;
using UnityEngine;
namespace ParallelRelay {
public class ParallelRelayExceptions: MonoBehaviour {
  public class DuplicateListenerException: Exception {
    public DuplicateListenerException (): base (
    "Attempted to add a duplicate listener to a ParallelRelay event.") {
    }
  }

  public class ListenerNotFoundException: Exception {
    public ListenerNotFoundException (): base (
    "The specified listener was not found in the ParallelRelay.") {
    }
  }

  public class InvalidRelayOperationException: Exception {

    public InvalidRelayOperationException (string message): base (message) { }
  }
}
}
