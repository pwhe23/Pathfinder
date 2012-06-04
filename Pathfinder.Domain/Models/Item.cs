
namespace Pathfinder.Domain {

	/// <summary> An Item in a Room </summary>
	public class Item {

		public string ItemId { get; set; }
		public string Name { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Top { get; set; }
		public int Left { get; set; }
	};

}
