
using System.Windows;
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

	};

}
