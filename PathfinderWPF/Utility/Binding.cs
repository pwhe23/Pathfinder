
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Core.WPF {

	public static class Bindings {

		private static readonly List<DependencyObject> _Fields = new List<DependencyObject>();

		public static readonly DependencyProperty FieldProperty = DependencyProperty.RegisterAttached(
			"Field", typeof(String), typeof(Bindings), new FrameworkPropertyMetadata(null, OnFieldChanged)
		);

		public static String GetField(DependencyObject obj) {
			return (String)obj.GetValue(FieldProperty);
		}

		public static void SetField(DependencyObject obj, String value) {
			obj.SetValue(FieldProperty, value);
		}

		private static void OnFieldChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
			//obj.FindParent<Screen>()._Fields.Add(obj);
			_Fields.Add(obj); //HACK: omg can't find parent
		}

		public static void SetFields(DependencyObject owner) {
			if (DesignerProperties.GetIsInDesignMode(owner)) return;
			var remove = new List<DependencyObject>();
			foreach (FrameworkElement obj in _Fields) {
				if (obj.FindParent(owner.GetType()) != owner || obj.DataContext is Window) continue;
				var field = GetField(obj);
				var prop = obj.DataContext.GetType().GetProperty(field);

				if (obj is TextBox) {
					var tb = obj as TextBox;
					tb.IsReadOnly = (!prop.CanWrite || prop.GetSetMethod(false) == null);
					var binding = new Binding(field);
					if (prop.PropertyType.FullName.Contains("DateTime")) binding.StringFormat = "MM/dd/yyyy";
					binding.Mode = (tb.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay);
					binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					binding.ValidatesOnDataErrors = true;
					binding.ValidatesOnExceptions = true;
					tb.SetBinding(TextBox.TextProperty, binding);

				} else if (obj is Selector) {
					var cb = obj as Selector;
					if (!cb.SelectedValuePath.HasValue()) cb.SelectedValuePath = "Tag";
					var binding = new Binding(field) { Mode = BindingMode.TwoWay };
					binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					binding.ValidatesOnDataErrors = true;
					cb.SetBinding(Selector.SelectedValueProperty, binding);

				} else if (obj is CheckBox) {
					var cb = obj as CheckBox;
					var binding = new Binding(field);
					binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					binding.ValidatesOnDataErrors = true;
					cb.SetBinding(ToggleButton.IsCheckedProperty, binding);

				} else if (obj is DatePicker) {
					var dp = obj as DatePicker;
					var binding = new Binding(field) { Mode = BindingMode.TwoWay };
					binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					binding.ValidatesOnDataErrors = true;
					dp.SetBinding(DatePicker.SelectedDateProperty, binding);

				} else if (obj is ContentControl) {
					var lb = obj as ContentControl;
					var binding = new Binding(field) { Mode = BindingMode.OneWay };
					if (prop.PropertyType == typeof(Decimal)) binding.StringFormat = "{0:C}";
					lb.SetBinding(ContentControl.ContentProperty, binding);

				}
				remove.Add(obj);
			}
			_Fields.RemoveRange(remove);
		}

	};

}
