
using System;
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

	};

}
