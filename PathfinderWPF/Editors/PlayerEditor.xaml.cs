
using System;
using System.Windows;
using System.Windows.Controls;
using Core;
using Core.WPF;
using Pathfinder.Domain;

namespace Pathfinder.WPF {

	/// <summary> This control is the screen for editing a player </summary>
	public partial class PlayerEditor {

		protected PlayerManager Model { get; set; }
		private readonly ObjectEditor _Editor;

		public PlayerEditor() {
			InitializeComponent();

			Model = new PlayerManager();
			Model.Create();

			_Editor = new ObjectEditor();
			_Editor.Initialize(Model.Player, Editor);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			try {
				this.ForceBinding();
				var method = Model.GetType().GetMethod((sender as Button).Name);
				method.Invoke(Model, new object[0]);
				_Editor.Refresh(Model.Player);
				App.SetStatus(method.Name + " Successful");
			} catch (Exception ex) {
				MessageBox.Show(ex.InnerException().Message);
			}
		}

	};

}
