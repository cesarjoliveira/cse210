using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        Console.WriteLine("Enter number: ");
        string input = Console.ReadLine();
        int value = int.Parse(input);
        List<int> numbers = new List<int>();
        while (value != 0) {
            numbers.Add(value);
            Console.WriteLine("Enter number: ");
            input = Console.ReadLine();
            value = int.Parse(input);
        }
        int accumulator = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            accumulator = accumulator + numbers[i];
        }
        int sum = accumulator;
        float average = (float)sum/numbers.Count;
        numbers.Sort();
        int highest = numbers[numbers.Count-1];
        Console.WriteLine($"The sum is: {sum}.");
        Console.WriteLine($"The average is: {average}.");
        Console.WriteLine($"The largest number is: {highest}.");
    }
}