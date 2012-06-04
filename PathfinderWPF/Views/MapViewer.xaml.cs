
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Pathfinder.Domain;

namespace Pathfinder.WPF.Views {

	/// <summary> Views a map </summary>
	public partial class MapViewer {

		public Room Model { get; set; }

		public MapViewer() {
			InitializeComponent();

			Model = new Room { Width = 10, Height = 10 };
			DrawMap();
		}

		void DrawMap() {
			Tiles.Children.Clear();
			for (var c = 0; c < Model.Width; c++) {
				Tiles.ColumnDefinitions.Add(new ColumnDefinition());
			}
			for (var r = 0; r < Model.Width; r++) {
				Tiles.RowDefinitions.Add(new RowDefinition());
			}
			for (var c = 0; c < Model.Width; c++) {
				for (var r = 0; r < Model.Width; r++) {
					var tile = new Rectangle();
					tile.Fill = new SolidColorBrush(Colors.Black);
					tile.Margin = new Thickness(1); ;
					tile.SetValue(Grid.RowProperty, r);
					tile.SetValue(Grid.ColumnProperty, c);
					Tiles.Children.Add(tile);
				}
			}
		}

	};

}
