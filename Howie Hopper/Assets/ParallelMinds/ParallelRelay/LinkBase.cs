using System;
namespace ParallelRelay {
public abstract class LinkBase<TDelegate>: ILinkBase<TDelegate> where TDelegate : class {
  protected ParallelRelayBase<TDelegate> parallelRelay;

  //Constructors
  private LinkBase () { }// To force use of parameters
  public LinkBase (ParallelRelayBase<TDelegate> parallelRelay) { this.parallelRelay = parallelRelay; }


  //Implementation
  public uint PersistentListenerCount { get { return parallelRelay.PersistentListenerCount; } }
  public uint NonceListenerCount { get { return parallelRelay.NonceListenerCount; } }

  public bool Contains (TDelegate listener) { return parallelRelay.Contains (listener); }

  public bool AddListener (TDelegate listener, bool allowDuplicates = false) {
    return parallelRelay.AddListener (listener, allowDuplicates);
  }

  public IParallelRelayBinding BindListener (TDelegate listener, bool allowDuplicates = false) {
    return parallelRelay.BindListener (listener, allowDuplicates);
  }

  public bool AddNonce (TDelegate listener, bool allowDuplicates = false) {
    return parallelRelay.AddNonce (listener, allowDuplicates);
  }

  public bool RemoveListener (TDelegate listener) { return parallelRelay.RemoveListener (listener); }

  public bool RemoveNonceListener (TDelegate listener) {
    return parallelRelay.RemoveNonceListener (listener);
  }

  public void RemoveAllListeners (bool removePersistant = true, bool removeNonce = true) {
    parallelRelay.RemoveAllListeners (removePersistant, removeNonce);
  }
}

public class Link: LinkBase<Action>, ILink {
  public Link (ParallelRelayBase<Action> parallelRelay): base (parallelRelay) { }
}
public class Link<T>: LinkBase<Action<T>>, ILink<T> {
  public Link (ParallelRelayBase<Action<T>> parallelRelay): base (parallelRelay) { }
}
public class Link<T, U>: LinkBase<Action<T, U>>, ILink<T, U> {
  public Link (ParallelRelayBase<Action<T, U>> parallelRelay): base (parallelRelay) { }
}
public class Link<T, U, V>: LinkBase<Action<T, U, V>>, ILink<T, U, V> {
  public Link (ParallelRelayBase<Action<T, U, V>> parallelRelay): base (parallelRelay) { }
}
public class Link<T, U, V, W>: LinkBase<Action<T, U, V, W>>, ILink<T, U, V, W> {
  public Link (ParallelRelayBase<Action<T, U, V, W>> parallelRelay): base (parallelRelay) { }
}
}
