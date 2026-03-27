using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Learn C# Basics", "John Doe", 600);
        Video video2 = new Video("OOP Explained", "Jane Smith", 800);
        Video video3 = new Video("Abstraction in Programming", "Mike Tech", 500);

        // Add comments to video1
        video1.AddComment(new Comment("Alice", "Great video!"));
        video1.AddComment(new Comment("Bob", "Very helpful."));
        video1.AddComment(new Comment("Charlie", "Thanks for explaining clearly."));

        // Add comments to video2
        video2.AddComment(new Comment("David", "OOP is easier now!"));
        video2.AddComment(new Comment("Eva", "Nice examples."));
        video2.AddComment(new Comment("Frank", "Loved it."));

        // Add comments to video3
        video3.AddComment(new Comment("Grace", "Abstraction makes sense now."));
        video3.AddComment(new Comment("Henry", "Good explanation."));
        video3.AddComment(new Comment("Ivy", "Very clear teaching."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display videos
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine("-----------------------------------");
        }
    }
}

// ---------------- VIDEO CLASS ----------------
class Video
{
    private string _title;
    private string _author;
    private int _length;
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");

        Console.WriteLine("Comments:");
        foreach (Comment comment in _comments)
        {
            comment.DisplayComment();
        }
    }
}

// ---------------- COMMENT CLASS ----------------
class Comment
{
    private string _author;
    private string _text;

    public Comment(string author, string text)
    {
        _author = author;
        _text = text;
    }

    public void DisplayComment()
    {
        Console.WriteLine($"{_author}: {_text}");
    }
}