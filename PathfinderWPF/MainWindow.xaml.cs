
namespace Pathfinder.WPF {

	/// <summary> Root of the app </summary>
	public partial class MainWindow {

		public MainWindow() {
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
			Content = new PlayerEditor();
		}

	};

}
