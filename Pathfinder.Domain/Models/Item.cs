
namespace Pathfinder.Domain {

	/// <summary> An Item in a Room </summary>
	public class Item {

		public string ObjectEntityID { get; set; }
		
		public string Name { get { return Entity.Name; } }
		public int Width { get { return Entity.Width; } }
		public int Height { get { return Entity.Height; } }

		private IEntity Entity {
			get { return null; } //get the referenced entity for Name and such
		}

	};

}
