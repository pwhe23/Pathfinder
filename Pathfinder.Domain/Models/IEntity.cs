
namespace Pathfinder.Domain {

	/// <summary> I'm still deciding on Entity vs Item and how the game references things that exist vs where they currently are </summary>
	public interface IEntity {
		string EntityID { get; set; }
		string Name { get; }
		int Width { get; }
		int Height { get; }
	};

}
