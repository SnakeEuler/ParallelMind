ParallelMinds State Machine

Bring Structure and Control to Your Unity Projects

Unleash the power of state machines to manage your game logic with clarity and ease. The ParallelMinds State Machine provides a robust foundation for organizing complex behaviors:

Key Features

    Hierarchical States: Break down complex behaviors into manageable sub-states, promoting code reuse and reducing redundancy.
    Event-Driven Transitions: Create smooth, reactive state changes based on player input, in-game events, or any triggers you define.
    Flexible Configuration: Design your states and transitions easily using the intuitive builder pattern.
    Local Time Tracking: Manage time-based logic within individual states.
    Robust Error Handling: Catch potential configuration errors during development for a smoother workflow.

Getting Started

    Attach the ParallelStateMachine: Add the component to a GameObject in your scene.

    Define States:  Use the StateBuilder to create and configure your states:
    C#

    var myStateMachine = GetComponent<ParallelStateMachine>(); 

    myStateMachine.AddState("IdleState")
        .OnEnter(() => Debug.Log("Entering Idle State"))
        .OnUpdate(() => Debug.Log("Updating Idle State"))
        .Build(); 

    Use code with caution.

Add Transitions:  Connect states using the AddTransition method:
C#

myStateMachine.AddState("RunState")
// ... setup ...
.AddTransition("RunKeyPressed", () => Input.GetKeyDown(KeyCode.LeftShift), "RunState")
.Build();

Use code with caution.

    Control State Flow:  Use fsm.ChangeState("stateName") to switch states and fsm.FireEvent("eventName") to trigger transitions.

Example: Upgradable Game Character
C#

// ... (Base movement states: Idle, Run, Jump)

// States for special arm attachments
myStateMachine.AddState("FireArmState")
.OnUpdate(() => { if (Input.GetButtonDown("Fire1")) FireProjectile(); })
.Build();

// ... (MagnetArmState configuration)

// Transitions based on equipment
myStateMachine.AddState("Idle")
.AddTransition("FireArmEquipped", () => hasFireArms, "FireArmState")
.AddTransition("MagnetArmEquipped", () => hasMagnetArms, "MagnetArmState")
.Build();

Use code with caution.

Beyond the Basics (In Development)

    Sub-States: Achieve even finer control with nested states.
    Co-routine Support: Enhance state logic with Unity's powerful Coroutines.
