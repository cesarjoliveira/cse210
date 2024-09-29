using System;
using System.IO; 
using System.ComponentModel.DataAnnotations;
using System.Reflection;

class Program
{
public class Entry
{
    public string Prompt { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    public Entry(string prompt, string content)
    {
        Prompt = prompt;
        Content = content;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Date: {Date.ToShortDateString()} - Prompt: {Prompt} {Content}";
    }
}

public class Journal
{
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(string prompt, string content)
    {
        entries.Add(new Entry(prompt, content));
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void Save(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                outputFile.WriteLine(entry);
            }
        }
    }

    public void Load(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
        int promptStartIndex = line.IndexOf("Prompt: ") + 8;
        int promptEndIndex = line.IndexOf(" ", promptStartIndex);
        string prompt = line[promptStartIndex..promptEndIndex];
        string content = line[(promptEndIndex + 1)..];
        AddEntry(prompt, content);
          }
    }
}


    static void Main(string[] args)
   {
    Journal journal = new Journal();
    int choice = 0;

    while (choice != 5)
    {
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do ?");
        string userentry = Console.ReadLine();
        choice = int.Parse(userentry);

        if (choice == 1)
        {
            string[] prompts = {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?",
            };
            Random randongenerator = new Random();
            int index = randongenerator.Next(prompts.Length);
            Console.WriteLine(prompts[index]);
            string content = Console.ReadLine();
            journal.AddEntry(prompts[index], content);
        }
        else if (choice == 2)
        {
            journal.DisplayEntries();
        }
        else if (choice == 3)
        {
            Console.Write("What is the file name? ");
            string filename = Console.ReadLine();
            journal.Load(filename);
        }
        else if (choice == 4)
        {
            Console.Write("What will be the file name? ");
            string filename = Console.ReadLine();
            journal.Save(filename);
        }
    }
}
}