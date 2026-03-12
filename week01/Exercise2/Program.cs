using System;

class Program
{
    static void Main(string[] args)
    {
        // Core Requirement 1: Ask for user input
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int percent = int.Parse(input);

        string letter = "";
        string sign = "";

        // Core Requirement 3: Use a variable to hold the letter
        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // --- Stretch Challenges ---

        // 1. Get the last digit using the modulus (%) operator
        int lastDigit = percent % 10;

        if (lastDigit >= 7)
        {
            sign = "+";
        }
        else if (lastDigit < 3)
        {
            sign = "-";
        }
        else
        {
            sign = "";
        }

        // 2 & 3. Handle exceptional cases (A+, F+, F-)
        if (letter == "A" && sign == "+")
        {
            sign = ""; // There is no A+
        }

        if (letter == "F")
        {
            sign = ""; // There is no F+ or F-
        }

        // Final Grade Output
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // --- Core Requirement 2: Pass/Fail Logic ---
        if (percent >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep working hard for next time.");
        }
    }
}
