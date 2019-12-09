using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IntCode
{
	public class Computer
	{
		public IReadOnlyList<int> OutputCodes => _outputCodes;

		private int _instructionPointer = 0;
		private List<int> _outputCodes = new List<int>();

		private Memory<int> _codes;

		public Computer(string codeSource)
		{
			_codes = new Memory<int>(codeSource.Split(',').ToList().ConvertAll<int>(s => int.Parse(s)).ToArray());
		}

		public void ExecuteInstruction(int input)
		{
			while (_codes.Span[_instructionPointer] != 99)
			{
				var op = new Operation(_codes.Span[_instructionPointer]);
				//Extract Parameter modes
				switch (op.OperationCode)
				{
					case OpCode.Add:
						Add(op);
						break;
					case OpCode.Multiply:
						Multiply(op);
						break;
					case OpCode.Input:
						Input(input);
						break;
					case OpCode.Output:
						Output(op);
						break;
					case OpCode.JumpIfTrue:
						JumpIfTrue(op);
						break;
					case OpCode.JumpIfFalse:
						JumpIfFalse(op);
						break;
					case OpCode.LessThan:
						LessThan(op);
						break;
					case OpCode.EqualsOp:
						EqualsOp(op);
						break;
				}
			}
		}

		

		private void Multiply(Operation op)
		{
			int value1 = ExtractValue(op.Parameter1Mode, _instructionPointer + 1);
			int value2 = ExtractValue(op.Parameter2Mode, _instructionPointer + 2);

			int storageLocation = _codes.Span[_instructionPointer + 3];

			_codes.Span[storageLocation] = value1 * value2;
			_instructionPointer += 4;
		}
		
		private void Add(Operation op)
		{
			int value1 = ExtractValue(op.Parameter1Mode, _instructionPointer+1);
			int value2 = ExtractValue(op.Parameter2Mode, _instructionPointer+2);
			
			int storageLocation = _codes.Span[_instructionPointer + 3];

			_codes.Span[storageLocation] = value1 + value2;
			_instructionPointer += 4;
		}

		private void Input(int value)
		{
			int location = _codes.Span[_instructionPointer + 1];
			_codes.Span[location] = value;

			_instructionPointer += 2;
		}

		private void Output(Operation op)
		{
			int value = ExtractValue(op.Parameter1Mode, _instructionPointer + 1);
			_outputCodes.Add(value);

			_instructionPointer += 2;
		}

		private void JumpIfTrue(Operation op)
		{
			int value = ExtractValue(op.Parameter1Mode, _instructionPointer + 1);

			if (value != 0)
			{
				_instructionPointer = ExtractValue(op.Parameter2Mode, _instructionPointer + 2);
			}
			else
			{
				_instructionPointer += 3;
			}
		}

		private void JumpIfFalse(Operation op)
		{
			int value = ExtractValue(op.Parameter1Mode, _instructionPointer + 1);

			if (value == 0)
			{
				_instructionPointer = ExtractValue(op.Parameter2Mode, _instructionPointer+2);
			}
			else
			{
				_instructionPointer += 3;
			}
		}

		private void LessThan(Operation op)
		{
			int value1 = ExtractValue(op.Parameter1Mode, _instructionPointer + 1);
			int value2 = ExtractValue(op.Parameter2Mode, _instructionPointer + 2);
			
			int storageLocation = _codes.Span[_instructionPointer + 3];
			if (value1 < value2)
			{
				_codes.Span[storageLocation] = 1;
			}
			else
			{
				_codes.Span[storageLocation] = 0;
			}

			_instructionPointer += 4;
		}

		private void EqualsOp(Operation op)
		{
			int value1 = ExtractValue(op.Parameter1Mode, _instructionPointer + 1);
			int value2 = ExtractValue(op.Parameter2Mode, _instructionPointer + 2);

			int storageLocation = _codes.Span[_instructionPointer + 3];
			if (value1 == value2)
			{
				_codes.Span[storageLocation] = 1;
			}
			else
			{
				_codes.Span[storageLocation] = 0;
			}

			_instructionPointer += 4;
		}


		private int ExtractValue(int parameterMode, int instructionPointer)
		{
			if (parameterMode == 1)
			{
				return _codes.Span[instructionPointer];
			}
			else
			{
				int location = _codes.Span[instructionPointer];
				return _codes.Span[location];
			}
		}

		private enum OpCode
		{
			Add = 1,
			Multiply = 2,
			Input = 3,
			Output = 4,
			JumpIfTrue = 5,
			JumpIfFalse = 6,
			LessThan = 7,
			EqualsOp = 8,
		}

		private class Operation
		{
			public OpCode OperationCode { get; private set; }

			public bool HasParameterMode { get; private set; }

			public int Parameter1Mode { get; private set; }
			public int Parameter2Mode { get; private set; }
			public int Parameter3Mode { get; private set; }

			public Operation(int operation)
			{
				string opString = operation.ToString("00000");

				OperationCode = (OpCode) int.Parse(opString.Substring(3));

				Parameter1Mode = int.Parse(opString.Substring(2, 1));
				Parameter2Mode = int.Parse(opString.Substring(1, 1));
				Parameter3Mode = int.Parse(opString.Substring(0, 1));

				if (Parameter1Mode == 1 || Parameter2Mode == 1 || Parameter3Mode == 1)
				{
					HasParameterMode = true;
				}
			}
			
		}
	}
}
