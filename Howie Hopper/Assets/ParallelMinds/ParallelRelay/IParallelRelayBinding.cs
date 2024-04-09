namespace ParallelRelay {
public interface IParallelRelayBinding  {
  bool Enabled { get; }
  bool AllowDuplicates { get; set; }
  uint PersistentListenerCount { get; }
  bool Enable (bool enable);
}
}
