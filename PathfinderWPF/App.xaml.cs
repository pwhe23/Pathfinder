
using System;
using System.Windows.Controls;
using Pathfinder.WPF.Views;

namespace Pathfinder.WPF {

	/// <summary> Application startup </summary>
	public partial class App {

		private void Application_Startup(object sender, System.Windows.StartupEventArgs e) {
			Config.Load();
		}

		public static void SetStatus(string message) {
			Root.SetStatus(message);
		}

		public static void AddLog(string message) {
			LogViewer.Log.Items.Add(new ListBoxItem {
				Content = DateTime.Now.ToString("h:mmt").ToLower() + ": " + message
			});
		}

		public static MainWindow Root {
			get { return (MainWindow)Current.MainWindow; }
		}

		public static LogViewer LogViewer { get; set; }

		private void Application_Exit(object sender, System.Windows.ExitEventArgs e) {
			Config.Instance.Save();
		}

	};

}
