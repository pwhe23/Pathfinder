
using System.Windows;
using System.Windows.Controls;

namespace Pathfinder.WPF {

	/// <summary> Root of the app </summary>
	public partial class MainWindow {

		public MainWindow() {
			InitializeComponent();
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e) {
			MenuItem_Click(PlayerAdventure, null);
			Width = Config.Instance.MainWindow_Width ?? Width;
			Height = Config.Instance.MainWindow_Height ?? Height;
			WindowState = Config.Instance.MainWindow_WindowState ?? WindowState.Normal;
			Username.Text = App.Username;
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

		private void Window_Closed(object sender, System.EventArgs e) {
			Config.Instance.MainWindow_WindowState = WindowState;
			if (WindowState == WindowState.Normal) {
				Config.Instance.MainWindow_Width = Width;
				Config.Instance.MainWindow_Height = Height;
			}
		}

	};

}
