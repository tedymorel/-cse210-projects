using System;
using System.Collections.Generic;
using System.IO;

// GoalManager is responsible for managing the list of goals,
// the user's score, leveling system, and file persistence.
public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private string _playerName;

    // EXTRA CREDIT: Level titles based on score thresholds
    private static readonly (int threshold, string title)[] _levels = new[]
    {
        (0,     "Earth Wanderer"),
        (500,   "Novice Seeker"),
        (1000,  "Bronze Pilgrim"),
        (2000,  "Silver Pilgrim"),
        (4000,  "Golden Pilgrim"),
        (7000,  "Emerald Sage"),
        (12000, "Diamond Knight"),
        (20000, "Eternal Guardian"),
        (35000, "Celestial Champion"),
        (50000, "Infinity Legend"),
    };

    public GoalManager(string playerName)
    {
        _playerName = playerName;
        _goals = new List<Goal>();
        _score = 0;
    }

    // ── Display ────────────────────────────────────────────────────────────────

    public void DisplayPlayerInfo()
    {
        string level = GetLevelTitle();
        Console.WriteLine($"\n  Player : {_playerName}");
        Console.WriteLine($"  Level  : {level}");
        Console.WriteLine($"  Score  : {_score} pts");
        Console.WriteLine($"  Goals  : {_goals.Count}");
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("  (No goals yet. Create one!)");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDisplayString()}");
        }
    }

    // ── Create goals ──────────────────────────────────────────────────────────

    public void CreateGoal()
    {
        Console.WriteLine("\n  What type of goal?");
        Console.WriteLine("  1. Simple Goal   (completed once)");
        Console.WriteLine("  2. Eternal Goal  (never-ending)");
        Console.WriteLine("  3. Checklist Goal (must do N times)");
        Console.WriteLine("  4. Negative Goal  (lose points for bad habits) ⭐");
        Console.Write("  Choice: ");
        string choice = Console.ReadLine();

        Console.Write("  Name: ");
        string name = Console.ReadLine();
        Console.Write("  Description: ");
        string description = Console.ReadLine();
        Console.Write("  Points per event: ");
        int points = int.Parse(Console.ReadLine());

        Goal goal = null;

        switch (choice)
        {
            case "1":
                goal = new SimpleGoal(name, description, points);
                break;
            case "2":
                goal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("  How many times must it be completed? ");
                int required = int.Parse(Console.ReadLine());
                Console.Write("  Bonus points for completing it? ");
                int bonus = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, description, points, required, bonus);
                break;
            case "4":
                goal = new NegativeGoal(name, description, points);
                break;
            default:
                Console.WriteLine("  Invalid choice. Goal not created.");
                return;
        }

        _goals.Add(goal);
        Console.WriteLine($"\n  ✅ Goal \"{name}\" created!");
    }

    // ── Record an event ───────────────────────────────────────────────────────

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("  No goals to record. Create one first!");
            return;
        }

        Console.WriteLine("\n  Which goal did you accomplish?");
        DisplayGoals();
        Console.Write("  Choice: ");

        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > _goals.Count)
        {
            Console.WriteLine("  Invalid selection.");
            return;
        }

        Goal goal = _goals[index - 1];
        int previousScore = _score;
        int earned = goal.RecordEvent();
        _score += earned;
        if (_score < 0) _score = 0; // Floor at 0 so score never goes negative

        if (earned > 0)
        {
            Console.WriteLine($"\n  🎉 You earned {earned} points!");
        }
        else if (earned < 0)
        {
            Console.WriteLine($"\n  💔 You lost {Math.Abs(earned)} points. Stay strong!");
        }
        else
        {
            Console.WriteLine("  This goal is already complete.");
        }

        // EXTRA CREDIT: Level-up celebration
        string oldLevel = GetLevelTitle(previousScore);
        string newLevel = GetLevelTitle(_score);
        if (oldLevel != newLevel)
        {
            Console.WriteLine($"\n  ⭐ LEVEL UP! You are now a \"{newLevel}\"! ⭐");
        }

        if (goal.IsComplete())
        {
            Console.WriteLine($"  🏆 Goal \"{goal.GetName()}\" is now COMPLETE! Congratulations!");
        }

        Console.WriteLine($"  Total Score: {_score} pts");
    }

    // ── Save / Load ───────────────────────────────────────────────────────────

    public void SaveGoals()
    {
        Console.Write("  Filename to save to (e.g. goals.txt): ");
        string filename = Console.ReadLine();

        using (StreamWriter file = new StreamWriter(filename))
        {
            file.WriteLine(_playerName);
            file.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                file.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"  ✅ Goals saved to \"{filename}\".");
    }

    public void LoadGoals()
    {
        Console.Write("  Filename to load from: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("  File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _playerName = lines[0];
        _score = int.Parse(lines[1]);
        _goals.Clear();

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];
            // Split on the first colon to get type vs. data
            int colonIndex = line.IndexOf(':');
            string type = line.Substring(0, colonIndex);
            string data = line.Substring(colonIndex + 1);
            string[] parts = data.Split(',');

            Goal goal = null;

            switch (type)
            {
                case "SimpleGoal":
                    goal = new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3]));
                    break;
                case "EternalGoal":
                    goal = new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
                    break;
                case "ChecklistGoal":
                    goal = new ChecklistGoal(parts[0], parts[1], int.Parse(parts[2]),
                                             int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    break;
                case "NegativeGoal":
                    goal = new NegativeGoal(parts[0], parts[1], int.Parse(parts[2]));
                    break;
            }

            if (goal != null)
                _goals.Add(goal);
        }

        Console.WriteLine($"  ✅ Loaded {_goals.Count} goals for \"{_playerName}\" with {_score} pts.");
    }

    // ── Leveling (EXTRA CREDIT) ───────────────────────────────────────────────

    private string GetLevelTitle(int score)
    {
        string title = _levels[0].title;
        foreach (var (threshold, levelTitle) in _levels)
        {
            if (score >= threshold)
                title = levelTitle;
        }
        return title;
    }

    private string GetLevelTitle() => GetLevelTitle(_score);
}
