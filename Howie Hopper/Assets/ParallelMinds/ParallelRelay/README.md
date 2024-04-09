README

ParallelRelay Event System

This project provides a versatile event system for your Unity games, empowering you to send and receive messages for flexible code communication.  Choose between two approaches to best suit your project's needs.

Why Event Systems?

Event systems help you decouple different parts of your game logic. Instead of objects directly calling methods on each other, they can send events. Other objects "listen" for these events and react accordingly. This leads to:

    Easier to manage dependencies: Changes in one area have less chance of causing issues in others.
    Improved code organization: Your game logic can be divided into more manageable pieces.
    Flexibility: Add or modify what happens in response to events without altering the code that triggers them.

1. Core ParallelRelay System

   Purpose: Offers maximum performance and code-driven control over event management. Ideal for when you want a lightweight solution and granular customization.

   Key Features:
   Persistent & Nonce Listeners: Tailor listener behavior – persistent listeners stay subscribed, nonce listeners automatically remove themselves after being triggered once.
   Listener Binding: Enable and disable listeners as needed for dynamic control.
   Lazy Linking: Ensures efficiency by minimizing overhead when events are not actively in use.

   How to Use:
   Create an Event: Choose the appropriate ParallelRelay class based on the parameters your event needs (up to 4 supported).
   Register Listeners: Use AddListener for persistent listeners and AddNonce for single-use listeners.
   Dispatch (Send) the Event: Call Dispatch on your ParallelRelay, providing any expected parameters.

2. ScriptableEvent System

   Purpose:  Adds Unity Editor integration on top of ParallelRelay. Events become ScriptableObject assets for easy visualization and workflow integration.

   Key Features:
   Editor-Friendly Events: Create and modify events within the Project View. Drag and drop them to visually connect components!
   Visual Event Wiring: Use UnityEvents to link components to your events without writing extensive listener code.

   How to Use:
   Create a ScriptableEvent: Right-click in the Project View, go to "Create -> Events -> ParallelRelay" and choose the matching data type.
   Attach Listeners: Add an EventListener<T> component (T matches your event's type) to a GameObject. Drag your ScriptableEvent into the designated field.
   Add Actions: Use UnityEvents in the Inspector to define what happens when the event is triggered.

Choosing Your System

    Core ParallelRelay: Prioritize this for the most lightweight system and full code-based control.
    ScriptableEvent: Opt for this when you want the benefits of the editor: easy visual wiring of events, and managing them like other Unity Assets.

Example (Core ParallelRelay)
Code snippet

// Event for when a player's score changes
ParallelRelay<int> scoreUpdated = new ParallelRelay<int>();

// Listener to update a scoreboard
void UpdateScoreboard(int newScore) {
scoreDisplayText.text = "Score: " + newScore;
}

// Register the listener
scoreUpdated.AddListener(UpdateScoreboard);

// ... Later in your game logic
scoreUpdated.Dispatch(1000); // Dispatch with the new score
