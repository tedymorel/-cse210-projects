using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Build a list that holds any Shape (polymorphism)
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("Red", 5));
        shapes.Add(new Rectangle("Blue", 4, 7));
        shapes.Add(new Circle("Green", 3));

        // Iterate and display color and area for each shape
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor()},  Area: {shape.GetArea():F2}");
        }
    }
}