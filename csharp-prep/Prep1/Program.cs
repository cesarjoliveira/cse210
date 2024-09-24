using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string Fname = Console.ReadLine();
        Console.Write("What is your last name? "); 
        string Lname = Console.ReadLine();

        Console.WriteLine($"Your name is {Lname}, {Fname} {Lname}.");
    }
}