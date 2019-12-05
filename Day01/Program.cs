using System;
using System.IO;
using System.Linq;

namespace Day01
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var masses = File.ReadAllLines("Input.txt").ToList().ConvertAll<int>(s => int.Parse(s));

			//divide by 3, round down, subtract 2
			int total = masses.Select(m => (m / 3) - 2).Sum();

			Console.WriteLine(total);

			Console.WriteLine("Part 2");

			int totalMass = 0;
			foreach (var mass in masses)
			{
				int newMass = mass;
				while( (newMass = (newMass / 3) - 2) > 0)
				{
					totalMass += newMass;
				}
			}
			Console.WriteLine(totalMass);


			Console.ReadLine();
		}
	}
}
