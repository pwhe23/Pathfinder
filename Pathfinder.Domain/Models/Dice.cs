
using System;
using System.Collections.Generic;
using System.Linq;
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

		/// <summary>
		/// Executes the "roll" command
		/// </summary>
		/// <param name="prms">Expects something like "1d6".</param>
		/// <returns>An array of random numbers</returns>
		public static IEnumerable<int> Roll(string prms) {
			if (!prms.HasValue()) return new List<int>();
			var args = prms.ToLower().Split('d').Select(x => x.To<int?>()).ToList();
			if (args.Count != 2) return new List<int>();

			var dice = new Dice {
				Count = args[0],
				Sides = args[1],
			};
			return dice.Roll();
		} 

	};
}
