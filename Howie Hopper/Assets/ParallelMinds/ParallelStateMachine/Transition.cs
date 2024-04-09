using System;

namespace ParallelMinds {
public class Transition {
  public State SourceState { get; set; }
  public string EventName { get; set; }
  public string TargetStateName { get; set; }
  public Func<bool> Condition { get; set; }

  public Transition (State sourceState, string targetStateName, Func<bool> condition) {
    SourceState = sourceState;
    TargetStateName = targetStateName;
    Condition = condition;
  }
  public Transition (State sourceState, Func<bool> condition) {
    SourceState = sourceState;
    Condition = condition;
  }
}
}
