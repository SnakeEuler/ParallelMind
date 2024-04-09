namespace ParallelRelay {
public class EventContext {
  private readonly object data;
  // Initializes a new instance of the EventContext class with the specified data.
  public EventContext (object data) { this.data = data; }

  // Returns the generic type T after casting the data to type T.
  // The type T must have a parameterless constructor.
  public T As<T> () where T : new () { return (T)data; }
}
}
