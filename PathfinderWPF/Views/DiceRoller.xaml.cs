
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Core;
using Core.WPF;
using Pathfinder.Domain;

namespace Pathfinder.WPF.Views {

	/// <summary> Interaction logic for DiceRoller.xaml </summary>
	public partial class DiceRoller {

		public DiceRoller() {
			InitializeComponent();
			if (this.IsDesignMode()) return;
			SetState();
			Loaded += DiceRoller_Loaded;
		}

		private Dice Model;

		void DiceRoller_Loaded(object sender, RoutedEventArgs e) {
			Model = new Dice();
			DataContext = Model;

			Count.Items.AddRange(1.upto(6).Select(x => new ComboBoxItem { Tag=x, Content = x.ToString() }));
			Sides.Items.AddRange(new[]{4,6,8,10,12,20}.Select(x => new ComboBoxItem { Tag=x, Content = x.ToString() }));

			Model.Count = Config.Instance.DiceRoller_Count;
			Model.Sides = Config.Instance.DiceRoller_Sides;

			this.FindParent<Window>().Closed += Window_Closed;
			Bindings.SetFields(this);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			switch ((sender as FrameworkElement).Name) {
				case "Clear":
					Rolls.Children.Clear();
					break;
				case "Roll":
					this.ForceBinding();
					Rolls.Children.Clear();
					foreach (var i in (IEnumerable<int>)App.Execute("roll " + Model.Count + "d" + Model.Sides)) {
						Rolls.Children.Add(new Label {
							Content = i.ToString(), 
							Background = new SolidColorBrush(Colors.White),
							Margin = new Thickness(2, 1, 2, 1),
							Padding = new Thickness(5, 1, 5, 1),
							BorderBrush = new SolidColorBrush(Colors.Black),
							BorderThickness = new Thickness(1),
						});
					}
					break;
			}
			SetState();
		}

		private void SetState() {
			Clear.Visibility = Rolls.Children.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
		}

		void Window_Closed(object sender, System.EventArgs e) {
			Config.Instance.DiceRoller_Count = Model.Count;
			Config.Instance.DiceRoller_Sides = Model.Sides;
		}

	};
}
