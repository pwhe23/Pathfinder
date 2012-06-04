
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Core.WPF {

	public static class WpfExt {

		public static GridLength Convert(this GridLengthConverter conv, String value) {
			return (GridLength) conv.ConvertFrom(value);
		}

		public static T FindChild<T>(this FrameworkElement control, string name) where T : FrameworkElement {
			if (control == null) {
				return null;
			}
			else if (control is T && control.Name.Equals(name)) {
				return control as T;
			}
			else if (control is ContentControl) {
				return ((control as ContentControl).Content as FrameworkElement).FindChild<T>(name);
			}
			else if (control is Panel) {
				foreach (FrameworkElement child in (control as Panel).Children) {
					var found = child.FindChild<T>(name) as FrameworkElement;
					if (found != null) return found as T;
				}
			}
			else if (control is ItemsControl) {
				foreach (DependencyObject child in (control as ItemsControl).Items) {
					if (child is FrameworkElement) {
						var found = (child as FrameworkElement).FindChild<T>(name) as FrameworkElement;
						if (found != null) return found as T;
					}
				}
			}
			else if (control is ToolBarTray) {
				foreach (var tb in (control as ToolBarTray).ToolBars) {
					var found = tb.FindChild<T>(name);
					if (found != null) return found;
				}
			}
			return null;
		}

		public static T FindParent<T>(this DependencyObject dep) where T : DependencyObject {
			while ((dep != null) && !(dep is T)) {
				if (dep is FrameworkElement)
					dep = (dep as FrameworkElement).Parent;
				else
					dep = System.Windows.Media.VisualTreeHelper.GetParent(dep);
			}
			return dep as T;
		}

		public static Object FindParent(this DependencyObject dep, Type type) {
			while (dep != null && !type.IsInstanceOfType(dep)) {
				if (dep is FrameworkElement)
					dep = (dep as FrameworkElement).Parent;
				else
					dep = System.Windows.Media.VisualTreeHelper.GetParent(dep);
			}
			return dep;
		}

		public static Button AddButton(this ItemCollection list, String caption, RoutedEventHandler click, String enabled) {
			var button = new Button {Content = caption};
			button.Click += click;
			list.Add(button);

			if (enabled != null) {
				var binding = new Binding(enabled);
				binding.Mode = BindingMode.OneWay;
				button.SetBinding(Button.IsEnabledProperty, binding);
			}

			return button;
		}

		public static void ForceBinding(this UserControl control) {
			control.Focus();
			SendKeys("\t"); //force binding
		}

		public static String Version(this Assembly asm) {
			var version = asm.GetName().Version;
			return version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
		}

		[DllImport("user32.dll")] //sends a windows message to the specified window
		static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, int lParam);

		const ushort WM_KEYDOWN = 0x0100;
		const ushort WM_KEYUP = 0x0101;

		//http://social.msdn.microsoft.com/Forums/en-US/windowssdk/thread/59c947e2-1367-44e8-a086-a86ae0ee55a0/
		public static void SendKeys(String txt) {
			var hwnd = new System.Windows.Interop.WindowInteropHelper(Application.Current.MainWindow).Handle;
			foreach (var key in new UTF8Encoding().GetBytes(txt)) {
				SendMessage(hwnd, WM_KEYDOWN, key, 0);
				SendMessage(hwnd, WM_KEYUP, key, 0);
			}
			System.Windows.Forms.Application.DoEvents();
		}

	};
}
