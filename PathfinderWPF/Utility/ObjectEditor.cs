
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.WpfPropertyGrid;
using Core;

namespace Pathfinder.WPF {

	/// <summary> The ObjectEditor class provides the mechanisms necessary to edit a naked domain object </summary>
	public class ObjectEditor : ICustomTypeDescriptor {

		public Object Model { get; protected set; }
		private PropertyGrid Editor;

		/// <summary> Load the model and the editor </summary>
		public void Initialize(Object model, PropertyGrid editor) {
			Model = model;
			Editor = editor;

			Editor.SelectedObjectsChanged += Editor_SelectedObjectsChanged;
			Editor.PropertyValueChanged += Editor_PropertyValueChanged;

			LoadEditors(model.GetType());
			Editor.SelectedObject = this;
		}

		public void Refresh(Object model) {
			Model = model;
			Editor.SelectedObject = null;
			Editor.SelectedObject = this;
		}

		/// <summary> Load any special editors for the Type </summary>
		protected virtual void LoadEditors(Type type) {
			foreach (var prop in type.GetProperties()) {
				
				// Load custom editor for Nullable properties
				if (prop.PropertyType.IsGenericType
					&& prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
					&& prop.PropertyType.GetGenericArguments()[0].IsEnum) {
					AddEditor(prop.PropertyType, EditorKeys.EnumEditorKey);
				}

				// Load custom editors for Complex types
				if (!prop.PropertyType.CanConvertFrom<string>()) {
					AddEditor(prop.PropertyType, EditorKeys.ComplexPropertyEditorKey);
				}

			}
		}

		/// <summary> Add the Editor for this PropertyType if not already added </summary>
		protected virtual void AddEditor(Type type, ComponentResourceKey editor) {
			var found = Editor.Editors.FindTypeEditor(type);
			if (found != null) return;
			Editor.Editors.Add(new TypeEditor(type, editor));
		}

		void Editor_SelectedObjectsChanged(object sender, EventArgs e) {

		}

		void Editor_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e) {
			//for some reason this empty event seems to be required in order for the object to be updated
		}

		public AttributeCollection GetAttributes() {
			return TypeDescriptor.GetAttributes(Model);
		}

		public string GetClassName() {
			return TypeDescriptor.GetClassName(Model);
		}

		public string GetComponentName() {
			return TypeDescriptor.GetComponentName(Model);
		}

		public TypeConverter GetConverter() {
			return TypeDescriptor.GetConverter(Model);
		}

		public EventDescriptor GetDefaultEvent() {
			return TypeDescriptor.GetDefaultEvent(Model);
		}

		public PropertyDescriptor GetDefaultProperty() {
			return TypeDescriptor.GetDefaultProperty(Model);
		}

		public object GetEditor(Type editorBaseType) {
			return TypeDescriptor.GetEditor(Model, editorBaseType);
		}

		public EventDescriptorCollection GetEvents() {
			return TypeDescriptor.GetEvents(Model);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) {
			return TypeDescriptor.GetEvents(Model, attributes);
		}

		public PropertyDescriptorCollection GetProperties() {
			return TypeDescriptor.GetProperties(Model);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
			var properties = TypeDescriptor.GetProperties(Model, attributes).Cast<PropertyDescriptor>().ToList();
			for (var i = 0; i < properties.Count; i++) {
				if (!properties[i].PropertyType.CanConvertFrom<string>()) {
					var attr = properties[i].Attributes.Cast<Attribute>().ToList();
					if (!attr.Any(x => x is TypeConverterAttribute)) attr.Add(new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
					properties[i] = TypeDescriptor.CreateProperty(Model.GetType(), properties[i], attr.ToArray());
				}
			}
			return new PropertyDescriptorCollection(properties.ToArray(), true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd) {
			return Model;
		}

	};

}
