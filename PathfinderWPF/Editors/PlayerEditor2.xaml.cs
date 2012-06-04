
using System;
using System.Windows;
using System.Windows.Controls;
using Core;
using Core.WPF;
using Pathfinder.Domain;

namespace Pathfinder.WPF {

	/// <summary> A better player editor than my temporary one </summary>
	public partial class PlayerEditor2 {

		protected PlayerManager Model { get; set; }

		public PlayerEditor2() {
			InitializeComponent();
			Loaded += UserControl_Loaded;
		}

		void UserControl_Loaded(object sender, RoutedEventArgs e) {
			Model = new PlayerManager();
			Model.Create();

			DataContext = Model.Player;

			Bindings.SetFields(this);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			try {
				this.ForceBinding();
				var method = Model.GetType().GetMethod((sender as Button).Name);
				method.Invoke(Model, new object[0]);
				DataContext = Model.Player;
				App.SetStatus(method.Name + " Successful");
			} catch (Exception ex) {
				MessageBox.Show(ex.InnerException().Message);
			}
		}

	};
}
