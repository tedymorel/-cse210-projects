// A ChecklistGoal must be completed a set number of times.
// Each completion earns points; the final completion earns a bonus.
public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _requiredTimes;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int requiredTimes, int bonusPoints)
        : base(name, description, points)
    {
        _timesCompleted = 0;
        _requiredTimes = requiredTimes;
        _bonusPoints = bonusPoints;
    }

    // Constructor used when loading from file
    public ChecklistGoal(string name, string description, int points, int requiredTimes, int bonusPoints, int timesCompleted)
        : base(name, description, points)
    {
        _timesCompleted = timesCompleted;
        _requiredTimes = requiredTimes;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (IsComplete()) return 0;

        _timesCompleted++;
        int earned = GetPoints();

        if (_timesCompleted >= _requiredTimes)
        {
            earned += _bonusPoints; // Bonus on final completion!
        }
        return earned;
    }

    public override bool IsComplete() => _timesCompleted >= _requiredTimes;

    public override string GetDisplayString()
    {
        string status = IsComplete() ? "[X]" : "[ ]";
        return $"{status} {GetName()} ({GetDescription()}) -- Completed {_timesCompleted}/{_requiredTimes} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{GetName()},{GetDescription()},{GetPoints()},{_requiredTimes},{_bonusPoints},{_timesCompleted}";
    }
}
