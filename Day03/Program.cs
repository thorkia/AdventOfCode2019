using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day03
{
	class Program
	{
		//private static string[] testSource = {"R8,U5,L5,D3", "U7,R6,D4,L4"};
		private static readonly Point CentralPoint = Point.Empty;
		static void Main()
		{
			Dictionary<Point, WirePresence> wirePaths = new Dictionary<Point, WirePresence>();
			

			string[] source = File.ReadAllLines("Input.txt");
			//Build Path 1
			List<WireDirection> path1 = source[0].Split(",").ToList()
				.Select(s => new WireDirection(s)).ToList();
			//Build Path 2
			List<WireDirection> path2 = source[1].Split(",").ToList()
				.Select(s => new WireDirection(s)).ToList();
			//Find Intersections

			Dictionary<Point, int> path1StepCount = new Dictionary<Point, int>();
			Point start = CentralPoint;
			Point startStep = CentralPoint;
			int stepCount = 0;
			foreach (var wireDirection in path1)
			{
				start = wireDirection.ApplyDirection(start, wirePaths, WirePresence.WireOne);
				(stepCount, startStep) = wireDirection.ApplyDirection(startStep, path1StepCount, stepCount);
			}

			Dictionary<Point, int> path2StepCount = new Dictionary<Point, int>();
			start = CentralPoint;
			startStep = CentralPoint;
			stepCount = 0;
			foreach (var wireDirection in path2)
			{
				start = wireDirection.ApplyDirection(start, wirePaths, WirePresence.WireTwo);
				(stepCount,startStep) = wireDirection.ApplyDirection(startStep, path2StepCount, stepCount);
			}

			var crossed = wirePaths.Where(w => w.Value == (WirePresence.WireOne | WirePresence.WireTwo)).ToList();

			Console.WriteLine("Min Distance");
			var min = crossed.Select(c => Distance(CentralPoint, c.Key)).Min();
			Console.WriteLine($"{min}");
			
			Console.WriteLine("Min Step Count");
			var minStep = crossed.Select(c => path1StepCount[c.Key] + path2StepCount[c.Key]).Min();
			Console.WriteLine($"{minStep}");

			Console.ReadLine();
		}


		static int Distance(Point start, Point end)
		{
			return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
		}
	}


	[Flags]
	public enum WirePresence
	{
		None = 0,
		WireOne = 1,
		WireTwo = 2,
	}
}
