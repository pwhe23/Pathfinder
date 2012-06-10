
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Core {

	public static class Ext {
		
		public static Exception InnerException(this Exception ex) {
			return ex == null || ex.InnerException == null ? ex : InnerException(ex.InnerException);
		}

		public static bool CanConvertFrom<T>(this Type type) {
			return CanConvertFrom(type, typeof(T));
		}
		public static bool CanConvertFrom(this Type to, Type from) {
			return TypeDescriptor.GetConverter(to).CanConvertFrom(from);
		}

		public static void Each<T>(this IEnumerable<T> list, Action<T> func) {
			foreach (T item in list) func(item);
		}

		public static void RemoveRange<T>(this IList<T> list, IEnumerable<T> items) {
			items.Each(i => list.Remove(i));
		}

		public static int[] upto(this int start, int end) {
			return step(start, end, 1);
		}
		public static int[] downto(this int start, int end) {
			return step(start, end, -1);
		}
		public static int[] step(this int start, int end, int step) {
			var list = new List<int>();
			for (int i = start; i <= end; i += step) {
				list.Add(i);
			}
			return list.ToArray();
		}

		public static IList AddRange(this IList list, IEnumerable items) {
			if (list == null || items == null) return null;
			foreach (var item in items) {
				list.Add(item);
			}
			return list;
		}

		public static bool HasValue(this object obj) {
			if (obj == null) return false;
			if (obj is String) return HasValue(obj as String);
			return true;
		}
		public static bool HasValue(this String txt) {
			return !String.IsNullOrWhiteSpace(txt);
		}
		public static bool HasValue(this DateTime date) {
			return (date != DateTime.MinValue);
		}

		public static string GetTypeName(this Type type) {
			if (type == null) return null;
			return type.FullName + ", " + type.Assembly.GetName().Name;
		}

		public static T GetAttribute<T>(this ICustomAttributeProvider type) where T : Attribute {
			var arr = (T[])type.GetCustomAttributes(typeof(T), false);
			return arr.Length > 0 ? arr[0] : null;
		}

		public static bool HasAttribute<T>(this ICustomAttributeProvider type) where T : Attribute {
			var arr = (T[])type.GetCustomAttributes(typeof(T), false);
			return arr.Length > 0;
		}

		public static bool Is(this String one, params String[] two) {
			foreach (var txt in two) {
				if ((one ?? String.Empty).Equals((txt ?? String.Empty), StringComparison.InvariantCultureIgnoreCase)) return true;
			}
			return false;
		}

		public static bool In(this String one, params String[] two) {
			return two.Any(txt => one.Is(txt));
		}

		public static bool Contains(this String txt, params string[] arr) {
			if (!txt.HasValue() || arr.Length == 0) return false;
			foreach (var s in arr) {
				if (txt.Contains(s)) return true;
			}
			return false;
		}

		public static bool contains(IEnumerable<String> txt, params string[] arr) {
			if (txt == null || arr.Length == 0) return false;
			foreach (var s in arr) {
				if (txt.Contains(s)) return true;
			}
			return false;
		}

		public static bool contains(this string txt, string to) {
			if (!txt.HasValue() || !to.HasValue()) return false;
			return txt.IndexOf(to, StringComparison.CurrentCultureIgnoreCase) != -1;
		}

		public static T To<T>(this Object obj) {
			var value = To(obj, typeof(T));
			return (value == null && !typeof(T).FullName.Contains("String") ? default(T) : (T)value);
		}

		public static Object To(Object value, PropertyInfo prop) {
			if (value != null && prop.PropertyType.IsAssignableFrom(value.GetType())) return value;
			var tca = prop.GetAttribute<TypeConverterAttribute>();
			if (tca != null) {
				var tc = (TypeConverter)Activator.CreateInstance(Type.GetType(tca.ConverterTypeName));
				return tc.ConvertFrom(value);
			}
			return value.To(prop.PropertyType);
		}

		public static Object To(this Object obj, Type type) {
			if (obj == null || type == null) return obj;
			if (obj is String && type.FullName.Contains("Boolean")) {
				if ((obj as String).HasValue() && obj.ToString().Contains("True", "true", "on", "ON")) return true;
				else return false;
			}
			if (obj is String && (string)obj == String.Empty && !type.FullName.Contains("String")) {
				obj = null;
			}
			if (obj != null && obj.GetType() == type) {
				return obj;
			} else if (obj != null && type.IsEnum) {
				return Enum.Parse(type, (String)obj);
			} else if (obj != null && (type == typeof(decimal?) || type == typeof(decimal))) {
				return decimal.Parse(obj.ToString(), NumberStyles.Any);
			} else if (type.IsGenericType && type.GetGenericTypeDefinition().Name.Contains("Nullable")) {
				if (obj == null) return null;
				var nullableConverter = new NullableConverter(type);
				type = nullableConverter.UnderlyingType;
			} else if (obj is byte[] && type == typeof(String)) {
				return System.Net.IPAddress.NetworkToHostOrder(BitConverter.ToInt64((byte[])obj, 0)).To(type);
			} else if (obj == null) {
				// && !type.IsClass && !type.IsByRef
				return null;
			} else if (!typeof(IConvertible).IsAssignableFrom(type)) {
				return obj;
			} else if (obj is DateTime && type.FullName.Contains("String")) {
				if ((DateTime)obj == DateTime.MinValue) return "";
				return Convert.ChangeType(((DateTime)obj).ToString("MM/dd/yyyy hh:mm tt"), type);
			}
			try {
				return Convert.ChangeType(obj, type);
			} catch (Exception ex) {
				throw new Exception("Can't convert " + obj + " to type " + type, ex);
			}
		}

	};

}
