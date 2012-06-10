
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.WPF;

namespace Pathfinder.WPF.Views {

	/// <summary> Adventure log </summary>
	public partial class LogViewer {

		public LogViewer() {
			InitializeComponent();
			if (this.IsDesignMode()) return;
			Loaded += LogViewer_Loaded;
		}

		void LogViewer_Loaded(object sender, RoutedEventArgs e) {
			App.LogViewer = this;
			App.AddLog("test3");
		}

		private void Command_KeyUp(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) Button_Click(Go, null);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			switch ((sender as Button).Name) {
				case "Go":
					App.Execute(Command.Text);
					Command.Clear();
					break;
			}
		}

	};

}
