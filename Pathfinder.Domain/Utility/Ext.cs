
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

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

	};

}
