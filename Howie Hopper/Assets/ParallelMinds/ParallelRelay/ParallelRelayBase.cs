using System;
namespace ParallelRelay {
// ParallelRelayBase: Abstract base class for ParallelRelay event system.
public abstract class ParallelRelayBase<TDelegate>: ILinkBase<TDelegate> where TDelegate : class {
  public uint PersistentListenerCount => pCount;
  public uint NonceListenerCount => nCount;

  public uint ListenerCount => pCount + nCount;

  protected bool hasLink = false;
  protected TDelegate[] listeners = new TDelegate[1];
  protected uint pCount;
  private uint pCap;

  protected TDelegate[] nonceListeners;
  protected uint nCount;
  private uint nCap;

  protected static readonly IndexOutOfRangeException eIOOR = new IndexOutOfRangeException (
  "Fewer listeners than expected. See guidelines on using RemoveListener and RemoveAll within ParallelRelay listeners.");

  // Check if a listener is already added.
  public bool Contains (TDelegate listener) => Contains (listeners, pCount, listener);

  // Add a new listener to the event system.
  public bool AddListener (TDelegate listener, bool allowDuplicates = false) {
    if (!allowDuplicates && Contains (listener)) return false;
    if (pCount >= pCap) {
      pCap = pCap == 0? 1 : pCap * 2;
      listeners = Expand (listeners, pCap, pCount);
    }
    listeners[pCount ++] = listener;
    return true;
  }

  // Bind a listener with the ParallelRelay system.
  public IParallelRelayBinding BindListener (TDelegate listener, bool allowDuplicates = false) {
    return AddListener (listener, allowDuplicates)
        ? new ParallelRelayBinding<TDelegate> (this, listener, allowDuplicates, true) : null;
  }

  // Add a nonce (single-use) listener.
  public bool AddNonce (TDelegate listener, bool allowDuplicates = false) {
    if (!allowDuplicates && Contains (nonceListeners, nCount, listener)) return false;
    if (nCount >= nCap) {
      nCap = nCap == 0? 1 : nCap * 2;
      nonceListeners = Expand (nonceListeners, nCap, nCount);
    }
    nonceListeners[nCount ++] = listener;
    return true;
  }

  // Remove a specific listener.
  public bool RemoveListener (TDelegate listener) {
    for (uint i = 0;i < pCount;++ i) {
      if (listeners[i].Equals (listener)) {
        RemoveAt (i);
        return true;
      }
    }
    return false;
  }

  // Remove a specific nonce listener.
  public bool RemoveNonceListener (TDelegate listener) {
    for (uint i = 0;i < nCount;++ i) {
      if (nonceListeners[i].Equals (listener)) {
        RemoveOnceAt (i);
        return true;
      }
    }
    return false;
  }

  // Remove all listeners.
  public void RemoveAllListeners (bool removePersistent = true, bool removeNonce = true) {
    if (removePersistent) {
      Array.Clear (listeners, 0, (int)pCap);
      pCount = 0;
    }
    if (removeNonce && nCount > 0) {
      Array.Clear (nonceListeners, 0, (int)nCap);
      nCount = 0;
    }
  }

  // Utility methods for internal array management.
  protected void RemoveAt (uint i) => pCount = RemoveAt (listeners, pCount, i);
  protected void RemoveOnceAt (uint i) => nCount = RemoveAt (nonceListeners, nCount, i);

  protected static uint RemoveAt (TDelegate[] arr, uint count, uint index) {
    for (uint i = index;i < count - 1;++ i) {
      arr[i] = arr[i + 1];
    }
    arr[-- count] = null;
    return count;
  }

  protected static bool Contains (TDelegate[] arr, uint count, TDelegate item) {
    for (uint i = 0;i < count;++ i) {
      if (arr[i].Equals (item)) return true;
    }
    return false;
  }

  protected static TDelegate[] Expand (TDelegate[] arr, uint newCapacity, uint count) {
    var newArr = new TDelegate[newCapacity];
    Array.Copy (arr, newArr, count);
    return newArr;
  }
}
}
