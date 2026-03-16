using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Date;
    public string Prompt;
    public string Response;

    public void Display()
    {
        Console.WriteLine($"{Date} - {Prompt}");
        Console.WriteLine(Response);
        Console.WriteLine();
    }
}

class PromptGenerator
{
    public List<string> Prompts = new List<string>()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(Prompts.Count);
        return Prompts[index];
    }
}

class Journal
{
    public List<Entry> Entries = new List<Entry>();

    public void DisplayJournal()
    {
        foreach (Entry entry in Entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter output = new StreamWriter(filename))
        {
            foreach (Entry entry in Entries)
            {
                output.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        Entries.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");

            Entry entry = new Entry();
            entry.Date = parts[0];
            entry.Prompt = parts[1];
            entry.Response = parts[2];

            Entries.Add(entry);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal");
            Console.WriteLine("4. Load journal");
            Console.WriteLine("5. Exit");

            Console.Write("Choose an option: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                string prompt = promptGenerator.GetRandomPrompt();
                Console.WriteLine(prompt);

                Console.Write("> ");
                string response = Console.ReadLine();

                Entry entry = new Entry();
                entry.Date = DateTime.Now.ToShortDateString();
                entry.Prompt = prompt;
                entry.Response = response;

                journal.Entries.Add(entry);
            }

            else if (choice == 2)
            {
                journal.DisplayJournal();
            }

            else if (choice == 3)
            {
                Console.Write("Enter filename to save: ");
                string filename = Console.ReadLine();
                journal.SaveToFile(filename);
            }

            else if (choice == 4)
            {
                Console.Write("Enter filename to load: ");
                string filename = Console.ReadLine();
                journal.LoadFromFile(filename);
            }
        }
    }
}