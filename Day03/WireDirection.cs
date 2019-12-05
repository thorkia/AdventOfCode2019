using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day03
{
	public class WireDirection
	{
		public Point Vector { get; private set; } = Point.Empty;

		public int Length { get; private set; } = 0;

		public WireDirection(string instruction)
		{
			string direction = instruction.Substring(0, 1);

			switch (direction)
			{
				case "R":
					Vector = new Point(1,0);
					break;
				case "L":
					Vector = new Point(-1,0);
					break;
				case "U":
					Vector = new Point(0,1);
					break;
				case "D":
					Vector = new Point(0,-1);
					break;
				default:
					Vector = Point.Empty;
					break;
			}


			Length = int.Parse(instruction.Substring(1));
		}
	}


	public static class WireDirectionExtensions
	{
		public static Point ApplyDirection(this WireDirection direction, Point position, Dictionary<Point, WirePresence> map,
			WirePresence wireType)
		{
			
			for (int a = 0; a < direction.Length; a++)
			{
				position = new Point(position.X + direction.Vector.X,
					position.Y + direction.Vector.Y);

				if (!map.ContainsKey(position))
				{
					map[position] = WirePresence.None;
				}

				map[position] = map[position] | wireType;
			}

			return position;
		}

		public static (int, Point) ApplyDirection(this WireDirection direction, Point position, Dictionary<Point, int> map,
			int count)
		{

			for (int a = 0; a < direction.Length; a++)
			{
				position = new Point(position.X + direction.Vector.X,
					position.Y + direction.Vector.Y);
				count++;

				if (!map.ContainsKey(position))
				{
					map[position] = 0;
				}

				map[position] = count;
			}

			return (count, position);
		}
	}
}
