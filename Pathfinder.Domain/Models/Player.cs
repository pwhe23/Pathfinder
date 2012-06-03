
namespace Pathfinder.Domain {

	/// <summary> Each character playing the game </summary>
	public class Player : IEntity {

		public string EntityID { get; set; }
		public int Width { get { return 1; } }
		public int Height { get { return 1; } }

		public string Name { get; set; }
		public Alignment? Alignment { get; set; }
		public Gender? Gender { get; set; }
		public Race? Race { get; set; }
		public Class? Class { get; set; }
		public int? XP { get; set; }
		public int? Level { get; set; }

		public Ability Strength { get; set; }
		public Ability Dexterity { get; set; }
		public Ability Constitution { get; set; }
		public Ability Intelligence { get; set; }
		public Ability Wisdom { get; set; }
		public Ability Charisma { get; set; }

	};

	public enum Alignment {
		CG
	};

	public enum Gender {
		Male,
		Female,
	};

	public enum Race {
		Human,
	};

	public enum Class {
		Wizard,
	};

}
