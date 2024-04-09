using UnityEngine;

namespace ParallelMinds {
public class IdleState: State {
  private PlayerMovementController pmc;
  private PlayerData playerData;
  private Rigidbody rb;

  public IdleState (PlayerMovementController controller): base (StateName.Idle, controller.fsm) {
    pmc = controller;
    playerData = controller.playerData;
    rb = controller.rb;

    // Add transition to WalkState
    transitions[StateName.Walk] = () => pmc.playerInputHandler.MoveDirection != Vector2.zero;
    transitions[StateName.Jump] = () => pmc.playerInputHandler.jumpInputPressed;
  }

  public override void UpdateState () {
    base.UpdateState ();
    HandleStateChange ();
  }

  public void HandleStateChange () {
    // Handle transitions
    foreach (var transition in transitions) {
      if (transition.Value ()) {
        fsm.ChangeState (transition.Key);
        break;
      }
    }
  }
}
}
