using System;

namespace RoutingRecords
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Checks if the type parameter is asignable from this type.
		/// </summary>
		/// <param name="type">The type object to ckeck.</param>
		/// <param name="t">The type base to asign.</param>
		/// <returns><see cref="true"/> when <see cref="t"/> is assignable from <see cref="type"/>.</returns>
		public static bool Is(this Type type, Type t)
			=> t.IsAssignableFrom(type);

		/// <summary>
		/// Checks if the type parameter is not asignable from this type.
		/// </summary>
		/// <param name="type">The type object to ckeck.</param>
		/// <param name="t">The type base to asign.</param>
		/// <returns><see cref="true"/> when <see cref="t"/> is not assignable from <see cref="type"/>.</returns>
		public static bool IsNot(this Type type, Type t)
			=> !type.Is(t);

		/// <summary>
		/// Checks if the generic type is asignable from this type.
		/// </summary>
		/// <typeparam name="T">The generic <see cref="Type"/>.</typeparam>
		/// <param name="type">The type base to asign.</param>
		/// <returns><see cref="true"/> when <see cref="type"/> is assignable from generic type <see cref="T"/>.</returns>
		public static bool Is<T>(this Type type)
			=> type.Is(typeof(T));

		/// <summary>
		/// Checks if the generic type is not asignable from this type.
		/// </summary>
		/// <typeparam name="T">The generic <see cref="Type"/>.</typeparam>
		/// <param name="type">The type base to asign.</param>
		/// <returns><see cref="true"/> when <see cref="type"/> is not assignable from generic type <see cref="T"/>.</returns>
		public static bool IsNot<T>(this Type type)
			=> !type.Is<T>();
	}
}
