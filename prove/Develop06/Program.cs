using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private const string GoalsFilePath = "goals.txt";
    private const string ScoreFilePath = "score.txt";
    private int _nextGoalId = 1;

    public GoalManager() 
    {
        LoadScore();
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            DisplayPlayerInfo();
            Console.WriteLine("\n1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("\nSelect a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoalMenu();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEventMenu();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
    }

    private void WaitForUserInput()
    {
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private void CreateGoalMenu()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string goalType = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points for the goal: ");
        int points = int.Parse(Console.ReadLine());

        Goal newGoal = null;

        if (goalType == "3")
        {
            Console.Write("Enter the target number of completions: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points for completing the goal: ");
            int bonus = int.Parse(Console.ReadLine());
            newGoal = new ChecklistGoal(_nextGoalId++, name, description, points, target, bonus);
        }
        else if (goalType == "1")
        {
            newGoal = new SimpleGoal(_nextGoalId++, name, description, points);
        }
        else if (goalType == "2")
        {
            newGoal = new EternalGoal(_nextGoalId++, name, description, points);
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        _goals.Add(newGoal);
        Console.WriteLine($"Goal '{name}' created successfully! Total goals: {_goals.Count}");
        WaitForUserInput();
    }

    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
        }
        else
        {
            foreach (var goal in _goals)
            {
                Console.WriteLine(goal.GetDetailsString());
            }
        }
        WaitForUserInput();
    }

    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter(GoalsFilePath))
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("You should add goals before saving.");
            }
            else
            {
                foreach (var goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
                Console.WriteLine("Goals saved successfully.");
            }
        }
        
        WaitForUserInput();
    }

    public void LoadGoals()
    {
        if (!File.Exists(GoalsFilePath))
        {
            Console.WriteLine("No saved goals found.");
            return;
        }

        using (StreamReader reader = new StreamReader(GoalsFilePath))
        {
            string line;
            int maxId = 0;

            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(':');
                if (parts.Length < 5)
                {
                    Console.WriteLine($"Invalid line format: {line}. Skipping.");
                    continue;
                }

                int id;
                if (!int.TryParse(parts[0], out id))
                {
                    Console.WriteLine($"Invalid ID format for line: {line}. Skipping.");
                    continue;
                }

                maxId = Math.Max(maxId, id);

                string type = parts[1].ToLower();
                string name = parts[2];
                string description = parts[3];
                int points;

                if (!int.TryParse(parts[4], out points))
                {
                    Console.WriteLine($"Invalid points value for goal '{name}': {parts[4]}. Skipping.");
                    continue;
                }

                switch (type)
                {
                    case "simplegoal":
                        bool isComplete = parts.Length > 5 && bool.Parse(parts[5]);
                        _goals.Add(new SimpleGoal(id, name, description, points, isComplete));
                        break;
                    case "eternalgoal":
                        _goals.Add(new EternalGoal(id, name, description, points));
                        break;
                    case "checklistgoal":
                        if (parts.Length < 8)
                        {
                            Console.WriteLine($"Invalid checklist format for goal '{name}'. Expected at least 8 parts but got {parts.Length}. Skipping.");
                            continue;
                        }

                        int target, bonus, amountCompleted;

                        if (!int.TryParse(parts[5], out target) || !int.TryParse(parts[6], out bonus) || !int.TryParse(parts[7], out amountCompleted))
                        {
                            Console.WriteLine($"Invalid target, bonus, or amountCompleted value for checklist goal '{name}'. Skipping.");
                            continue;
                        }

                        _goals.Add(new ChecklistGoal(id, name, description, points, target, bonus, amountCompleted));
                        break;
                    default:
                        Console.WriteLine($"Unknown goal type '{type}' for goal '{name}'. Skipping.");
                        break;
                }
            }

            _nextGoalId = maxId + 1;
        }

        Console.WriteLine("Goals loaded successfully.");
        WaitForUserInput();
    }

    public void RecordEventMenu()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available to record events.");
            WaitForUserInput();
            return;
        }

        Console.WriteLine("Select a goal to record an event:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{_goals[i].GetId()}: {_goals[i].GetDetailsString()}");
        }

        Console.Write("Enter the ID of the goal you completed (or '0' to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
        {
            var selectedGoal = _goals.Find(g => g.GetId() == id);
            if (selectedGoal != null)
            {
                selectedGoal.RecordEvent();
                _score += selectedGoal.Points;

                if (selectedGoal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
                {
                    _score += checklistGoal.BonusPoints;
                    Console.WriteLine($"Bonus points added for completing the checklist goal '{checklistGoal.GetShortName()}'!");
                }

                Console.WriteLine($"Event recorded for '{selectedGoal.GetShortName()}'. You now have {_score} points.");
                SaveScore();
            }
            else
            {
                Console.WriteLine("Goal not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. No event recorded.");
        }

        WaitForUserInput();
    }

    private void SaveScore()
    {
        using (StreamWriter writer = new StreamWriter(ScoreFilePath))
        {
            writer.WriteLine(_score);
        }
    }

    private void LoadScore()
    {
        if (File.Exists(ScoreFilePath))
        {
            using (StreamReader reader = new StreamReader(ScoreFilePath))
            {
                if (int.TryParse(reader.ReadLine(), out int savedScore))
                {
                    _score = savedScore;
                }
            }
        }
    }
}

public abstract class Goal
{
    protected int _id;
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(int id, string name, string description, int points)
    {
        _id = id;
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public virtual string GetStringRepresentation()
    {
        return $"{_id}:{GetType().Name.ToLower()}:{_shortName}:{_description}:{_points}";
    }

    public int GetId() => _id;
    public string GetShortName() => _shortName;
    public int Points => _points;
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(int id, string name, string description, int points, bool isComplete = false) 
        : base(id, name, description, points) 
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        return IsComplete() ? $"[X] {_shortName}: {_description} (Completed)" : $"[ ] {_shortName}: {_description} (Not completed)";
    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()}:{_isComplete}";
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(int id, string name, string description, int points) 
        : base(id, name, description, points) { }

    public override void RecordEvent()
    {
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[ ] {_shortName}: {_description} (Eternal goal)";
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public int BonusPoints => _bonus;

    public ChecklistGoal(int id, string name, string description, int points, int target, int bonus, int amountCompleted = 0) 
        : base(id, name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++;
        }
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        return IsComplete() ? 
            $"[X] {_shortName}: {_description} (Completed {_amountCompleted}/{_target})" : 
            $"[ ] {_shortName}: {_description} (Completed {_amountCompleted}/{_target})";
    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()}:{_target}:{_bonus}:{_amountCompleted}";
    }
}