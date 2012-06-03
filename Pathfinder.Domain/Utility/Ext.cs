
using System;
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

	};

}
