using System;
using System.Collections.Generic;
using System.Threading;

class MindfulnessProgram
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            MindfulnessActivity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.StartActivity();
        }
    }
}

abstract class MindfulnessActivity
{
    protected string ActivityName;
    protected string Description;
    protected int Duration;

    public void StartActivity()
    {
        Console.WriteLine($"Activity: {ActivityName}");
        Console.WriteLine($"Description: {Description}");
        Console.Write("Enter duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());

        
        Console.WriteLine("Preparing to begin...");
        DisplayLoadingSpinner(3);

        ExecuteActivity();
        
        EndActivity();
    }

    protected abstract void ExecuteActivity();

    public void EndActivity()
    {
        Console.WriteLine("Good job! You've completed the activity.");
        Console.WriteLine($"Duration: {Duration} seconds.");
        
        
        DisplayLoadingSpinner(1);
    }

    protected void DisplayLoadingSpinner(int seconds)
{
    string loading = "Loading";
    for (int i = 0; i < seconds * 4; i++)
    {
        Console.Write($"\r{loading} {new string('.', i % 4)}");
        Thread.Sleep(250);
    }
    Console.WriteLine(); 
}
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        ActivityName = "Breathing Activity";
        Description = "This activity will help you relax by guiding you through breathing in and out slowly.";
    }

    protected override void ExecuteActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        int breatheInDuration = 4; 
        int breatheOutDuration = 4; 

        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Breathe in...");
            for (int i = breatheInDuration; i > 0; i--)
            
            {
                Console.Write($"{i}");
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine(); 

            
            Console.Write($"Now Breathe out...");
            for (int i = breatheOutDuration; i > 0; i--)
            {
                Console.Write($"{i}");
                Thread.Sleep(1000); 
                Console.Write("\b \b");
            }
            Console.WriteLine(); 
        }
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "How did you feel when it was complete?",
        "What did you learn about yourself through this experience?",
        "Have you ever done anything like this before?",
        "Why was this experience meaningful to you?"
    };

    public ReflectionActivity()
    {
        ActivityName = "Reflection Activity";
        Description = "This activity will help you reflect on your strengths and resilience.";
    }

    protected override void ExecuteActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        Random random = new Random();

        
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        
        
        Console.WriteLine("(Press Enter when you are ready to continue...)");
        Console.ReadLine(); 

        
        foreach (var question in questions)
        {
            if (DateTime.Now >= endTime)
            {
                break; 
            }

            Console.WriteLine(question);
            DisplayLoadingSpinner(5); 
        }
        
        if (DateTime.Now >= endTime)
        {
            Console.WriteLine("Well done!");
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        ActivityName = "Listing Activity";
        Description = "This activity will help you reflect on the good things in your life.";
    }

    protected override void ExecuteActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        DisplayLoadingSpinner(5);

        Console.WriteLine("Start listing items (press Enter after each one). You have " + Duration + " seconds.");
        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }
}