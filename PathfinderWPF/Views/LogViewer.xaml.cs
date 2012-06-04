
using System.Windows;
using System.Windows.Controls;

namespace Pathfinder.WPF.Views {

	/// <summary> Adventure log </summary>
	public partial class LogViewer {

		public LogViewer() {
			InitializeComponent();
			Loaded += LogViewer_Loaded;
		}

		void LogViewer_Loaded(object sender, RoutedEventArgs e) {
			Log.Items.Add(new ListBoxItem { Content = "test2" });
		}

	};

}
