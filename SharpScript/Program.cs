// See https://aka.ms/new-console-template for more information
using SharpScript;

Console.WriteLine("Hello, World!");

var terms = Interpreter.EvaluateParens("( RANGE ( 1 + 3  * ( 3 * 12 ) , 400 ) )".Split(' ').ToList());

foreach (var term in terms)
{
    foreach (var token in term)
    {
        Console.Write(token + ' ');
    }

    Console.WriteLine();
}