
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.WpfPropertyGrid;
using Core;
using Pathfinder.Domain;

namespace Pathfinder.WPF {

	/// <summary> This control is the screen for editing a player </summary>
	public partial class PlayerEditor {

		protected PlayerManager Model { get; set; }

		public PlayerEditor() {
			InitializeComponent();
			Loaded += PlayerEditor_Loaded;
			Editor.SelectedObjectsChanged += Editor_SelectedObjectsChanged;
			Editor.PropertyValueChanged += Editor_PropertyValueChanged;
			LoadNullableEnums(typeof (Player));
		}

		void Editor_SelectedObjectsChanged(object sender, EventArgs e) {

		}

		void Editor_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e) {
			//for some reason this empty event seems to be required in order for the object to be updated
		}

		private void LoadNullableEnums(Type type) {
			foreach (var prop in type.GetProperties()) {
				if (prop.PropertyType.IsGenericType
					&& prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) 
					&& prop.PropertyType.GetGenericArguments()[0].IsEnum) {
						Editor.Editors.Add(new TypeEditor(prop.PropertyType, EditorKeys.EnumEditorKey));
				}
			}
		}

		void PlayerEditor_Loaded(object sender, RoutedEventArgs e) {
			Model = new PlayerManager();
			Model.Player = new Player();
			Editor.SelectedObject = Model.Player;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			try {
				var method = Model.GetType().GetMethod((sender as Button).Name);
				method.Invoke(Model, new object[0]);
			} catch (Exception ex) {
				MessageBox.Show(ex.InnerException().Message);
			}
			Editor.SelectedObject = Model.Player;
		}

	};

}
