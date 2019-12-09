using System;
using System.IO;
using System.Linq;
using IntCode;

namespace Day05
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			//string testProgram = "103,0,4,0,99";
			//Computer testComp = new Computer(testProgram);

			//testComp.ExecuteInstruction(10);

			//Console.WriteLine($"Last Output: {testComp.OutputCodes.Last()}");


			string input = File.ReadAllText("Input.txt");
			//Computer comp = new Computer(input);
			//comp.ExecuteInstruction(1);
			//Console.WriteLine($"Last Output: {comp.OutputCodes.Last()}");

			Computer comp2 = new Computer(input);
			comp2.ExecuteInstruction(5);
			Console.WriteLine($"Last Output: {comp2.OutputCodes.Last()}");

			Console.ReadLine();
		}
	}
}
