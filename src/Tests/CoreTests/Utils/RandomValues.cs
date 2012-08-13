using System;
using System.Text;

namespace Procent.Samples.NHibernateStarter.Tests.Core.Utils
{
	public static class RandomValues
	{
		private static readonly Random _random = new Random();

		#region Array

		public static T[] Array<T>(Func<T> create)
		{
			return Array(create, _random.Next(2, 15));
		}

		public static T[] Array<T>(Func<T> create, int length)
		{
			T[] result = new T[length];

			for (int i = 0; i < length; i++)
			{
				result[i] = create();
			}

			return result;
		}

		#endregion

		#region String

		private const int A = 'A';
		private const int z = 'z';

		/// <summary>
		/// Generates a random string of a random length.
		/// </summary>
		public static string String()
		{
			return String(_random.Next(200));
		}

		/// <summary>
		/// Generates a random string of a specified length.
		/// </summary>
		/// <param name="length">Length of the result string.</param>
		public static string String(int length)
		{
			StringBuilder result = new StringBuilder(length);

			for (int i = 0; i < length; i++)
			{
				var charToAppend = (char)_random.Next(A, z + 1);
				if (char.IsLetter(charToAppend) == false)
				{
					i--;
				}
				else
				{
					result.Append(charToAppend);
				}
			}

			return result.ToString();
		}

		#endregion

		#region Numbers

		public static int Number()
		{
			return _random.Next();
		}

		public static int Number(int min, int max)
		{
			return _random.Next(min, max);
		}

		public static int Number(int max)
		{
			return _random.Next(max);
		}

		public static double Double()
		{
			return _random.NextDouble();
		}

		#endregion

		#region TimeSpan

		public static TimeSpan TimeSpan()
		{
			return new TimeSpan(Number());
		}

		#endregion

		#region Guid

		public static Guid Guid()
		{
			return System.Guid.NewGuid();
		}

		#endregion

		#region DateTime

		public static DateTime DateTime()
		{
			return new DateTime(
				Number(2000, 2010), Number(1, 13), Number(1, 25), Number(0, 24), Number(0, 60), Number(0, 60)
				);
		}

		#endregion
	}
}