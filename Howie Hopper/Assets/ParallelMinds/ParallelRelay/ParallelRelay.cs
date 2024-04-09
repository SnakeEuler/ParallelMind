using System;
namespace ParallelRelay {
public class ParallelRelay: ParallelRelayBase<Action>, ILink {
  private ILink link = null;
  public new uint ListenerCount => base.ListenerCount;

  public ILink Plink {
    get {
      if (!hasLink) {
        link = new Link (this);
        hasLink = true;
      }
      return link;
    }
  }

  // Dispatch function that iterates through the listeners and invokes them,
  // then iterates through the nonceListeners and removes null elements
  public void Dispatch () {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      var listener = listeners[i - 1];// Store in a local variable
      if (listener != null) {
        listener ();
      } else {
        RemoveAt (i - 1);
      }
    }
    for (var i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (); }
    }
  }
}

public class ParallelRelay<T>: ParallelRelayBase<Action<T>>, ILink<T> {
  private ILink<T> link = null;

  public new uint ListenerCount => base.ListenerCount;

  public ILink<T> Plink {
    get {
      if (hasLink) return link;
      link = new Link<T> (this);
      hasLink = true;
      return link;
    }
  }

  // Dispatches the given parameter to all listeners, and removes any null listeners from the list.
  public void Dispatch (T t) {
    for (var i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (var i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t); }
    }
  }
}

public class ParallelRelay<T, U>: ParallelRelayBase<Action<T, U>>, ILink<T, U> {
  private ILink<T, U> link = null;

  public new uint ListenerCount => base.ListenerCount;

  public ILink<T, U> Plink {
    get {
      if (hasLink) return link;
      link = new Link<T, U> (this);
      hasLink = true;
      return link;
    }
  }

  // Dispatches the given values to all the listeners, removing any null listeners
  // and handling nonce listeners.
  public void Dispatch (T t, U u) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t, u);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (var i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t, u); }
    }
  }
}

public class ParallelRelay<T, U, V>: ParallelRelayBase<Action<T, U, V>>, ILink<T, U, V> {
  private ILink<T, U, V> link = null;

  public new uint ListenerCount => base.ListenerCount;

  public ILink<T, U, V> Plink {
    get {
      if (!hasLink) {
        link = new Link<T, U, V> (this);
        hasLink = true;
      }
      return link;
    }
  }

  public void Dispatch (T t, U u, V v) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t, u, v);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (uint i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t, u, v); }
    }
  }
}

public class ParallelRelay<T, U, V, W>: ParallelRelayBase<Action<T, U, V, W>>, ILink<T, U, V, W> {
  private ILink<T, U, V, W> link = null;

  public new uint ListenerCount => base.ListenerCount;

  public ILink<T, U, V, W> Plink {
    get {
      if (hasLink) return link;
      link = new Link<T, U, V, W> (this);
      hasLink = true;
      return link;
    }
  }

  public void Dispatch (T t, U u, V v, W w) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t, u, v, w);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (var i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      l?.Invoke (t, u, v, w);
    }
  }
}
}
