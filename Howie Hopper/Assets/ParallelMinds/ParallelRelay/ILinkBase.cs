using System;
namespace ParallelRelay {
public interface ILinkBase<TDelegate> where TDelegate : class {
  uint PersistentListenerCount { get; }

  uint NonceListenerCount { get; }

  bool Contains (TDelegate listener);

  bool AddListener (TDelegate listener, bool allowDuplicates = false);

  IParallelRelayBinding BindListener (TDelegate listener, bool allowDuplicates = false);

  bool AddNonce (TDelegate listener, bool allowDuplicates = false);

  bool RemoveListener (TDelegate listener);

  bool RemoveNonceListener (TDelegate listener);

  void RemoveAllListeners (bool removePersistant = true, bool removeNonce = true);
}
public interface ILink : ILinkBase<Action> {}
public interface ILink<T> : ILinkBase<Action<T>> {}
public interface ILink<T, U> : ILinkBase<Action<T, U>> {}
public interface ILink<T, U, V> : ILinkBase<Action<T, U, V>> {}
public interface ILink<T, U, V, W> : ILinkBase<Action<T, U, V, W>> {}	
}
