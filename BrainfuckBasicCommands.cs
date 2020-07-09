using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			var lettersAndNumbers = ("abcdefghijklmnopqrstuvwxyz".ToUpper()
										  + "abcdefghijklmnopqrstuvwxyz"
										  + "0123456789").ToCharArray();

			vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
			vm.RegisterCommand('+', b => b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + 1) % 256));
			vm.RegisterCommand('-', b => b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + 256 - 1) % 256));
			vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte)read());
			vm.RegisterCommand('>', b => b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length);
			vm.RegisterCommand('<', b => b.MemoryPointer = (b.MemoryPointer + b.Memory.Length - 1) % b.Memory.Length);
			foreach (var e in lettersAndNumbers)
				vm.RegisterCommand(e, b => vm.Memory[vm.MemoryPointer] = (byte)e);
		}
	}
}