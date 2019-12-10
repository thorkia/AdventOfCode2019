using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Day06
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			//var orbitsList = File.ReadAllLines("testInput.txt");
			//var orbitsList = File.ReadAllLines("testInput-Day2.txt");
			var orbitsList = File.ReadAllLines("Input.txt");

			Dictionary<string, Orbit> orbitalDictionary = new Dictionary<string, Orbit>();

			//Build Orbit Tree
			foreach (var orbitPair in orbitsList)
			{
				var splitPair = orbitPair.Split(')');

				if (!orbitalDictionary.ContainsKey(splitPair[0]))
				{
					orbitalDictionary[splitPair[0]] = new Orbit(splitPair[0], null);
				}

				if (orbitalDictionary.ContainsKey(splitPair[1]))
				{
					orbitalDictionary[splitPair[1]].Orbits = orbitalDictionary[splitPair[0]];
				}
				else
				{
					orbitalDictionary[splitPair[1]] = new Orbit(splitPair[1], orbitalDictionary[splitPair[0]]);
				}
			}

			//Do the count
			int count = 0;
			foreach (var orbitalObject in orbitalDictionary.Values)
			{
				count += orbitalObject.GetOrbitCount();
			}

			var you = orbitalDictionary["YOU"];
			var santa = orbitalDictionary["SAN"];
			int steps = 0;

			while (you.Orbits.Name != santa.Orbits.Name)
			{
				if (!you.IsInPath(santa.Orbits.Name) && !santa.IsInPath(you.Orbits.Name))
				{
					you.Orbits = you.Orbits.Orbits;
					steps++;
				}
				else if (santa.IsInPath(you.Orbits.Name) && (you.Orbits.Name != santa.Orbits.Name))
				{
					santa.Orbits = santa.Orbits.Orbits;
					steps++;
				}
			}

			Console.WriteLine($"Steps: {steps}");


			Console.ReadLine();
		}
	}

	public class Orbit
	{
		public string Name { get; private set; }

		public Orbit Orbits { get; set; }

		public Orbit(string name, Orbit orbits)
		{
			Name = name;
			Orbits = orbits;
		}

		public bool IsInPath(string name)
		{
			if (Orbits == null)
			{
				return false;
			}

			if (Orbits.Name == name)
			{
				return true;
			}

			return Orbits.IsInPath(name);
		}

		public int GetOrbitCount()
		{
			var orbit = Orbits;
			int count = 0;
			while (orbit != null)
			{
				count++;
				orbit = orbit.Orbits;
			}

			return count;
		}
	}
}
