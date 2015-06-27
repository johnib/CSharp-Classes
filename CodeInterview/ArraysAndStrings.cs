using System;
using System.Text;


namespace CodeInterview {

	class ArraysAndStrings {

		private const bool TRIVIAL = false;

		/// <summary>
		/// Checks if the given string has all unique characters
		/// </summary>
		/// <returns><c>true</c>, if string is unique, <c>false</c> otherwise.</returns>
		/// <param name="str">String.</param>
		public static bool isUnique(string str) {
			if (str == null) return false;
			bool isUnique = true;
			int len = str.Length;

			if (TRIVIAL) {
				// using bubble-sort method, O(n^2). n = str.Length.
				for (int i = 0; i < len; i++) {
					char toLookFor = str[i];
					for (int j = i + 1; j < len; j++) {
						char toCheck = str[j];
						if (toLookFor == toCheck) {
							isUnique = false;
							break;
						}
					}
				}
			} else {
				// using an array counter, O(n).
				int[] array = new int[256];
				for (int i = 0; i < len; i++) {
					char toLookFor = str[i];
					int idxToLookUp = (int)toLookFor;
					if (array[idxToLookUp] > 0) {
						isUnique = false;
						break;
					} else {
						array[idxToLookUp]++;
					}
				}
			}

			return isUnique;
		}

		/// <summary>
		/// Reverses the C style string.
		/// </summary>
		/// <returns>The reversed C style string.</returns>
		/// <param name="str">String.</param>
		public static string reverseCStyle(string str) {
			// assuming the last char is '\0'.
			int len = str.Length - 2;
			int halfLen = (len + 1) / 2;

			char[] chars = str.ToCharArray();
			for (int i = 0; i < halfLen; i++) {
				char temp = chars[i];
				chars[i] = chars[len - i];
				chars[len - i] = temp;
			}

			return new string(chars);
		}


		/// <summary>
		/// Removes the dupelicate chars from the given string.
		/// </summary>
		/// <returns>The dupes.</returns>
		/// <param name="str">String.</param>
		public static string removeDupes(string str) {
			string result = null;
			if (str != null) {
				StringBuilder sb = new StringBuilder();
				int len = str.Length;
				for (int i = 0; i < len; i++) {
					char currentChar = str[i];
					bool dupe = false;

					for (int j = i + 1; j < len; j++) {
						char nextChar = str[j];
						if (dupe = (currentChar == nextChar)) {
							break;
						}
					}

					if (!dupe) {
						sb.Append(currentChar);
					}
				}

				result = sb.ToString();
			}

			return result;
		}

		/// <summary>
		/// Checks if both strings are anagrams.
		/// </summary>
		/// <returns><c>true</c>, if anagrams was ared, <c>false</c> otherwise.</returns>
		/// <param name="str1">Str1.</param>
		/// <param name="str2">Str2.</param>
		public static bool areAnagrams(string str1, string str2) {
			int len1 = str1.Length, len2 = str2.Length;
			bool bothAnagrams = true;

			if (len1 == len2) {
				int[] charCounter = new int[256];
				foreach (char c in str1)
					charCounter[(int)c]++;

				foreach (char c in str2)
					charCounter[(int)c]--;

				foreach (int num in charCounter)
					if (num != 0) {
						bothAnagrams = false;
						break;
					}
			} else {
				bothAnagrams = false;
			}

			return bothAnagrams;
		}

		/// <summary>
		/// Replaces the spaces with the string "%20".
		/// </summary>
		/// <returns>The spaces.</returns>
		/// <param name="str">String.</param>
		public static string replaceSpaces(string str) {
			string result = null;
			if (str != null) {
				int len = str.Length;
				StringBuilder sb = new StringBuilder();
				for (int i = len - 1; i >= 0; i--) {
					char currentChar = str[i];
					if (currentChar == ' ') {
						sb.Insert(0, "%20");
					} else {
						sb.Insert(0, currentChar);
					}
				}

				result = sb.ToString();
			}

			return result;
		}

		/// <summary>
		/// Rotates by 90 degrees the specified mat.
		/// </summary>
		/// <param name="mat">Mat.</param>
		public static int[,] rotate90(int[,] mat) {
			int rows = mat.GetLength(0), cols = mat.GetLength(1);
			int[,] rotatedMat = new int[mat.GetLength(0), mat.GetLength(1)];
			for (int j = 0; j < cols; j++) {
				for (int i = 0; i < rows; i++) {
					rotatedMat[rows - i - 1, i] = mat[i, j];
				}
			}

			return rotatedMat;
		}
	}
}

