
using System.Windows;
using System.Windows.Controls;

namespace Pathfinder.WPF {

	/// <summary> Root of the app </summary>
	public partial class MainWindow {

		public MainWindow() {
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e) {
			MenuItem_Click(PlayerAdventure, null);
		}

		public void SetStatus(string message) {
			Status.Text = message;
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e) {
			var item = sender as MenuItem;
			if (item == null) {
			} else if (item.Name == "Exit") {
				Application.Current.Shutdown();
			} else if (item.Name == "PlayerAdventure") {
				SetContent(new PlayerAdventure());
			} else if (item.Name == "PlayerEditor") {
				SetContent(new PlayerEditor());
			} else if (item.Name == "PlayerEditor2") {
				SetContent(new PlayerEditor2());
			}
		}

		private void SetContent(Control control) {
			Container.Content = control;
		}

	};

}
