using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage ?");
        string strgrade = Console.ReadLine();
        int grade = int.Parse(strgrade);
        string Fgrade = "X"; 
        if (grade >= 70)
        {
        if (grade >= 90)
        {Fgrade = "A";}
        else if (grade >= 80)
        {Fgrade = "B";}
        else if (grade >= 70)
        {Fgrade = "C";}
        Console.WriteLine("Congratulations! Your grade was enough to pass!");
        Console.WriteLine($"Your final Grade is: {Fgrade}");
        }
        else {
            if (grade >= 60)
            {Fgrade = "D";}
            else
            {Fgrade = "F";}
            Console.WriteLine("Your grade was not enough to pass. Please try best next time");
            Console.WriteLine($"Your final Grade is: {Fgrade}");
        }
        

    }
}