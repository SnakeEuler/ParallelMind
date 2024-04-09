using System;
namespace ParallelMinds.ParallelRelay {
public class ParallelRelayEventArgs: EventArgs {
  public object[] Arguments { get; private set; }

  public ParallelRelayEventArgs (params object[] args) { Arguments = args; }
}
}
