using System;
using System.Linq;

namespace Day04
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"111111 {IsValidPassword("111111".AsSpan())}");
			Console.WriteLine($"223450 {IsValidPassword("223450".AsSpan())}");
			Console.WriteLine($"123789 {IsValidPassword("123789".AsSpan())}");
			Console.WriteLine($"137899 {IsValidPasswordPart2("137899".AsSpan())}");

			int passwordCount = 0;
			int passwordCount2 = 0;
			for (int i = 137683; i < 596253; i++)
			{
				passwordCount += IsValidPassword(i.ToString().AsSpan()) ? 1 : 0;
				passwordCount2 += IsValidPasswordPart2(i.ToString().AsSpan()) ? 1 : 0;
			}
			
			Console.WriteLine($"Password Count {passwordCount}");
			Console.WriteLine($"Password Count Part 2 {passwordCount2}");

			Console.ReadLine();
		}

		static bool IsValidPassword(ReadOnlySpan<char> password)
		{
			//check if ordered
			for (int i = 0; i < password.Length-1; i++)
			{
				if (int.Parse(password.Slice(i, 1)) > int.Parse(password.Slice(i + 1, 1)))
				{
					return false;
				}
			}

			//check if any ordered
			for (int i = 0; i < password.Length-1; i++)
			{
				if (int.Parse(password.Slice(i, 1)) == int.Parse(password.Slice(i + 1, 1)))
				{
					return true;
				}
			}

			return false;
		}

		static bool IsValidPasswordPart2(ReadOnlySpan<char> password)
		{
			//check if ordered
			for (int i = 0; i < password.Length - 1; i++)
			{
				if (int.Parse(password.Slice(i, 1)) > int.Parse(password.Slice(i + 1, 1)))
				{
					return false;
				}
			}

			//check if any ordered

			string currentChar = "*";
			int dupeCount = 0;
			for (int i = 0; i < password.Length; i++)
			{
				if (dupeCount == 2 && currentChar != password.Slice(i, 1).ToString())
				{
					return true;
				}
				else if (currentChar == password.Slice(i, 1).ToString())
				{
					dupeCount++;
				}
				else
				{
					currentChar = password.Slice(i, 1).ToString();
					dupeCount = 1;
				}
			}

			return dupeCount == 2;
			//return false;
		}
	}
}
