
using System;

namespace Pathfinder.Domain {

	/// <summary> Executes commands </summary>
	public class Commander {
	
		public object Execute(string command) {
			var args = command.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			if (args.Length < 1) return null;

			//TODO: make this generic using type inference (eg if only 1 static method "roll", otherwise "roll dice")
			switch (args[0].ToLower()) {

				case "roll":
					return Dice.Roll(args[1]);

				default: //assume chat
					return command;
			}
		}

	};

}
