using System;


namespace CodeInterview {

	public class Singleton {

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CodeInterview.Singleton"/> was created.
		/// </summary>
		/// <value><c>true</c> if was created; otherwise, <c>false</c>.</value>
		private static bool WasCreated { get; set; }

		/// <summary>
		/// Gets or sets the instance.
		/// </summary>
		/// <value>The instance.</value>
		private static Singleton Instance { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Singleton"/> class.
		/// </summary>
		private Singleton() {
		}

		/// <summary>
		/// Init this instance.
		/// </summary>
		public static Singleton init() {
			if (!WasCreated) {
				WasCreated = true;
				Instance = new Singleton();
			}

			return Instance;
		}
	}
}

