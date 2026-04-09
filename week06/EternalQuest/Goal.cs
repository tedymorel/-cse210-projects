// Base class for all goal types in the Eternal Quest program.
// Uses abstraction to define shared behavior and encapsulate shared data.
public abstract class Goal
{
    // Shared attributes for every goal
    private string _name;
    private string _description;
    private int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    // Accessors
    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetPoints() => _points;

    // Polymorphic methods - each subclass must implement these
    public abstract int RecordEvent();          // returns points earned (may be negative)
    public abstract bool IsComplete();
    public abstract string GetDisplayString();  // shown in the goal list
    public abstract string GetStringRepresentation(); // for file serialization

    // Helper for setting name/desc/points from a loaded string
    protected void SetName(string name) => _name = name;
    protected void SetDescription(string description) => _description = description;
    protected void SetPoints(int points) => _points = points;
}
