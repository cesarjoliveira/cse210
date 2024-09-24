using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is the magic number?");
        Random randongenerator = new Random();
        int magicnumber = randongenerator.Next(1,101);
        Console.Write("What is your guess?");
        string ginput = Console.ReadLine();
        int guess = int.Parse(ginput);
        while (guess != magicnumber) {
        if (magicnumber > guess)
        {
            Console.WriteLine("Higher");
            Console.Write("What is your guess?");
            ginput = Console.ReadLine();
            guess = int.Parse(ginput);
        }
        else if (magicnumber < guess)
        {
            Console.WriteLine("Lower");
            Console.Write("What is your guess?");
            ginput = Console.ReadLine();
            guess = int.Parse(ginput);
        }
        else 
        { 
            Console.WriteLine("You guessed it!");
        }
        }
        Console.WriteLine("You guessed it!");
    }
}