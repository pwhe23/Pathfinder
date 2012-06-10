
using System;
using System.Collections.Generic;
using Core;

namespace Pathfinder.Domain {

	public class Dice {

		private static readonly Random _Randomizer;

		static Dice() {
			_Randomizer = new MersenneTwister();
		}

		public int? Sides { get; set; }
		public int? Count { get; set; }
		
		public Dice() {}

		public Dice(int sides, int count) {
			Sides = sides;
			Count = count;
		}

		public IEnumerable<int> Roll() {
			var list = new List<int>();
			if (!Sides.HasValue || !Count.HasValue) return list;
			for (var i = 0; i < Count.Value; i++) {
				list.Add(_Randomizer.Next(1, Sides.Value));
			}
			return list;
		}

	};
}
