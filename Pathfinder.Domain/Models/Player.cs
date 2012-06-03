
namespace Pathfinder.Domain {

	/// <summary> Each character playing the game </summary>
	public class Player {
		public string Name { get; set; }
		public Alignment? Alignment { get; set; }
		public Gender? Gender { get; set; }
		public Race? Race { get; set; }
		public Class? Class { get; set; }
		public int? XP { get; set; }
		public int? Level { get; set; }
	};

	public enum Alignment {
		CG
	}

	public enum Gender {
		Male,
		Female,
	}

	public enum Race {
		Human,
	}

	public enum Class {
		Wizard,
	}

}
