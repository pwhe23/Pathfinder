
using System;
using System.Collections;
using System.Windows.Controls;
using Core;
using Pathfinder.Domain;
using Pathfinder.WPF.Views;

namespace Pathfinder.WPF {

	/// <summary> Application startup </summary>
	public partial class App {

		private void Application_Startup(object sender, System.Windows.StartupEventArgs e) {
			Config.Load();
			App.Username = "username";
		}

		public static void SetStatus(string message) {
			Root.SetStatus(message);
		}

		public static void AddLog(string message) {
			LogViewer.Log.Items.Add(new ListBoxItem {
				Content =  message
			});
		}

		public static MainWindow Root {
			get { return (MainWindow)Current.MainWindow; }
		}

		public static LogViewer LogViewer { get; set; }

		protected static Commander _Commander = new Commander();

		public static Object Execute(string command) {
			AddLog(DateTime.Now.ToString("MM/dd/yy h:mmt").ToLower() + " " + Username + ": " + command);
			var result = _Commander.Execute(command);
			if (result == null) {
				//ignore
			} else if (result.GetType().CanConvertFrom<string>()) {
				AddLog(result.ToString());
			} else if (result is IEnumerable) {
				foreach (var value in (IEnumerable)result) {
					if (value != null) AddLog(" > " + value);
				}
			}
			return result;
		}

		private void Application_Exit(object sender, System.Windows.ExitEventArgs e) {
			Config.Instance.Save();
		}

		public static string Username { get; set; }

	};

}
