
using System.Collections.Generic;

namespace Pathfinder.Domain {

	/// <summary> The Adventure stores all the information for running an adventure </summary>
	public class Adventure {

		public string Name { get; set; }
		public Map Map { get; set; }
		public List<Player> Players { get; set; }
		public List<Action> Actions { get; set; }

	};

}
