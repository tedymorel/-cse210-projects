// EXTRA CREDIT: NegativeGoal - recording this goal LOSES points (for bad habits).
// Helps users track habits they want to STOP doing, like eating junk food.
public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int pointPenalty)
        : base(name, description, pointPenalty)
    {
    }

    public override int RecordEvent()
    {
        return -GetPoints(); // Deducts points as a penalty
    }

    public override bool IsComplete() => false; // Negative goals are never "complete"

    public override string GetDisplayString()
    {
        return $"[✗] {GetName()} ({GetDescription()}) -- -{GetPoints()} pts per occurrence";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{GetName()},{GetDescription()},{GetPoints()}";
    }
}
