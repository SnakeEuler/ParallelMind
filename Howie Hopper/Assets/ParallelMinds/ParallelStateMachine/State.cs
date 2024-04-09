using System;
using System.Collections.Generic;
using UnityEngine;

namespace ParallelMinds {
public class State {
  public string Name { get; }
  public StateName stateName { get; }
  public ParallelStateMachine fsm { get; }
  public State ParentState { get; set; }// For hierarchical states

  // Stores transitions associated with this state
  public Dictionary<StateName, Func<bool>> transitions = new Dictionary<StateName, Func<bool>> ();

  public float localTime;// Time tracking within a state

  public Action OnEnter { get; set; }
  public Action OnUpdate { get; set; }
  public Action OnFixedUpdate { get; set; }
  public Action OnExit { get; set; }

  public State (StateName stateName, ParallelStateMachine fsm) {
    this.stateName = stateName;

    this.fsm = fsm;
  }
  public State AddTransition (StateName targetStateName, Func<bool> condition) {
    if (transitions.ContainsKey (targetStateName)) {
      Debug.LogError (
      $"Transition to state '{targetStateName}' already exists in the state '{stateName}'.");
      return this;
    }
    transitions[targetStateName] = condition;
    return this;
  }

  // Methods for handling state changes
  public virtual void EnterState () { OnEnter?.Invoke (); }
  public virtual void UpdateState () { OnUpdate?.Invoke (); }
  public virtual void FixedUpdateState () { OnFixedUpdate?.Invoke (); }
  public virtual void ExitState () { OnExit?.Invoke (); }

  // Handles events, triggering state changes if any corresponding transitions exist
  public void HandleEvent () {
    foreach (var transition in transitions) {
      if (transition.Value ()) {
        fsm.ChangeState (transition.Key);
        break;
      }
    }
  }
}
}
