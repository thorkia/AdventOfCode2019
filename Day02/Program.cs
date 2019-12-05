using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			Func<List<int>> getCodes = () => File.ReadAllText("Input.txt").Split(',').ToList().ConvertAll<int>(s => int.Parse(s));

			var codes = getCodes();
			//List<int> codes = new List<int>() { 1, 1, 1, 4, 99, 5, 6, 0, 99 };

			//Day 1 instructions
			codes[1] = 12;
			codes[2] = 2;

			for (int loc = 0; loc < codes.Count; loc += 4)
			{
				if (codes[loc] == 1)
				{
					var input1Loc = codes[loc + 1];
					var input2Loc = codes[loc + 2];
					var storageLoc = codes[loc + 3];

					codes[storageLoc] = codes[input1Loc] + codes[input2Loc];
				}
				else if (codes[loc] == 2)
				{
					var input1Loc = codes[loc + 1];
					var input2Loc = codes[loc + 2];
					var storageLoc = codes[loc + 3];

					codes[storageLoc] = codes[input1Loc] * codes[input2Loc];
				}
				else if (codes[loc] == 99)
				{
					break;
				}
				else
				{
					Console.WriteLine($"error {codes[loc]}");
				}


			}
			Console.WriteLine(codes[0]);

			Console.WriteLine("Part 2");

			int noun = 0;
			int verb = 0;
			bool hitTarget = false;
			for (noun = 0; noun < 100; noun++)
			{
				for (verb = 0; verb < 100; verb++)
				{
					var codes2 = getCodes();
					codes2[1] = noun;
					codes2[2] = verb;
					DoProgram(codes2);

					if (codes2[0] == 19690720)
					{
						hitTarget = true;
						break;
					}
				}

				if (hitTarget)
				{
					Console.WriteLine("Hit 19690720");
					break;
				}

			}
			
			Console.WriteLine($"{ (100 * noun) + verb}");

			Console.ReadLine();
		}

		public static void DoProgram(List<int> codes)
		{
			for (int loc = 0; loc < codes.Count; loc += 4)
			{
				if (codes[loc] == 1)
				{
					var input1Loc = codes[loc + 1];
					var input2Loc = codes[loc + 2];
					var storageLoc = codes[loc + 3];

					codes[storageLoc] = codes[input1Loc] + codes[input2Loc];
				}
				else if (codes[loc] == 2)
				{
					var input1Loc = codes[loc + 1];
					var input2Loc = codes[loc + 2];
					var storageLoc = codes[loc + 3];

					codes[storageLoc] = codes[input1Loc] * codes[input2Loc];
				}
				else if (codes[loc] == 99)
				{
					break;
				}
				else
				{
					Console.WriteLine($"error {codes[loc]}");
				}


			}
		}
	}
}
