using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            int userNumber = -1;

            Console.WriteLine("Enter a list of numbers, type 0 when finished.");

            // 1. Ask the user for numbers and append to list (stopping at 0)
            while (userNumber != 0)
            {
                Console.Write("Enter number: ");
                string response = Console.ReadLine();
                userNumber = int.Parse(response);

                if (userNumber != 0)
                {
                    numbers.Add(userNumber);
                }
            }

            // --- Core Requirements ---

            // Compute the sum
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }

            // Compute the average
            // Note: We cast to float/double to ensure decimal precision
            float average = ((float)sum) / numbers.Count;

            // Find the maximum
            int max = numbers[0];
            foreach (int number in numbers)
            {
                if (number > max)
                {
                    max = number;
                }
            }

            // Output results
            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");
        }
    }
}