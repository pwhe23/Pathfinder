
using System.Windows;
using Core.WPF;
using Pathfinder.Domain;

namespace Pathfinder.WPF {

	/// <summary> A better player editor than my temporary one </summary>
	public partial class PlayerEditor2 {

		public PlayerEditor2() {
			InitializeComponent();
			Loaded += UserControl_Loaded;
		}

		void UserControl_Loaded(object sender, RoutedEventArgs e) {
			DataContext = new Player { Name = "Hello World" };
			Bindings.SetFields(this);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			
		}

	};
}
