using System;
using UnityEngine;
namespace ParallelMinds {
public class StateBuilder {
  private readonly State state;
  private readonly ParallelStateMachine fsm;

  // Initializes a StateBuilder with the given stateName and fsm.
  public StateBuilder (StateName stateName, ParallelStateMachine fsm) {
    this.state = new State (stateName, fsm);
    this.fsm = fsm;
  }
  // Constructor for StateBuilder class, initializes state and fsm parameters
  public StateBuilder (State state, ParallelStateMachine fsm) {
    this.state = state;
    this.fsm = fsm;
  }
  // Sets the onEnter action for the state and returns the StateBuilder instance.
  public StateBuilder OnEnter (Action onEnter) {
    state.OnEnter = onEnter;
    return this;
  }
  // Set the callback for when the state is updated and return the current StateBuilder instance.
  public StateBuilder OnUpdate (Action onUpdate) {
    state.OnUpdate = onUpdate;
    return this;
  }
  // Sets the action to be called on FixedUpdate and returns the StateBuilder for method chaining.
  public StateBuilder OnFixedUpdate (Action onFixedUpdate) {
    state.OnFixedUpdate = onFixedUpdate;
    return this;
  }
  // Adds a transition to the state with the specified target state name and condition,
  // and returns the modified StateBuilder.
  public StateBuilder AddTransition (StateName targetStateName, Func<bool> condition) {
    if (state.transitions.ContainsKey (targetStateName)) {
      Debug.LogError (
      $"Transition to state '{targetStateName}' already exists in the state '{state.stateName}'.");
      return this;
    }
    state.transitions[targetStateName] = condition;
    return this;
  }
  // Sets the action to be executed upon exiting the state and returns the StateBuilder.
  public StateBuilder OnExit (Action onExit) {
    state.OnExit = onExit;
    return this;
  }
  // Build function that checks for duplication before adding the state to the FSM
  public State Build () {
    // Check if the state is already added to avoid duplication
    if (!fsm.states.ContainsKey (state.stateName)) {
      fsm.states.Add (state.stateName, state);
    }
    return state;
  }
}
}
