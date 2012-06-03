
using System;

namespace Core {

	public static class Ext {
		
		public static Exception InnerException(this Exception ex) {
			return ex == null || ex.InnerException == null ? ex : InnerException(ex.InnerException);
		}

	};

}
