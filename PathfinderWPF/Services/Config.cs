
using System.IO;
using System.Windows;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pathfinder.WPF {

	public class Config {

		private static readonly string FilePath = "Config.js";
		public static Config Instance { get; protected set; }

		public static void Load() {
			if (!File.Exists(FilePath)) {
				Instance = new Config();
				return;
			}
			var data = File.ReadAllText(FilePath);
			Instance = JsonConvert.DeserializeObject<Config>(data, Settings());
		}

		public void Save() {
			_Type = GetType().GetTypeName();
			var json = JsonConvert.SerializeObject(this, Formatting.Indented, Settings());
			File.WriteAllText(FilePath, json);
		}

		private static JsonSerializerSettings Settings() {
			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new StringEnumConverter());
			return settings;
		}

		public string _Type { get; set; }

		public int? DiceRoller_Count { get; set; }
		public int? DiceRoller_Sides { get; set; }

		public WindowState? MainWindow_WindowState { get; set; }
		public double? MainWindow_Width { get; set; }
		public double? MainWindow_Height { get; set; }

		public double? PlayerAdventure_SplitWidth { get; set; }
		public double? PlayerAdventure_SplitHeight { get; set; }

	}

}
