using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var cycles = new Dictionary<int, int>();
			var openBrackets = new Stack<int>();
			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[') openBrackets.Push(i);
				if (vm.Instructions[i] == ']')
				{
					var lastOpenBracket = openBrackets.Pop();
					cycles[lastOpenBracket] = i;
					cycles[i] = lastOpenBracket;
				}
			}

			vm.RegisterCommand('[', b => 
			{ 
				if (b.Memory[b.MemoryPointer] == 0) b.InstructionPointer = cycles[b.InstructionPointer]; 
			});
			vm.RegisterCommand(']', b => 
			{
				if (b.Memory[b.MemoryPointer] != 0) b.InstructionPointer = cycles[b.InstructionPointer];
			});
		}
	}
}