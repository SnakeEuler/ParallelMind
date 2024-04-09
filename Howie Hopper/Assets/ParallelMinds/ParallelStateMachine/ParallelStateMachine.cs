using System;
using System.Collections.Generic;
using UnityEngine;

namespace ParallelMinds {
public class ParallelStateMachine: MonoBehaviour {
  public State currentState;
  public Dictionary<StateName, State> states = new Dictionary<StateName, State> ();

  public void Update () { currentState?.UpdateState (); }

  public void FixedUpdate () { currentState?.FixedUpdateState (); }

  // Adds a new state to the state machine and returns a builder for configuring it
  public StateBuilder AddState (StateName stateName) {
    if (states.ContainsKey (stateName)) {
      Debug.LogError ("State with name '" + stateName + "' already exists in the state machine.");
      return null;
    }
    var newState = new State (stateName, this);
    states[stateName] = newState;
    return new StateBuilder (newState, this);
  }
  public void AddState (StateName stateName, State state) {
    if (states.ContainsKey (stateName)) {
      Debug.LogError ("State with name '" + stateName + "' already exists in the state machine.");
      return;
    }
    states[stateName] = state;
  }
  // Changes the current state of the state machine
  public void ChangeState (StateName stateName) {
    if (states.TryGetValue (stateName, out var newState)) {
      currentState?.ExitState ();
      currentState = newState;
      currentState.EnterState ();
    } else {
      Debug.LogError ($"State '{stateName}' not found.");
    }
  }
  public void ChangeState (State state) {
    if (states.TryGetValue (state.stateName, out var newState)) {
      currentState?.ExitState ();
      currentState = newState;
      currentState.EnterState ();
    } else {
      Debug.LogError ($"State '{state.stateName}' not found.");
    }
  }
  // Triggers an event within the current state
  public void FireEvent (string eventName) { currentState?.HandleEvent (); }

  // Returns the current active state
  public State GetCurrentState () { return currentState; }
}
}
