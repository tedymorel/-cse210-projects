using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain = "yes";

        // Stretch Challenge 2: Loop to play the whole game again
        while (playAgain.ToLower() == "yes")
        {
            // Core Requirement 3: Generate a random magic number
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = -1;
            int guessCount = 0;

            Console.WriteLine("I'm thinking of a number between 1 and 100.");

            // Core Requirement 2: Loop until the guess matches the magic number
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++; // Stretch Challenge 1: Keep track of guesses

                // Core Requirement 1: Determine higher, lower, or correct
                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            // Display guess count at the end
            Console.WriteLine($"It took you {guessCount} guesses.");

            // Ask to play again
            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine();
            Console.WriteLine(); 
        }

        Console.WriteLine("Thanks for playing!");
    }
}