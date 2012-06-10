
using System;
using System.Windows;
using Core.WPF;

namespace Pathfinder.WPF {

	/// <summary> Interaction logic for PlayerAdventure.xaml </summary>
	public partial class PlayerAdventure {

		public PlayerAdventure() {
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			if (Config.Instance.PlayerAdventure_SplitWidth.HasValue) SplitCol.Width = new GridLength(Config.Instance.PlayerAdventure_SplitWidth.Value);
			if (Config.Instance.PlayerAdventure_SplitHeight.HasValue) SplitRow.Height = new GridLength(Config.Instance.PlayerAdventure_SplitHeight.Value);
			this.FindParent<Window>().Closed += Window_Closed;
		}

		void Window_Closed(object sender, EventArgs e) {
			Config.Instance.PlayerAdventure_SplitWidth = SplitCol.Width.Value;
			Config.Instance.PlayerAdventure_SplitHeight = SplitRow.Height.Value;
		}

	};
}
