
namespace Pathfinder.WPF {

	/// <summary> Application startup </summary>
	public partial class App {
		
		public static void SetStatus(string message) {
			Root.SetStatus(message);
		}

		public static MainWindow Root {
			get { return (MainWindow)Current.MainWindow; }
		}

	};

}
