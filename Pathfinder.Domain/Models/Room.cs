
using System.Collections.Generic;

namespace Pathfinder.Domain {

	/// <summary> A Room of a Map can contain multiple Items including Enemies, Players, doors, and other objects </summary>
	public class Room {

		public string Name { get; set; } //optional
		public int Width { get; set; }
		public int Height { get; set; }
		public List<Item> Items { get; set; }

	};

}
