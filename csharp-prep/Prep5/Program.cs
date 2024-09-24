using System;
using System.Reflection.Metadata;

class Program
{
    static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Program!");
        }
        
        static string PromptUserName()
        { 
            Console.Write($"Please enter your name: ");
            string userName = Console.ReadLine();
            return userName;
        }
        static int PromptUserNumber()
        {
            Console.Write($"Hello user!, Please Insert your favorite number: ");
            string userNumberstr = Console.ReadLine();
            int userNumber = int.Parse(userNumberstr);
            return userNumber;
        }
        static int SquareNumber(int userNumber)
        {
            int squared = userNumber * userNumber;
            return squared;
        } 
        static void DisplayResult(string userName, int squared)
        {
            Console.Write($"{userName}, the square of your number is {squared}");
        }
    static void Main(string[] args)
    {
        DisplayWelcome();
        string userName = PromptUserName(); 
        int userNumber = PromptUserNumber();
        int squared = SquareNumber(userNumber);
        DisplayResult(userName, squared);

        
    }
}