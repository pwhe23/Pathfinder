
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pathfinder.Domain {

	/// <summary> The PlayerManager can save or load player data </summary>
	public class PlayerManager {

		public PlayerManager() {
			FilePath = "PlayerManager.js";
		}

		public string _Type { get; set; }
		public Player Player { get; set; }
		protected string FilePath { get; set; }

		public void Save() {
			_Type = typeof(PlayerManager).FullName + ", " + typeof(PlayerManager).Assembly.GetName().Name;
			var json = JsonConvert.SerializeObject(this, Formatting.Indented, Settings());
			File.WriteAllText(FilePath, json);
		}

		public void Load() {
			if (!File.Exists(FilePath)) throw new Exception("Cannot find file: " + FilePath);
			var data = File.ReadAllText(FilePath);
			var obj = JsonConvert.DeserializeObject<PlayerManager>(data, Settings());
			Player = obj.Player;
		}

		private JsonSerializerSettings Settings() {
			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new StringEnumConverter());
			return settings;
		}

	};

}
