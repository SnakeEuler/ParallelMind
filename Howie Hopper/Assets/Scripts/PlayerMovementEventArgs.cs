using System;
using UnityEngine;
namespace ParallelMinds {
public class PlayerMovementEventArgs: EventArgs {
  public Vector3 velocity;
  public Vector3 position;

  // Constructor for creating a new PlayerMovementEventArgs object with the given velocity and position.
  public PlayerMovementEventArgs (Vector3 velocity, Vector3 position) {
    this.velocity = velocity;
    this.position = position;
  }
}
}
