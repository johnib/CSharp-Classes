using System;


namespace CodeInterview {

	public static class Recursions {

		private const bool TRIVIAL = false;

		public static long fibo(int nth_element) {
			long result;

			if (nth_element <= 2) {
				result = 1;
			} else if (TRIVIAL) {
				result = fibo(nth_element - 1) + fibo(nth_element - 2);
			} else {
				result = fibo_helper(1, 1, 1, nth_element);
			}

			return result;
		}

		private static long fibo_helper(int one, int two, int i, int n) {
			if (i == n) {
				return one;
			} else {
				return fibo_helper(two, one + two, i + 1, n);
			}
		}


	}
}
